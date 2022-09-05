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
        public CityEvent GetCityEventById(long idEvent)
        {
            return _eventsRepository.GetCityEventById(idEvent);
        }

        public bool InsertCityEvent(CityEvent cityEvent)
        {
            return _eventsRepository.InsertCityEvent(cityEvent);
        }
        public bool UpdateCityEvent(long idEvent, CityEvent cityEvent)
        {
            return _eventsRepository.UpdateCityEvent(idEvent, cityEvent);
        }
        public bool DeleteCityEvent(long idEvent)
        {
            return _eventsRepository.DeleteCityEvent(idEvent);
        }
    }
}
