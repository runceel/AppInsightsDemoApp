using AppInsightsDemoApp.RedisClients;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppInsightsDemoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendController : ControllerBase
    {
        private readonly IRedisClient _redisClient;

        public SendController(IRedisClient redisClient)
        {
            _redisClient = redisClient ?? throw new ArgumentNullException(nameof(redisClient));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SendInput body)
        {
            if (body.Input == "error")
            {
                throw new InvalidOperationException("テスト用の例外です。");
            }

            if (body.Input.StartsWith("cache ", StringComparison.InvariantCultureIgnoreCase))
            {
                await _redisClient.StringSetAsync("cache", body.Input[6..^0]);
            }

            if (body.Input == "slow")
            {
                await Task.Delay(6000);
            }

            return new OkResult();
        }

        [HttpGet]
        public async Task<ActionResult<GetResponse>> Get([FromQuery] string key)
        {
            if (await _redisClient.ContainsKeyAsync(key))
            {
                var res = new OkObjectResult(new GetResponse 
                {
                    Cache = await _redisClient.StringGetAsync(key),
                });
                return res;
            }

            return new NotFoundResult();
        }
    }

    public class GetResponse
    {
        [JsonPropertyName("cache")]
        public string Cache { get; set; }
    }

    public class SendInput
    {
        [JsonPropertyName("input")]
        public string Input { get; set; }
    }
}
