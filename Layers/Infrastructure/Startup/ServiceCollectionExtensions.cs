using AVIS.CoreBase.Middleware;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication;
using AVIS.CoreBase.Interfaces;
using AVIS.CoreBase6.Middleware;

//Dependencia Arquitectura
using Avis.Catalogo.Domain;
using Avis.Catalogo.Application;

namespace Avis.Catalogo.Infrastructure
{
    /// <summary>
    /// Extension para definir la inyeccion de dependencia del proyecto
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var info = new OpenApiInfo();
                info.Title = "DEMO CITAS";
                info.Version = "V3";
                info.Contact = new OpenApiContact
                {
                    Name = "Area TI Grupo Gomex",
                    Email = "desarrollo@avis.com.mx",
                };
                info.License = new OpenApiLicense
                {
                    Name = "Licencia de uso.",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.txt"),
                };
                c.SwaggerDoc("v3", info);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
               {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                });


                //El nombre por default es el nombre del ensamblado con la extension XML
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //Le indicamos a Swagger que incluya ese archivo
                c.IncludeXmlComments(xmlPath);
            });

            services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, SwagBasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddScoped<ISwagUserService, SwagUserService>();

            return services;
        }

        public static IServiceCollection AddDapper(this IServiceCollection services, IConfiguration configuration)
        {
            string cnnString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped((s) => new SqlConnection(cnnString));

            //Define un objeto de transaccion para dapper
            services.AddScoped<IDbTransaction>(s =>
            {
                SqlConnection conn = s.GetRequiredService<SqlConnection>();
                conn.Open();
                return conn.BeginTransaction();
            });

            //Define la unidad de trabajo para la transaccion
            services.AddScoped<IDapperUnitofWork, DapperUnitofWork>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Declaracion de inyeccion de dépedencias
            services.AddScoped<IAutoService, AutoService>();

            services.AddScoped<IAutoAggregate, AutoAggregate>();

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            //Declaracion de validadores
            services.AddScoped<IValidator<AutoDTO>, AutoDTOValidator>();
            return services;
        }







    }
}
