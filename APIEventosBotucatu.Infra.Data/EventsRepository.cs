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

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query).ToList();
        }

        public CityEvent GetCityEventById(long idEvent)
        {
            var query = "SELECT * FROM CityEvent WHERE IdEvent=@idEvent;";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<CityEvent>(query, parameters);
        }

        public List<CityEvent> GetCityEventsByTitle(string eventTitle)
        {
            var query = "SELECT * FROM CityEvent WHERE title LIKE CONCAT('%',@title,'%');";

            var parameters = new DynamicParameters();
            parameters.Add("title", eventTitle);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public List<CityEvent> GetCityEventsByLocalAndDate(string local, DateTime dateHourEvent)
        {
            var query = "SELECT * FROM CityEvent WHERE local LIKE CONCAT('%',@local,'%') AND CONVERT(DATE, dateHourEvent)=@dateHourEvent ;";

            var parameters = new DynamicParameters();
            parameters.Add("local", local);
            parameters.Add("dateHourEvent", dateHourEvent.Date);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public CityEvent GetCityEventByTitleAndDate(string eventTitle, DateTime eventDate)
        {
            var query = "SELECT * FROM CityEvent WHERE title=@title AND CONVERT(DATE, dateHourEvent)=@dateHourEvent;";

            var parameters = new DynamicParameters();
            parameters.Add("title", eventTitle);
            parameters.Add("dateHourEvent", eventDate.Date);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<CityEvent>(query, parameters);
        }

        public List<CityEvent> GetCityEventByPriceRangeAndDate(decimal minPrice, decimal maxPrice, DateTime eventDate)
        {
            var query = "SELECT * FROM CityEvent WHERE price >= @minprice AND price <= @maxPrice AND CONVERT(DATE, dateHourEvent)=@dateHourEvent;";

            var parameters = new DynamicParameters();
            parameters.Add("minPrice", minPrice);
            parameters.Add("maxPrice", maxPrice);
            parameters.Add("dateHourEvent", eventDate.Date);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public bool InsertCityEvent(CityEvent cityEvent)
        {
            var query = "INSERT INTO CityEvent VALUES(@title, @description, @dateHourEvent, @local, @adress, @price, @status);";

            var parameters = new DynamicParameters(cityEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateCityEvent(long idEvent, CityEvent cityEvent)
        {
            var query = "UPDATE CityEvent SET title=@title, description=@description, dateHourEvent=@dateHourEvent, local=@local, adress=@adress, price=@price, status=@status  WHERE idEvent=@idEvent;";

            cityEvent.IdEvent = idEvent;
            var parameters = new DynamicParameters(cityEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool DeleteCityEvent(long idEvent)
        {
            var query = "DELETE FROM CityEvent WHERE idEvent=@idEvent";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }
    }
}
