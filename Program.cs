using Avis.CoreBase.Middleware;
using AVIS.CoreBase.Middleware;
using FluentValidation;
using Serilog;

using Avis.Catalogo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json")
    .AddEnvironmentVariables()
    .Build();


builder.Host.AddSerilog();
//carpeta de startup

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAvisCoreBaseMin(configuration);
builder.Services.AddDapper(configuration);
builder.Services.AddServices();
builder.Services.AddValidators();
builder.Services.AddApiVersioning();
builder.Services.AddSwagger();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseStaticFiles();
app.MapSwagger();
app.UseHttpsRedirection();
// app.UseAuthentication();
// app.UseAuthorization();
app.AddRoutes();


//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();



#region AREA DEL PROGRAMA
try
{
    Log.Information("Inicia el demo");
    app.Run();
    return 0;
}
catch (Exception e)
{

    Log.Fatal(e, "Hubo un error");
    return 1;
}
finally
{
    Log.Information("Saliendo del demo");
    Log.CloseAndFlush();
}
#endregion