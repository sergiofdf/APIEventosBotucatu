using APIEventosBotucatu.Core.Models;

namespace APIEventosBotucatu.Core.Interfaces
{
    public interface IEventsService
    {
        List<CityEvent> GetCityEvents();
        CityEvent GetCityEventById(long idEvent);
    }
}
