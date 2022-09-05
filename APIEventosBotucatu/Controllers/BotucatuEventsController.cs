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

        [HttpGet("/Eventos/{idEvent}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CityEvent> GetEvent(long idEvent)
        {
            var cityEvent = _eventsService.GetCityEventById(idEvent);
            if (cityEvent == null)
            {
                return NotFound();
            }
            return Ok(cityEvent);
        }
    }
}
