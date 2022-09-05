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
            var query = "SELECT * FROM [sergio.dias].dbo.CityEvent;";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query).ToList();
        }

        public CityEvent GetCityEventById(long idEvent)
        {
            var query = "SELECT * FROM [sergio.dias].dbo.CityEvent WHERE IdEvent=@idEvent;";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<CityEvent>(query, parameters);
        }
    }
}
