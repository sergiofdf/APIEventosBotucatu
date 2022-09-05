using APIEventosBotucatu.Core.Interfaces;
using APIEventosBotucatu.Core.Models;

namespace APIEventosBotucatu.Core.Services
{
    public class EventsService : IEventsService
    {
        public IEventsRepository _eventsRepository;
        public EventsService(IEventsRepository eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }
        public List<CityEvent> GetCityEvents()
        {
            return _eventsRepository.GetCityEvents();
        }
    }
}
