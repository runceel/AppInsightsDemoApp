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
        [HttpPost]
        public IActionResult Post([FromBody]SendInput body)
        {
            if (body.Input == "error")
            {
                throw new InvalidOperationException("テスト用の例外です。");
            }

            return new OkResult();
        }
    }

    public class SendInput
    {
        [JsonPropertyName("input")]
        public string Input { get; set; }
    }
}
