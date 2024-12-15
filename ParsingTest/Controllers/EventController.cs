using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using ParsingTest.Models;
using ParsingTest.Services.Interfaces;

namespace ParsingTest.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EventController(IDataBaseService dataBaseService) : Controller
    {
        private FlurlClient client = new("https://line32w.bk6bba-resources.com");
        private readonly IDataBaseService _dataBaseService = dataBaseService;

        [HttpGet("/{id}")]
        public IActionResult GetEvent(long id)
        {
            //EventResponce result = new();

            //var temp = await client.Request("events", "event")
            //    .AppendQueryParam("lang", "ru")
            //    .AppendQueryParam("scopeMarket", "1600")
            //    .AppendQueryParam("eventId", id)
            //    .GetAsync()
            //    .ReceiveJson<EventResponce>();

            return Ok(_dataBaseService.GetEvent(id));
        }

        [HttpGet("/Range/Ids")]
        public IActionResult GetEventIds(int offset, int count)
        {
            var result = _dataBaseService.GetEventsIds().Skip(offset).Take(count);

            return Ok(result);
        }

        [HttpGet("/Factors")]
        public IActionResult GetEventFactors(long eventId)
        {
            List<Factor> result = [];

            _dataBaseService.LastResult.CustomFactors.Where(_ => _.E == eventId).ToList().ForEach(_ => result.AddRange(_.Factors));

            return Ok(result);
        }
    }
}
