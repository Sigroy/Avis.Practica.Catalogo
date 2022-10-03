using AVIS.CoreBase.Middleware;
using Serilog;

//Referencias arquitectura
using Avis.Catalogo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

builder.Host.AddSerilog();

builder.Services.AddControllers();
builder.Services.AddAvisCoreBaseMin(configuration);
builder.Services.AddDapper(configuration);
builder.Services.AddServices();
builder.Services.AddValidators();
builder.Services.AddApiVersioning();
builder.Services.AddSwagger();

var app = builder.Build();

app.UseStaticFiles();
app.MapSwagger();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.AddRoutes();

#region ÁREA DEL PROGRAMA

try
{
    Log.Information("Inicio práctica de API de catálogo");

    app.Run();

    return 0;
}
catch (Exception e)
{
    Log.Fatal(e, "Práctica catálogo terminó inesperadamente");
    return 1;
}
finally
{
    Log.Information("Saliendo de práctica catálogo");
    Log.CloseAndFlush();
}

#endregion