using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rubiera.Exceptions;
using rubiera.Services;

namespace rubiera.Controllers
{
    [ApiController]
    [Route("/api/weather")]
    public class WeatherApi : ControllerBase
    {
        private readonly ILogger<WeatherApi> _logger;

        private readonly RubieraService _rubiera;

        public WeatherApi(ILogger<WeatherApi> logger, RubieraService rubieraService)
        {
            _logger = logger;
            _rubiera = rubieraService;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get([FromQuery] Location location)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return Ok(_rubiera.getWeatherInfoForLocation(location));
                }
                catch (DataNotAvailable ex)
                {
                    int status = 422;
                    return StatusCode(status, apiError(status, ex.Message));
                }
                catch (ExternalServiceFailedException ex)
                {
                    int status = 429;
                    return StatusCode(status, apiError(status, ex.Message));
                }
                catch (CityNotFoundException ex)
                {
                    int status = 404;
                    return StatusCode(status, apiError(status, ex.Message));
                }
                catch (GeolocationServiceUnavailable ex)
                {
                    int status = 503;
                    return StatusCode(status, apiError(status, ex.Message));
                }
            }

            return BadRequest();
        }

        private ApiError apiError(int code, string msg)
        {
            var e = new ApiError();
            e.Code = code;
            e.Msg = msg;
            return e;
        }
    }
}