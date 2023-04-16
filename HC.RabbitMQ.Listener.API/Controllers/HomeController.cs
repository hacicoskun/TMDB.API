using HC.Shared.Application.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HC.RabbitMQ.Listener.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IRedisClient _redisClient;

        public HomeController(IRedisClient redisClient)
        {
            _redisClient = redisClient;
        }
        // GET: api/<HomeController>
        [HttpGet]
        public IEnumerable<string> RedisTest()
        {
            //_redisClient.SetStringAsync("key", "value");
            //_redisClient.GetStringAsync("key");
            return new string[] { "value1", "value2" };
        }

       
    }
}
