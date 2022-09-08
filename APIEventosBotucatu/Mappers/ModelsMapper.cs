using APIEventosBotucatu.Core.Models;
using AutoMapper;

namespace APIEventosBotucatu.Mappers
{
    public class ModelsMapper : Profile
    {
        public ModelsMapper()
        {
            CreateMap<CityEventDTO, CityEvent>();
            CreateMap<EventReservationDTO, EventReservation>();
        }
    }
}
