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
            
            var alljugadores = await dbContext
                                        .Jugadors
                                        .Include(j => j.Equipo)
                                        .ToListAsync();

            
            var jugadoresDto = alljugadores.Select(j => new JugadorDTO
            {
                Id = j.Id,
                Dorsal = j.Dorsal,
                Name = j.Name,
                Posicion = j.Posicion,
                NombreEquipo = j.Equipo?.Name, 
                Foto = j.Foto
            }).ToList();

            
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

        [HttpPut("ActualizarJugadores/{id}")]
        public async Task<IActionResult> UpdateJugador(int id, [FromBody] JugadorUpdateDto jugadorDto)
        {
           
            var actjugador = await dbContext.Jugadors.FirstOrDefaultAsync(b => b.Id == id);

            if (actjugador == null)
            {
                return NotFound($"No se encontró al jugador con el ID {id}.");
            }

            
            actjugador.Name = jugadorDto.Name;
            actjugador.Posicion = jugadorDto.Posicion;
            actjugador.Dorsal = jugadorDto.Dorsal;
            actjugador.Foto = jugadorDto.Foto;

            await dbContext.SaveChangesAsync();

            return NoContent();
        }



        // Funcion de DELETE, Eliminar Jugadores de la DB
        [HttpDelete("EliminarJugadores/{dorsal}")] 
        public async Task<IActionResult> DeleteJugador(int dorsal)
        {
            var jugador = await dbContext.Jugadors.FirstOrDefaultAsync(j => j.Dorsal == dorsal);

            if (jugador == null)
                return NotFound();

            dbContext.Jugadors.Remove(jugador);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("EliminarJugadores/{dorsal}")] 
        public async Task<IActionResult> DeleteJugadorConPost(int dorsal)
        {
            var jugador = await dbContext.Jugadors.FirstOrDefaultAsync(j => j.Dorsal == dorsal);

            if (jugador == null)
                return NotFound();

            dbContext.Jugadors.Remove(jugador);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

    }
}
