using APIEventosBotucatu.Core.Interfaces;
using APIEventosBotucatu.Core.Models;
using APIEventosBotucatu.Filters;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public BotucatuEventsController(IEventsService eventsService, IMapper mapper)
        {
            _eventsService = eventsService;
            _mapper = mapper;
        }

        [HttpGet("/Eventos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CityEvent>> GetAllEvents()
        {
            return Ok(_eventsService.GetCityEvents());
        }

        [HttpGet("/Eventos/id/{idEvent}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(CheckIfEventIdRegisteredActionFilter))]
        public ActionResult<CityEvent> GetEvent(long idEvent)
        {
            return Ok(_eventsService.GetCityEventById(idEvent));
        }

        [HttpGet("/Eventos/title/{eventTitle}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CityEvent>> GetEventByTitle(string eventTitle)
        {
            var eventsList = _eventsService.GetCityEventsByTitle(eventTitle);
            if (!eventsList.Any())
            {
                return NotFound();
            }
            return Ok(_eventsService.GetCityEventsByTitle(eventTitle));
        }

        [HttpPost("/Eventos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ServiceFilter(typeof(CheckIfEventExistsActionFilter))]
        public ActionResult<CityEvent> PostCityEvent(CityEventDTO cityEvent)
        {
            CityEvent cityEventMapped = _mapper.Map<CityEvent>(cityEvent);
            var result = _eventsService.InsertCityEvent(cityEventMapped);
            return CreatedAtAction(nameof(PostCityEvent), result);
        }

        [HttpPut("/Eventos")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(CheckIfEventIdRegisteredActionFilter))]
        public IActionResult UpdateCityEvent(long idEvent, CityEventDTO cityEvent)
        {
            CityEvent cityEventMapped = _mapper.Map<CityEvent>(cityEvent);
            _eventsService.UpdateCityEvent(idEvent, cityEventMapped);
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
