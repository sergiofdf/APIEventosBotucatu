using APIEventosBotucatu.Core.Models;

namespace APIEventosBotucatu.Core.Interfaces
{
    public interface IReservationRepository
    {
        List<EventReservation> GetReservations();
        EventReservation GetReservationById(long idReservation);
        List<EventReservation> GetReservationsByEventId(long idEvent);
        EventReservation GetReservationByEventIdAndPersonName(long idEvent, string personName);
        bool InsertReservation(EventReservation eventReservation);
        bool UpdateReservation(long idReservation, EventReservation eventReservation);
        bool DeleteReservation(long idReservation);
    }
}
