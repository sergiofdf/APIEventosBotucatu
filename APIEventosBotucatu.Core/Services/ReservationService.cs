using APIEventosBotucatu.Core.Interfaces;
using APIEventosBotucatu.Core.Models;

namespace APIEventosBotucatu.Core.Services
{
    public class ReservationService : IReservationService
    {
        public IReservationRepository _reservationRepository;
        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public List<EventReservation> GetReservations()
        {
            return _reservationRepository.GetReservations();
        }
        public EventReservation GetReservationById(long idReservation)
        {
            return _reservationRepository.GetReservationById(idReservation);
        }

        public EventReservation GetReservationByEventIdAndPersonName(long idEvent, string personName)
        {
            return _reservationRepository.GetReservationByEventIdAndPersonName(idEvent, personName);
        }

        public EventReservation InsertReservation(EventReservation eventReservation)
        {
            _reservationRepository.InsertReservation(eventReservation);
            return _reservationRepository.GetReservationByEventIdAndPersonName(eventReservation.IdEvent, eventReservation.PersonName);
        }

        public bool UpdateReservation(long idReservation, EventReservation eventReservation)
        {
            return _reservationRepository.UpdateReservation(idReservation, eventReservation);
        }
        public bool DeleteReservation(long idReservation)
        {
            return _reservationRepository.DeleteReservation(idReservation);
        }
    }
}
