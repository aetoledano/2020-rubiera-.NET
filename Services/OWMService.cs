using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rubiera.Exceptions;
using rubiera.Helpers;
using rubiera.openweathermap;

namespace rubiera.Services
{
    public class OWMService
    {
        private readonly ILogger<OCDService> _logger;

        private readonly RestClient restClient;

        private ConcurrentDictionary<string, long> times;
        private ConcurrentDictionary<string, WeatherInfo> reports;
        private Dictionary<string, City> cities;
        private HashSet<string> citiesIds;


        public OWMService(ILogger<OCDService> logger, RestClient restClient)
        {
            _logger = logger;
            this.restClient = restClient;

            times = new ConcurrentDictionary<string, long>();
            reports = new ConcurrentDictionary<string, WeatherInfo>();
            citiesIds = new HashSet<string>();
            cities = new Dictionary<string, City>();

            //read the cities
            string text = File.ReadAllText("cities.json");
            foreach (var c in JsonConvert.DeserializeObject<City[]>(text))
            {
                cities[c.Name.Trim().ToLower()] = c;
                citiesIds.Add(c.Id);
            }

            logger.LogInformation("Readed " + cities.Count + " cities");
        }

        public WeatherInfo getWeatherUpdateForCityName(string cityName)
        {
            City city;
            if (!cities.TryGetValue(cityName.ToLower(), out city))
            {
                throw new CityNotFoundException("City with name " + cityName + " could not be found.");
            }

            return getWeatherUpdateForCity(city.Id);
        }

        public WeatherInfo getWeatherUpdateForCity(String cityId)
        {
            if (!citiesIds.Contains(cityId))
            {
                throw new CityNotFoundException("City with id " + cityId + " could not be found.");
            }

            updateAccessTime(cityId);
            WeatherInfo wi;
            if (!reports.TryGetValue(cityId, out wi))
            {
                throw new DataNotAvailable("There is not data still available for this location.");
            }

            return wi;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void updateAccessTime(String key)
        {
            long lastTime;
            if (!times.TryGetValue(key, out lastTime))
            {
                times[key] = lastTime = DateTimeExtensions.currentTimeMillis();
                ThreadPool.QueueUserWorkItem((ignored) => { updateWeatherInfoByCityKey(key); });
            }

            if (DateTimeExtensions.currentTimeMillis() - lastTime > Constants.OPEN_WEATHER_MAP_REFRESH_INTERVAL)
            {
                times[key] = DateTimeExtensions.currentTimeMillis();
                ThreadPool.QueueUserWorkItem((ignored) => { updateWeatherInfoByCityKey(key); });
            }
        }

        private void updateWeatherInfoByCityKey(String cityId)
        {
            String uri = Constants.WEATHER_CITY_ID_URI
                .Replace(Constants.CITY_ID_PLACEHOLDER, cityId)
                .Replace(Constants.KEY_PLACEHOLDER, Constants.OPEN_WEATHER_MAP_KEY);

            try
            {
                WeatherResponse response = restClient.get<WeatherResponse>(uri);
                WeatherInfo weatherInfo = proccessWeatherResponse(response);
                reports[cityId] = weatherInfo;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error requesting external service: " + ex.ToString());
            }
        }

        private WeatherInfo proccessWeatherResponse(WeatherResponse response)
        {
            WeatherInfo info = new WeatherInfo();

            info.Name = response.Name;

            Weather w = response.Weather[0];
            info.Sky = w.Main;
            info.SkyDesc = w.Description;
            info.SkyImageUri = Constants.WEATHER_ICON_URI.Replace(Constants.ICON_ID_PLACEHOLDER, w.Icon);

            Main m = response.Main;
            info.Temp = m.Temp;
            info.ActualFeel = m.Feels_Like;
            info.HumidityPercentage = m.Humidity;
            if (response.Clouds != null)
                info.CloudsPercentage = response.Clouds.All;

            return info;
        }
    }
}