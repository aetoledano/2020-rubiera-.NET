namespace rubiera.Services
{
    public class RubieraService
    {

        private OCDService _ocdService;
        private OWMService _owmService;

        public RubieraService(OCDService ocdService, OWMService owmService)
        {
            _ocdService = ocdService;
            _owmService = owmService;
        }

        public WeatherInfo getWeatherInfoForLocation(Location loc)
        {
            string cityName = _ocdService.getCityName(loc);
            return _owmService.getWeatherUpdateForCityName(cityName);
        }
    }
}