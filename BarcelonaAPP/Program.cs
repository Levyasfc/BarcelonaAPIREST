using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Configura los servicios para la aplicación web
builder.Services.AddControllersWithViews();

// Registra HttpClient para consumir la API
// Asegúrate de que esta URL coincida con la URL de tu proyecto de API
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7176/");
});

var app = builder.Build();

// Configura el pipeline HTTP para la aplicación web
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Aplicacion}/{action=Index}/{id?}");

app.Run();