using APIEventosBotucatu.Core.Models;

namespace APIEventosBotucatu.Core.Interfaces
{
    public interface IEventsService
    {
        List<CityEvent> GetCityEvents();
        CityEvent GetCityEventById(long idEvent);
        List<CityEvent> GetCityEventsByTitle(string eventTitle);
        List<CityEvent> GetCityEventsByLocalAndDate(string local, DateTime dateHourEvent);
        CityEvent GetCityEventByTitleAndDate(string eventTitle, DateTime eventDate);
        List<CityEvent> GetCityEventByPriceRangeAndDate(decimal minPrice, decimal maxPrice, DateTime eventDate);
        CityEvent InsertCityEvent(CityEvent cityEvent);
        bool UpdateCityEvent(long idEvent, CityEvent cityEvent);
        bool DeleteCityEvent(long idEvent);
    }
}
