using APIEventosBotucatu.Core.Interfaces;
using APIEventosBotucatu.Core.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace APIEventosBotucatu.Infra.Data
{
    public class EventsRepository : IEventsRepository
    {
        private IConfiguration _configuration;

        public EventsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<CityEvent> GetCityEvents()
        {
            var query = "SELECT * FROM CityEvent;";

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Query<CityEvent>(query).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public CityEvent GetCityEventById(long idEvent)
        {
            var query = "SELECT * FROM CityEvent WHERE IdEvent=@idEvent;";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.QueryFirstOrDefault<CityEvent>(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public List<CityEvent> GetCityEventsByTitle(string eventTitle)
        {
            var query = "SELECT * FROM CityEvent WHERE title LIKE CONCAT('%',@title,'%');";

            var parameters = new DynamicParameters();
            parameters.Add("title", eventTitle);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Query<CityEvent>(query, parameters).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public List<CityEvent> GetCityEventsByLocalAndDate(string local, DateTime dateHourEvent)
        {
            var query = "SELECT * FROM CityEvent WHERE local LIKE CONCAT('%',@local,'%') AND CONVERT(DATE, dateHourEvent)=@dateHourEvent ;";

            var parameters = new DynamicParameters();
            parameters.Add("local", local);
            parameters.Add("dateHourEvent", dateHourEvent.Date);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Query<CityEvent>(query, parameters).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public CityEvent GetCityEventByTitleAndDate(string eventTitle, DateTime eventDate)
        {
            var query = "SELECT * FROM CityEvent WHERE title=@title AND CONVERT(DATE, dateHourEvent)=@dateHourEvent;";

            var parameters = new DynamicParameters();
            parameters.Add("title", eventTitle);
            parameters.Add("dateHourEvent", eventDate.Date);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.QueryFirstOrDefault<CityEvent>(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public List<CityEvent> GetCityEventByPriceRangeAndDate(decimal minPrice, decimal maxPrice, DateTime eventDate)
        {
            var query = "SELECT * FROM CityEvent WHERE price >= @minprice AND price <= @maxPrice AND CONVERT(DATE, dateHourEvent)=@dateHourEvent;";

            var parameters = new DynamicParameters();
            parameters.Add("minPrice", minPrice);
            parameters.Add("maxPrice", maxPrice);
            parameters.Add("dateHourEvent", eventDate.Date);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Query<CityEvent>(query, parameters).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public bool InsertCityEvent(CityEvent cityEvent)
        {
            var query = "INSERT INTO CityEvent VALUES(@title, @description, @dateHourEvent, @local, @address, @price, @status);";

            var parameters = new DynamicParameters(cityEvent);

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

        public bool UpdateCityEvent(long idEvent, CityEvent cityEvent)
        {
            var query = "UPDATE CityEvent SET title=@title, description=@description, dateHourEvent=@dateHourEvent, local=@local, address=@address, price=@price, status=@status  WHERE idEvent=@idEvent;";

            cityEvent.IdEvent = idEvent;
            var parameters = new DynamicParameters(cityEvent);

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

        public bool DeleteCityEvent(long idEvent)
        {
            var query = "DELETE FROM CityEvent WHERE idEvent=@idEvent";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

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
