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
                Dorsal = j.Dorsal,
                Name = j.Name,
                Posicion = j.Posicion,
                NombreEquipo = j.Equipo?.Name, // Aquí se accede al nombre del equipo
                Foto = j.Foto
            }).ToList();

            // 3. Devuelve el DTO, que el serializador puede manejar sin problemas
            return Ok(jugadoresDto);
            }

        // Obtener jugador por ID

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJugadorById(int id)
        {
            var jugador = await dbContext.Jugadors.FindAsync(id);

            if (jugador == null)
                return NotFound();

            var jugadorDto = new JugadorDTO
            {
                Id = jugador.Id,
                Name = jugador.Name,
                Posicion = jugador.Posicion,
                Dorsal = jugador.Dorsal,
                Foto = jugador.Foto,
                NombreEquipo = jugador.Equipo?.Name
            };

            return Ok(jugadorDto);
        }



        // Obtener jugadores por el dorsal

        [HttpGet("Jugadores/{dorsal}")]
        public async Task<ActionResult<JugadorDTO>> GetJugadorPorId(int dorsal)
        {
            var jugador = await dbContext.Jugadors
                                         .Include(j => j.Equipo)
                                         .FirstOrDefaultAsync(j => j.Dorsal == dorsal);

            if (jugador == null)
            {
                return NotFound();
            }

            // Mapear la entidad a un DTO
            var jugadorDto = new JugadorDTO
            {
                Dorsal = jugador.Dorsal,
                Name = jugador.Name,
                Posicion = jugador.Posicion,
                NombreEquipo = jugador.Equipo?.Name,
                Foto = jugador.Foto
            };

            return Ok(jugadorDto);
        }


        // Metodo Post

        // Añadir Jugadores a la DB

        [HttpPost("AgregarJugadores")]
        public async Task<IActionResult> CreateJugador([FromBody] JugadorCreateDto jugadorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newJugador = new Domain.Jugador()
            {
                Name = jugadorDto.Name,
                Posicion = jugadorDto.Posicion,
                Dorsal = jugadorDto.Dorsal,
                Foto = jugadorDto.Foto,
                EquipoId = jugadorDto.EquipoId
            };

            dbContext.Jugadors.Add(newJugador);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJugadorById), new { id = newJugador.Id }, newJugador);
        }

        // Funcion de UPDATE es decir el PUT

        [HttpPut("ActualizarJugadores/{dorsal}")]
        public async Task<IActionResult> UpdateJugador(int dorsal, [FromBody] JugadorUpdateDto jugadorDto)
        {
            
            var actjugador = await dbContext.Jugadors.FirstOrDefaultAsync(b => b.Dorsal == dorsal);

            
            if (actjugador == null)
            {
                return NotFound($"No se encontró al jugador con el dorsal {dorsal}.");
            }

            // 3. Mapea los datos del DTO a la entidad
            actjugador.Name = jugadorDto.Name;
            actjugador.Posicion = jugadorDto.Posicion;
            actjugador.Dorsal = jugadorDto.Dorsal;

           
            await dbContext.SaveChangesAsync();

            
            return NoContent();
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
