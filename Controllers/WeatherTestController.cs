using DAL8.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Net8Features.Models;
using System;
using System.Collections.Frozen;
using CW = System.Diagnostics.Debug;
namespace Net8Features.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherTestController : ControllerBase
    {
        IWeatherService _service;
        IServiceProvider _serviceProvider;
        public WeatherTestController(IWeatherService service, IServiceProvider serviceProvider)
        {

            _service = service;
            _serviceProvider = serviceProvider;
        }

        [HttpGet("GetTestWeatherForecast")]
        //public List<int> Get([FromKeyedServices(_config.Value.ServiceName)] IWeatherService service)      
        public IActionResult Get(int id)
        {
            _service.GetMyName();
            return Ok();
        }

        [HttpGet("GetUserByIDKeyedServiceScoped")]
        public User GetUserByIDKeyedServiceScoped([FromKeyedServices("scoped")] NorthwindContext dbContext, int id)
        {
            var result = dbContext.GetUserByID(id);
            return result;
        }

        [HttpGet("GetUserByIDKeyedServiceTransient/{id}")]
        public User GetUserByIDKeyedServiceTransient(NorthwindContext dbContext, int id)
        {
            var result = dbContext.GetUserByID(id);
            return result;
        }

        [HttpGet("GetUserByIDKeyedServiceProvider/{serviceType}")]
        public IActionResult GetUserByIDKeyedServiceProvider(ServiceType serviceType)
        {
            var weatherService = _serviceProvider.GetRequiredKeyedService<IWeatherService>(serviceType.ToString());
            weatherService.GetMyName();
            return Ok();
        }

        [HttpGet("TestStreamingDeserialization")]
        public Task<List<ShortCodPlayers>> TestStreamingDeserialization([FromKeyedServices("weather2")] IWeatherService service)
        {
            return service.GetAllCodPlayers();
        }        
    }
}
