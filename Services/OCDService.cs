using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using rubiera.Exceptions;
using rubiera.Helpers;
using rubiera.opencagedata;

namespace rubiera.Services
{
    public class OCDService
    {
        private readonly ILogger<OCDService> _logger;

        private readonly RestClient restClient;

        private ConcurrentDictionary<string, string> locations;
        private BlockingCollection<Location> locQueue;
        private bool serviceLocked;
        private long availableDate;

        public OCDService(RestClient restClient, ILogger<OCDService> logger)
        {
            this.restClient = restClient;
            _logger = logger;
            locations = new ConcurrentDictionary<string, string>();
            locQueue = new BlockingCollection<Location>();
            serviceLocked = false;
            availableDate = 0;
            Task.Run(QueueConsumer);
        }

        public string getCityName(Location loc)
        {
            String cityName;
            if (locations.TryGetValue(loc.ToString(), out cityName))
            {
                return cityName;
            }

            if (serviceLocked && DateTimeExtensions.currentTimeMillis() < availableDate)
            {
                throw new GeolocationServiceUnavailable("Reached geolocation queries limit.");
            }

            locQueue.Add(loc);

            throw new DataNotAvailable("Location is being processed.");
        }

        private void processGeolocationQueries(Location loc)
        {
            string unused_value;
            if (locations.TryGetValue(loc.ToString(), out unused_value))
                return;

            String uri = Constants.REVERSE_GEOCODING_URI
                .Replace(Constants.LAT_PLACEHOLDER, loc.lat + "")
                .Replace(Constants.LON_PLACEHOLDER, loc.lon + "")
                .Replace(Constants.KEY_PLACEHOLDER, Constants.OPEN_CAGE_DATA_KEY);

            try
            {
                ReverseGeocodingResponse response = restClient.get<ReverseGeocodingResponse>(uri);
                serviceLocked = false;
                _logger.LogInformation("OpenCageDataService " + response.Rate);
                availableDate = response.Rate.Reset;
                locations[loc.ToString()] = response.Results[0].Components.State;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error requesting external service: " + ex.ToString());
                if (ex.Message.Contains("402"))
                {
                    serviceLocked = true;
                }
            }
        }

        private async void QueueConsumer()
        {
            while (true)
            {
                Location loc = null;
                try
                {
                    loc = locQueue.Take();
                }
                catch (Exception any)
                {
                    _logger.LogInformation(any.Message);
                    return;
                }

                if (loc != null)
                {
                    Task.Factory.StartNew(() => processGeolocationQueries(loc));
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}