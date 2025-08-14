    using Microsoft.AspNetCore.Mvc;
    using BarcelonaAPIREST.Dal;
    using BarcelonaAPIREST.Domain;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

namespace BarcelonaAPIREST.Controllers
    {

        [ApiController]
        [Route("api")]
        public class JugadorController : ControllerBase 
        {
            private readonly SglDbContext dbContext;
            public JugadorController(SglDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            [HttpGet("jugadores")] // Devuelve todos los jugadores que esten el la DB
            public async Task<IActionResult> GetAllJugadores()
            {
                var alljugadores = await dbContext
                                            .Jugadors
                                            .ToListAsync();
                return Ok(alljugadores);
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
                    Posicion = jugador.Posicion
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
