using Microsoft.EntityFrameworkCore;
using N5Now.Core.Interfaces;
using N5Now.Core.Interfaces.Services;
using N5Now.Infrastructure.DataBaseIttion;
using N5Now.Infrastructure.Interfaces;
using N5Now.Infrastructure.Interfaces.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Configuracion: Agrego explicitamente lo siguiente
//(Esto ya está incluido por defecto en el template de ASP.NET Core)
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();
//FIN: Configuracion: Agrego explicitamente lo siguiente
//(Esto ya está incluido por defecto en el template de ASP.NET Core)

#region ENTITY FRAMEWORK
//Agregar DbContext al contenedor de servicios
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion ENTITY FRAMEWORK

/* Logs Levels
┌──────────────┐
│   Fatal      │ ← nivel más alto (más importante)
│   Error      │
│   Warning    │
│   Info       │ ← valor típico en producción
│   Debug      │
│   Verbose    │ ← nivel más bajo (más detallado)
└──────────────┘
*/
// Configurar Serilog con appsettings
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();
// FIN Configurar Serilog con appsettings

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Here Adds Customers Services
builder.Services.AddHttpClient();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
//END of Here Adds Customers Services

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
