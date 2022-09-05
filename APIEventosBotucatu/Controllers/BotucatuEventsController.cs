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

        [HttpPost("/Eventos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CityEvent> PostCityEvent(CityEvent cityEvent)
        {
            _eventsService.InsertCityEvent(cityEvent);
            return CreatedAtAction(nameof(PostCityEvent), cityEvent);
        }

        [HttpPut("/Eventos")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateCityEvent(long idEvent, CityEvent cityEvent)
        {
            if (!_eventsService.UpdateCityEvent(idEvent, cityEvent))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("/Eventos")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCityEvent(long idEvent)
        {
            if (!_eventsService.DeleteCityEvent(idEvent))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
