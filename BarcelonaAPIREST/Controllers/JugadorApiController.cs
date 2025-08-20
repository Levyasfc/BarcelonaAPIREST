    using Microsoft.AspNetCore.Mvc;
    using BarcelonaAPIREST.Dal;
    using BarcelonaAPIREST.Domain;
    using BarcelonaAPIREST.DTOs;
    using Microsoft.EntityFrameworkCore;

namespace BarcelonaAPIREST.Controllers
    {

        [ApiController]
        [Route("api")]
        public class JugadorApiController : ControllerBase 
        {
            private readonly SglDbContext dbContext;
            public JugadorApiController(SglDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            [HttpGet("jugadores")] // Devuelve todos los jugadores que esten el la DB
        public async Task<ActionResult<IEnumerable<JugadorDTO>>> GetAllJugadores()
        {
            // 1. Obtiene los datos del jugador E incluyendo su equipo
            var alljugadores = await dbContext
                                        .Jugadors
                                        .Include(j => j.Equipo)
                                        .ToListAsync();

            // 2. Mapea la entidad (con el ciclo) a un DTO (sin el ciclo)
            var jugadoresDto = alljugadores.Select(j => new JugadorDTO
            {
                Id = j.Id,
                Name = j.Name,
                Posicion = j.Posicion,
                NombreEquipo = j.Equipo?.Name, // Aquí se accede al nombre del equipo
                Foto = j.Foto
            }).ToList();

            // 3. Devuelve el DTO, que el serializador puede manejar sin problemas
            return Ok(jugadoresDto);
        }

        [HttpGet("Jugadores/{id}")]  // Uso de ActionResult, CRUDO, Lo que hace es mostrar que es lo que 
                                         // trearia al realizar la consulta y de donde...
            public async Task<ActionResult<IEnumerable<Jugador>>> GetJugadorPorId(int id)
            {
                var IdJugador = await dbContext.Jugadors.Where(b => b.Id == id).ToListAsync();
                return IdJugador.Any() ? Ok(IdJugador) : NotFound(id);
            }


            // Metodo Post

            // Añadir Jugadores a la DB

            [HttpPost("AgregarJugadores")]

            public async Task<IActionResult> CreateJugador(Jugador jugador)
            {
                var newJugador = new Domain.Jugador()
                {
                    Name = jugador.Name,
                    Posicion = jugador.Posicion, 
                    EquipoId = jugador.EquipoId 
                };
                dbContext.Jugadors.Add(newJugador);
                var resultado = await dbContext.SaveChangesAsync();
                return resultado == 1 ? Ok(newJugador.Id) : BadRequest();
            }

            // Funcion de UPDATE es decir el PUT

            [HttpPut("ActualizarJugadores/{id}")]

            public async Task<IActionResult> UpdateJugador(int id, Jugador jugador)
            {
                var actjugador = dbContext.Jugadors.First(b => b.Id == id);
                actjugador.Name = jugador.Name;
                actjugador.Posicion = jugador.Posicion;
                actjugador.EquipoId = jugador.EquipoId; // Asegurarse de que el EquipoId se actualice correctamente
            var result = await dbContext.SaveChangesAsync();
                return result == 1 ? Ok() : BadRequest();
            }

            // Funcion de DELETE, Eliminar Jugadores de la DB
            [HttpDelete("EliminarJugadores/{id}")]
            public async Task<IActionResult> DeleteJugador(int id)
            {
                var jugador = await dbContext.Jugadors.FindAsync(id);
                if (jugador == null)
                    return NotFound();

                dbContext.Jugadors.Remove(jugador);
                var result = await dbContext.SaveChangesAsync();

                return result == 1 ? Ok() : BadRequest();
            }






        }
}
