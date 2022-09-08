using APIEventosBotucatu.Core.Interfaces;
using APIEventosBotucatu.Core.Services;
using APIEventosBotucatu.Filters;
using APIEventosBotucatu.Infra.Data;
using APIEventosBotucatu.Mappers;

namespace APIEventosBotucatu
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(ModelsMapper));

            builder.Services.AddMvc(options =>
            {
                options.Filters.Add<GeneralExceptionFilter>();
            });

            builder.Services.AddScoped<IEventsService, EventsService>();
            builder.Services.AddScoped<IEventsRepository, EventsRepository>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

            builder.Services.AddScoped<CheckIfEventIdRegisteredActionFilter>();
            builder.Services.AddScoped<CheckIfEventIdRegisteredActionFilter>();
            builder.Services.AddScoped<CheckIfEventExistsActionFilter>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}