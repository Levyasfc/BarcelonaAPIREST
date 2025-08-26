using BarcelonaAPIREST.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System.Net.Http.Json; 
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

    public IActionResult Historia()
    {
        return View();
    }

    public async Task<IActionResult> Jugadores()
    {

        var jugadores = new List<JugadorDTO>();


        var response = await _httpClient.GetAsync("api/jugadores");

        if (response.IsSuccessStatusCode)
        {
            jugadores = await response.Content.ReadFromJsonAsync<List<JugadorDTO>>();
        }
        else
        {

            return View("Error");
        }


        return View(jugadores);
    }

    public async Task<IActionResult> GestionJugadores()
    {
        var jugadores = new List<JugadorDTO>();
        var response = await _httpClient.GetAsync("api/jugadores");

        if (response.IsSuccessStatusCode)
        {
            jugadores = await response.Content.ReadFromJsonAsync<List<JugadorDTO>>();
            return View(jugadores);
        }
        else
        {

            return View("Error");
        }
    }

    #region GESTION JUGADORES

    [HttpGet]
    public async Task<IActionResult> Crear()
    {
        ViewBag.Equipos = await GetEquipos();
        ViewBag.Posiciones = GetPosiciones();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(JugadorDTO jugador)
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.PostAsJsonAsync("api/AgregarJugadores", jugador);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GestionJugadores));
            }
            ModelState.AddModelError("", "Error al crear el jugador.");
        }
        return View(jugador);
    }

    public async Task<IActionResult> EditarJugador(int id)
    {
        var response = await _httpClient.GetAsync($"api/{id}");
        if (response.IsSuccessStatusCode)
        {
            var jugador = await response.Content.ReadFromJsonAsync<JugadorDTO>();
            if (jugador != null)
            {
                ViewBag.Posiciones = GetPosiciones();
                return View(jugador);
            }
        }
        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditarJugador(JugadorDTO jugador)
    {
        if (ModelState.IsValid)
        {
            var updateDto = new JugadorUpdateDto
            {
                Dorsal = jugador.Dorsal,
                Name = jugador.Name,
                Posicion = jugador.Posicion,
                Foto = jugador.Foto
            };

            var response = await _httpClient.PutAsJsonAsync($"api/ActualizarJugadores/{jugador.Id}", updateDto);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GestionJugadores));
            }
            ModelState.AddModelError("", "Error al editar el jugador.");
        }
        return View(jugador);
    }

    [HttpPost]
    [ActionName("EliminarJugador")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EliminarJugadorConfirmado(int dorsal)
    {

        var response = await _httpClient.PostAsync($"api/EliminarJugadores/{dorsal}", null);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(GestionJugadores));
        }

        return RedirectToAction(nameof(GestionJugadores));
    }

    #endregion


    private List<SelectListItem> GetPosiciones()
    {
        return new List<SelectListItem>
    {
        new SelectListItem { Value = "DC", Text = "DelanteroCentro" },
        new SelectListItem { Value = "EI", Text = "ExtremoIzquierdo" },
        new SelectListItem { Value = "ED", Text = "ExtremoDerecho" },
        new SelectListItem { Value = "MCO", Text = "MediocampistaOfensivo" },
        new SelectListItem { Value = "MC", Text = "Mediocapista" },
        new SelectListItem { Value = "LI", Text = "LaterialIzquierdo" },
        new SelectListItem { Value = "LD", Text = "LateralDerecho" },
        new SelectListItem { Value = "POR", Text = "Portero" },
        new SelectListItem { Value = "DFC", Text = "Defensa" }
    };
    }

    private async Task<List<SelectListItem>> GetEquipos()
    {
        var response = await _httpClient.GetAsync("api/equipos");
        if (response.IsSuccessStatusCode)
        {
            var equipos = await response.Content.ReadFromJsonAsync<List<EquipoDTO>>();
            if (equipos != null)
            {
                return equipos.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name 
                }).ToList();
            }
        }
        return new List<SelectListItem>();
    }
}