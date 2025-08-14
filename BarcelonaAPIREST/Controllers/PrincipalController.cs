using Microsoft.AspNetCore.Mvc;
using BarcelonaAPIREST.Domain;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;

public class PrincipalController : Controller
{
    private readonly HttpClient _httpClient;

    public PrincipalController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }


    #region BuscarJugadores
    public async Task<IActionResult> Index()
    {

        // Peticion GET a la API de JUGADORES

        var jugadoresRespuesta = await _httpClient.GetFromJsonAsync<List<Jugador>>("api/jugadores");
        return View(jugadoresRespuesta ?? new List<Jugador>());

    }

    public async Task<IActionResult> Detalles(int Nombre)
    {
        var jugador = await _httpClient.GetFromJsonAsync<Jugador>($"api/jugadores/{Nombre}");
        if (jugador == null)
        {
            return NotFound("El Jugador no esta registrado");
        }
        return View(jugador);
    }

    #endregion

    #region AgregarJugadores

    // POST PARA AGREGAR JUGADORES
    public async Task<IActionResult> AgregarJugador(Jugador jugador)
    {
        var response = await _httpClient.PostAsJsonAsync("api/AgregarJugadores", jugador);
        if (response.IsSuccessStatusCode)
            return RedirectToAction(nameof(Index));

        ModelState.AddModelError(string.Empty, "Error al agregar el jugador");
        return View(jugador);
    }

    #endregion

    #region EdicionJugadores

    // GET: Editar jugador
    public async Task<IActionResult> Edit(int id)
    {
        var jugador = await _httpClient.GetFromJsonAsync<Jugador>($"api/jugadores/{id}");
        if (jugador == null) return NotFound();
        return View(jugador);
    }

    //POST PARA EDITAR JUGADORES

    public async Task<IActionResult> EditarJugador(int id, Jugador jugador)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/ActualizarJugadores/{id}", jugador);
        if (response.IsSuccessStatusCode)
            return RedirectToAction(nameof(Index));
        ModelState.AddModelError(string.Empty, "Error al actualizar el jugador");
        return View(jugador);
    }

    #endregion

    #region EliminarJugadores
    // GET: Eliminar jugador
    public async Task<IActionResult> Delete(int id)
    {
        var jugador = await _httpClient.GetFromJsonAsync<Jugador>($"api/jugadores/{id}");
        if (jugador == null) return NotFound();
        return View(jugador);
    }

    // POST: Confirmar eliminación
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Jugadores/{id}");
        if (response.IsSuccessStatusCode)
            return RedirectToAction(nameof(Index));

        ModelState.AddModelError("", "No se pudo eliminar el jugador");
        return RedirectToAction(nameof(Index));
    }
    #endregion
}
