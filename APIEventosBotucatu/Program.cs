using APIEventosBotucatu.Core.Interfaces;
using APIEventosBotucatu.Core.Services;
using APIEventosBotucatu.Filters;
using APIEventosBotucatu.Infra.Data;
using APIEventosBotucatu.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace APIEventosBotucatu
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("PolicyCors",
                    policy =>
                    {
                        policy.WithOrigins("https://localhost:7179")
                               .WithOrigins("http://127.0.0.1:5173")
                               .WithOrigins("https://eventos-botucatu-sergio.netlify.app")
                              .WithMethods("GET", "POST", "PUT", "DELETE");
                        policy.AllowAnyHeader();
                    });
            });

            builder.Services.AddControllers();

            var key = Encoding.ASCII.GetBytes(builder.Configuration["secretKey"]);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = "APIClientes.com",
                        ValidAudience = "APIEvents.com"
                    };
                });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Autenticação baseada em Json Web Token (JWT). Entrar SOMENTE com o token no campo abaixo."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
            });

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
            builder.Services.AddScoped<CheckReservationExistsActionFilter>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("PolicyCors");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}