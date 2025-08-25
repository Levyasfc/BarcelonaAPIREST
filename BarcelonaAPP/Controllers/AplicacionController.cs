using Microsoft.AspNetCore.Mvc;
using BarcelonaAPIREST.DTOs;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class AplicacionController : Controller
{
    private readonly HttpClient _httpClient;

    public AplicacionController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("api/jugadores");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var jugadores = JsonSerializer.Deserialize<List<JugadorDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(jugadores);
        }
        return View(new List<JugadorDTO>());
    }

    public async Task<IActionResult> Crear(JugadorDTO jugador)
    {
        if (ModelState.IsValid)
        {
            
            var response = await _httpClient.PostAsJsonAsync("api/AgregarJugadores", jugador);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error al crear el jugador.");
        }
        return View(jugador);
    }


}