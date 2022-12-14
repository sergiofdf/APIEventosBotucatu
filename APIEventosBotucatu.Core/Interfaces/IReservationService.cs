using APIEventosBotucatu.Core.Models;

namespace APIEventosBotucatu.Core.Interfaces
{
    public interface IReservationService
    {
        List<EventReservation> GetReservations();
        EventReservation GetReservationById(long idReservation);
        List<EventReservation> GetReservationsByEventId(long idEvent);
        EventReservation GetReservationByEventIdAndPersonName(long idEvent, string personName);
        List<ReservationWithTitleDTO> GetReservationsByPersonNameAndEventTitle(string personName, string eventTitle);
        EventReservation InsertReservation(EventReservation eventReservation);
        bool UpdateReservation(long idReservation, EventReservation eventReservation);
        bool UpdateReservationQuantity(long idReservation, long quantity);
        bool DeleteReservation(long idReservation);
    }
}
