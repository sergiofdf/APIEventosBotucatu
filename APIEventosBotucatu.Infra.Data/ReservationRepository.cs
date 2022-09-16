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

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Query<EventReservation>(query).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }

        }
        public EventReservation GetReservationById(long idReservation)
        {
            var query = "SELECT * FROM EventReservation WHERE idReservation=@idReservation;";

            var parameters = new DynamicParameters();
            parameters.Add("idReservation", idReservation);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.QueryFirstOrDefault<EventReservation>(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }
        public List<EventReservation> GetReservationsByEventId(long idEvent)
        {
            var query = "SELECT * FROM EventReservation WHERE idEvent=@idEvent;";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Query<EventReservation>(query, parameters).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }

        }
        public EventReservation GetReservationByEventIdAndPersonName(long idEvent, string personName)
        {
            var query = "SELECT * FROM EventReservation WHERE idEvent=@idEvent AND personName=@personName;";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);
            parameters.Add("personName", personName);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.QueryFirstOrDefault<EventReservation>(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
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

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Query<ReservationWithTitleDTO>(query, parameters).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public bool InsertReservation(EventReservation eventReservation)
        {
            var query = "INSERT INTO EventReservation VALUES(@idEvent, @personName, @quantity);";

            var parameters = new DynamicParameters(eventReservation);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Execute(query, parameters) == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public bool UpdateReservation(long idReservation, EventReservation eventReservation)
        {
            var query = "UPDATE EventReservation SET idEvent=@idEvent, personName=@personName, quantity=@quantity WHERE idReservation=@idReservation;";

            eventReservation.IdReservation = idReservation;
            var parameters = new DynamicParameters(eventReservation);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Execute(query, parameters) == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public bool UpdateReservationQuantity(long idReservation, long quantity)
        {
            var query = "UPDATE EventReservation SET quantity=@quantity WHERE idReservation=@idReservation;";

            var parameters = new DynamicParameters();
            parameters.Add("idReservation", idReservation);
            parameters.Add("quantity", quantity);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Execute(query, parameters) == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }
        public bool DeleteReservation(long idReservation)
        {
            var query = "DELETE FROM EventReservation WHERE idReservation=@idReservation";

            var parameters = new DynamicParameters();
            parameters.Add("idReservation", idReservation);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Execute(query, parameters) == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
