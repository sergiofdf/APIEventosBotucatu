using APIEventosBotucatu.Core.Interfaces;
using APIEventosBotucatu.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEventosBotucatu.Filters
{
    public class CheckIfEventExistsActionFilter : ActionFilterAttribute
    {
        private readonly IEventsService _eventsService;
        public CheckIfEventExistsActionFilter(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            CityEventDTO cityEvent = (CityEventDTO)context.ActionArguments["cityEvent"];

            List<CityEvent> evenstList = _eventsService.GetCityEvents();

            bool eventAlreadyRegistered = evenstList.FindAll(x => (x.Title == cityEvent.Title && x.DateHourEvent.Date == cityEvent.DateHourEvent.Date))
                .Any();

            if (eventAlreadyRegistered)
            {
                var problem = new ProblemDetails
                {
                    Status = 409,
                    Title = "Conflict",
                    Detail = "O evento já foi cadastrado.",
                };

                context.Result = new ObjectResult(problem);
            }
        }
    }
}
