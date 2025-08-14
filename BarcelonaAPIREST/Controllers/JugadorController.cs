using BarcelonaAPIREST.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BarcelonaAPIREST.Controllers
{
    public class JugadorController : Controller
    {
        private readonly HttpClient _httpClient;

        public JugadorController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/jugadores");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jugadores = JsonSerializer.Deserialize<List<Jugador>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Pasa la lista a la vista
                return View(jugadores);
            }

            // Si la llamada falla, devuelve una vista de error
            return View("Error");
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Jugador jugador)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("api/AgregarJugadores", jugador);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error al crear el jugador.");
                }
            }
            return View(jugador);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var response = await _httpClient.GetAsync($"api/Jugadores/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jugador = JsonSerializer.Deserialize<Jugador>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(jugador);
            }
            return NotFound();
        }

        [HttpPost]

        public async Task<IActionResult> Editar(int id, Jugador jugador)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"api/ActualizarJugadores/{id}", jugador);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error al actualizar el jugador.");
                }
            }
            return View(jugador);
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var response = await _httpClient.GetAsync($"api/Jugadores/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jugador = JsonSerializer.Deserialize<Jugador>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(jugador);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Eliminar")]

        public async Task<IActionResult> ConfirmarEliminar(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/EliminarJugadores/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return BadRequest("Error al eliminar el jugador.");

        }
    }
}
