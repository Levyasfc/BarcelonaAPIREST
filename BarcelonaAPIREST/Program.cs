using BarcelonaAPIREST.Dal;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configura los servicios para una API pura.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Configura Swagger para la documentación de la API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura la base de datos.
builder.Services.AddDbContext<SglDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionJugadoresDB")));

var app = builder.Build();

// Configura el pipeline para la API.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers(); // Esto mapea tus controladores de API.

// Crea la base de datos si no existe.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SglDbContext>();
    db.Database.EnsureCreated();
}

app.Run();