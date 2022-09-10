using APIEventosBotucatu.Core.Interfaces;
using APIEventosBotucatu.Core.Models;

namespace APIEventosBotucatu.Core.Services
{
    public class EventsService : IEventsService
    {
        public IEventsRepository _eventsRepository;
        public IReservationService _reservationService;
        public EventsService(IEventsRepository eventsRepository, IReservationService reservationService)
        {
            _eventsRepository = eventsRepository;
            _reservationService = reservationService;
        }

        public List<CityEvent> GetCityEvents()
        {
            return _eventsRepository.GetCityEvents();
        }
        public CityEvent GetCityEventById(long idEvent)
        {
            return _eventsRepository.GetCityEventById(idEvent);
        }

        public List<CityEvent> GetCityEventsByTitle(string eventTitle)
        {
            return _eventsRepository.GetCityEventsByTitle(eventTitle);
        }

        public CityEvent GetCityEventByTitleAndDate(string eventTitle, DateTime eventDate)
        {
            return _eventsRepository.GetCityEventByTitleAndDate(eventTitle, eventDate);
        }

        public CityEvent InsertCityEvent(CityEvent cityEvent)
        {
            _eventsRepository.InsertCityEvent(cityEvent);
            return _eventsRepository.GetCityEventByTitleAndDate(cityEvent.Title, cityEvent.DateHourEvent);
        }
        public bool UpdateCityEvent(long idEvent, CityEvent cityEvent)
        {
            return _eventsRepository.UpdateCityEvent(idEvent, cityEvent);
        }
        public bool DeleteCityEvent(long idEvent)
        {
            var listEventsReservation = _reservationService.GetReservationsByEventId(idEvent);
            if (listEventsReservation.Count == 0)
            {
                return _eventsRepository.DeleteCityEvent(idEvent);
            }
            CityEvent cityEvent = GetCityEventById(idEvent);
            cityEvent.Status = false;
            return _eventsRepository.UpdateCityEvent(idEvent, cityEvent);
        }
    }
}
