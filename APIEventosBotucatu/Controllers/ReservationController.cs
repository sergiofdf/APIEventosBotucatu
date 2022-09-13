using APIEventosBotucatu.Core.Interfaces;
using APIEventosBotucatu.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace APIEventosBotucatu.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [EnableCors("PolicyCors")]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private IReservationService _reservationService;
        private readonly IMapper _mapper;
        public ReservationController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpGet("/Reservas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<List<EventReservation>> GetAllReservations()
        {
            return Ok(_reservationService.GetReservations());
        }

        [HttpGet("/Reservas/{idReservation}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<EventReservation> GetReservation(long idReservation)
        {
            var eventReservation = _reservationService.GetReservationById(idReservation);
            if (eventReservation == null)
            {
                return NotFound();
            }
            return Ok(eventReservation);
        }

        [HttpGet("/Reservas/NomePessoaENomeEvento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public ActionResult<ReservationWithTitleDTO> GetReservationByPersonNameAndEventTitle(string personName, string eventTitle)
        {
            var eventReservation = _reservationService.GetReservationsByPersonNameAndEventTitle(personName, eventTitle);
            if (eventReservation == null)
            {
                return NotFound();
            }
            return Ok(eventReservation);
        }

        [HttpPost("/Reservas")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "cliente, admin")]
        public ActionResult<EventReservation> PostEventReservation(EventReservationDTO eventReservation)
        {
            EventReservation eventMapped = _mapper.Map<EventReservation>(eventReservation);
            var result = _reservationService.InsertReservation(eventMapped);
            return CreatedAtAction(nameof(PostEventReservation), result);
        }

        [HttpPut("/Reservas")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateReservation(long idReservation, EventReservationDTO eventReservation)
        {
            EventReservation eventMapped = _mapper.Map<EventReservation>(eventReservation);
            if (!_reservationService.UpdateReservation(idReservation, eventMapped))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("/Reservas/Quantidade")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateReservationQuantity(long idReservation, long quantity)
        {
            if (!_reservationService.UpdateReservationQuantity(idReservation, quantity))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("/Reservas")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteCityEvent(long idReservation)
        {
            if (!_reservationService.DeleteReservation(idReservation))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
