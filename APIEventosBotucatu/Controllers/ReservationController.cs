using APIEventosBotucatu.Core.Interfaces;
using APIEventosBotucatu.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIEventosBotucatu.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ReservationController : ControllerBase
    {
        private IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("/Reservas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<EventReservation>> GetAllReservations()
        {
            return Ok(_reservationService.GetReservations());
        }

        [HttpGet("/Reservas/{idReservation}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EventReservation> GetReservation(long idReservation)
        {
            var eventReservation = _reservationService.GetReservationById(idReservation);
            if (eventReservation == null)
            {
                return NotFound();
            }
            return Ok(eventReservation);
        }

        [HttpPost("/Reservas")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EventReservation> PostEventReservation(EventReservation eventReservation)
        {
            _reservationService.InsertReservation(eventReservation);
            return CreatedAtAction(nameof(PostEventReservation), eventReservation);
        }

        [HttpPut("/Reservas")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateReservation(long idReservation, EventReservation eventReservation)
        {
            if (!_reservationService.UpdateReservation(idReservation, eventReservation))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("/Reservas")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
