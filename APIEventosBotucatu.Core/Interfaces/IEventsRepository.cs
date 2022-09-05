using APIEventosBotucatu.Core.Models;

namespace APIEventosBotucatu.Core.Interfaces
{
    public interface IEventsRepository
    {
        List<CityEvent> GetCityEvents();
    }
}
