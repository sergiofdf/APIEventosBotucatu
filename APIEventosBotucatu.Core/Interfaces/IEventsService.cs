using APIEventosBotucatu.Core.Models;

namespace APIEventosBotucatu.Core.Interfaces
{
    public interface IEventsService
    {
        List<CityEvent> GetCityEvents();
        CityEvent GetCityEventById(long idEvent);
        bool InsertCityEvent(CityEvent cityEvent);
        bool UpdateCityEvent(long idEvent, CityEvent cityEvent);
        bool DeleteCityEvent(long idEvent);
    }
}
