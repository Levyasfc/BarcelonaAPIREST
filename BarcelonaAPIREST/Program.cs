using BarcelonaAPIREST.Dal;

var builder = WebApplication.CreateBuilder(args);

// Aquí mantienes solo los servicios necesarios para MVC y la API
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SglDbContext>();

// Registrar HttpClient para consumir la API interna
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7176/"); // Cambia al puerto HTTPS que uses
});

var app = builder.Build();

// Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Solo el enrutamiento para MVC.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PaginaPrincipal}/{action=Index}/{id?}");

// Crear DB si no existe
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SglDbContext>();
    db.Database.EnsureCreated();
}

app.Run();