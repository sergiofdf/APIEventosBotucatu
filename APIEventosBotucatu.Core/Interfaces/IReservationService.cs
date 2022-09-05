﻿using APIEventosBotucatu.Core.Models;

namespace APIEventosBotucatu.Core.Interfaces
{
    public interface IReservationService
    {
        List<EventReservation> GetReservations();
        EventReservation GetReservationById(long idReservation);
        bool InsertReservation(EventReservation eventReservation);
        bool UpdateReservation(long idReservation, EventReservation eventReservation);
        bool DeleteReservation(long idReservation);
    }
}