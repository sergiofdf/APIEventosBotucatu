using APIEventosBotucatu.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEventosBotucatu.Filters
{
    public class CheckIfEventIdRegisteredActionFilter : ActionFilterAttribute
    {
        private readonly IEventsService _eventsService;
        public CheckIfEventIdRegisteredActionFilter(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idEvent = (long)context.ActionArguments["idEvent"];
            if (_eventsService.GetCityEventById(idEvent) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
    }
}
