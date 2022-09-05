using APIEventosBotucatu.Core.Interfaces;
using APIEventosBotucatu.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIEventosBotucatu.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class BotucatuEventsController : ControllerBase
    {
        private readonly IEventsService _eventsService;

        public BotucatuEventsController(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        [HttpGet("/Eventos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CityEvent>> GetAllEvents()
        {
            return Ok(_eventsService.GetCityEvents());
        }
    }
}
