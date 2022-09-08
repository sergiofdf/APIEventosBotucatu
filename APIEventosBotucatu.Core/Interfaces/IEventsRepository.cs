using APIEventosBotucatu.Core.Models;

namespace APIEventosBotucatu.Core.Interfaces
{
    public interface IEventsRepository
    {
        List<CityEvent> GetCityEvents();
        CityEvent GetCityEventById(long idEvent);
        CityEvent GetCityEventByTitleAndDate(string eventTitle, DateTime eventDate);
        bool InsertCityEvent(CityEvent cityEvent);
        bool UpdateCityEvent(long idEvent, CityEvent cityEvent);
        bool DeleteCityEvent(long idEvent);
    }
}
