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

    [HttpPost]
    public async Task<IActionResult> Crear(JugadorDTO jugador, IFormFile FotoFile)
    {
        using (var content = new MultipartFormDataContent())
        {
            // Agrega los datos del jugador como contenido
            content.Add(new StringContent(jugador.Name), "Name");
            content.Add(new StringContent(jugador.Posicion), "Posicion");
            content.Add(new StringContent(jugador.NombreEquipo), "NombreEquipo");

            // Agrega el archivo de la foto
            if (FotoFile != null && FotoFile.Length > 0)
            {
                var streamContent = new StreamContent(FotoFile.OpenReadStream());
                content.Add(streamContent, "FotoFile", FotoFile.FileName);
            }

            var response = await _httpClient.PostAsync("api/JugadorApi/Crear", content); // Ajusta la URL de tu API
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            // Manejar errores
        }
        return View(jugador);
    }


}