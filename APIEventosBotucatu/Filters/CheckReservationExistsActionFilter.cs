using APIEventosBotucatu.Core.Interfaces;
using APIEventosBotucatu.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEventosBotucatu.Filters
{
    public class CheckReservationExistsActionFilter : ActionFilterAttribute
    {
        private readonly IReservationService _reservationService;

        public CheckReservationExistsActionFilter(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            EventReservationDTO eventReservation = (EventReservationDTO)context.ActionArguments["eventReservation"];

            List<EventReservation> reservationList = _reservationService.GetReservations();

            bool reservationRegistered = reservationList.FindAll(x => x.IdEvent == eventReservation.IdEvent && x.PersonName == eventReservation.PersonName).Any();

            if (reservationRegistered)
            {
                var problem = new ProblemDetails
                {
                    Status = 409,
                    Title = "Conflict",
                    Detail = "Esta reserva já foi cadastrada.",
                };

                context.Result = new ObjectResult(problem);
            }
        }
    }
}
