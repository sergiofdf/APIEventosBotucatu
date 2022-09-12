using APIEventosBotucatu.Core.Interfaces;
using APIEventosBotucatu.Core.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace APIEventosBotucatu.Infra.Data
{
    public class ReservationRepository : IReservationRepository
    {
        private IConfiguration _configuration;

        public ReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<EventReservation> GetReservations()
        {
            var query = "SELECT * FROM EventReservation;";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<EventReservation>(query).ToList();
        }
        public EventReservation GetReservationById(long idReservation)
        {
            var query = "SELECT * FROM EventReservation WHERE idReservation=@idReservation;";

            var parameters = new DynamicParameters();
            parameters.Add("idReservation", idReservation);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<EventReservation>(query, parameters);
        }
        public List<EventReservation> GetReservationsByEventId(long idEvent)
        {
            var query = "SELECT * FROM EventReservation WHERE idEvent=@idEvent;";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<EventReservation>(query, parameters).ToList();
        }
        public EventReservation GetReservationByEventIdAndPersonName(long idEvent, string personName)
        {
            var query = "SELECT * FROM EventReservation WHERE idEvent=@idEvent AND personName=@personName;";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);
            parameters.Add("personName", personName);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<EventReservation>(query, parameters);
        }

        public List<ReservationWithTitleDTO> GetReservationsByPersonNameAndEventTitle(string personName, string eventTitle)
        {
            var query = @"SELECT er.IdReservation, er.IdEvent, er.PersonName, er.Quantity, ce.Title 
FROM EventReservation er JOIN CityEvent ce
ON er.IdEvent = ce.IdEvent
WHERE er.PersonName = @personName AND ce.Title LIKE CONCAT('%', @eventTitle, '%');";


            var parameters = new DynamicParameters();
            parameters.Add("personName", personName);
            parameters.Add("eventTitle", eventTitle);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<ReservationWithTitleDTO>(query, parameters).ToList();
        }

        public bool InsertReservation(EventReservation eventReservation)
        {
            var query = "INSERT INTO EventReservation VALUES(@idEvent, @personName, @quantity);";

            var parameters = new DynamicParameters(eventReservation);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateReservation(long idReservation, EventReservation eventReservation)
        {
            var query = "UPDATE EventReservation SET idEvent=@idEvent, personName=@personName, quantity=@quantity WHERE idReservation=@idReservation;";

            eventReservation.IdReservation = idReservation;
            var parameters = new DynamicParameters(eventReservation);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateReservationQuantity(long idReservation, long quantity)
        {
            var query = "UPDATE EventReservation SET quantity=@quantity WHERE idReservation=@idReservation;";

            var parameters = new DynamicParameters();
            parameters.Add("idReservation", idReservation);
            parameters.Add("quantity", quantity);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }
        public bool DeleteReservation(long idReservation)
        {
            var query = "DELETE FROM EventReservation WHERE idReservation=@idReservation";

            var parameters = new DynamicParameters();
            parameters.Add("idReservation", idReservation);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }
    }
}
