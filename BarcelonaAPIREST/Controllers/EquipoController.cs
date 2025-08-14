using BarcelonaAPIREST.Dal;
using BarcelonaAPIREST.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarcelonaAPIREST.Controllers
{

    /*
     *  Espacio de CRUD de la API de Equipos de futbol
     * 
     * Busquedas por Todos los equipos o Todos los Jugadores
     * 
     * Falta asignar Equipo por jugador, Claves foraneas
     */

    [ApiController]
    [Route("api")]


    public class EquipoController : ControllerBase
    {
        private readonly SglDbContext dbContext;

        public EquipoController(SglDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        // Metodos solo de cada tipo de controller

        [HttpGet("equipos")]

        public async Task<IActionResult> GetAllEquipos()
        {
            var allequipos = await dbContext
                                    .Equipos
                                    .ToListAsync();
            return Ok(allequipos);
        }

        [HttpGet("equipos/{id}")] // Busca Equipos por ID

        public async Task<IActionResult> GetTeamForId(int id)
        {
            var idEquipo = await dbContext.Equipos  // Importante aqui se seleciona cual de
                                                    // las tablas de la DB se ba a hacer la consulta
                                                    // del crud, tener en cuenta que esta GET DELETE o editar
                .Where(b => b.Id == id)  // Funcion "Flecha" para la identificacion de ID en la busqueda
                .ToListAsync();
            return idEquipo.Any() ? Ok(idEquipo) : NotFound(id); // Esta linea es el return de datos, La que valida que si existan datos en la DB
        }


        // Metodo Post

        [HttpPost("AgragarEquipo")]

        public async Task<IActionResult> NewEquipo(Equipo equipo)
        {
            var newEquipo = new Domain.Equipo()
            {
                Name = equipo.Name
            };
            dbContext.Equipos.Add(newEquipo);
            var result = await dbContext.SaveChangesAsync();
            return result == 1 ? Ok(newEquipo) : BadRequest();

        }

        //Metodo PUT

        [HttpPut("ActualizarEquipos/{Name}")]

        public async Task<IActionResult> UpdateEquipo(string Name , Equipo equipo)
        {
            var actequipo = dbContext.Equipos.First(b => b.Name == Name);
            actequipo.Name = equipo.Name;
            var result = await dbContext.SaveChangesAsync();
            return result == 1 ? Ok() : BadRequest();
        }


    }
}
