using Microsoft.AspNetCore.Mvc;

namespace BarcelonaAPIREST.DTOs
{
    public class EquipoDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Escudo { get; set; } // URL de la imagen del escudo del equipo
        

    }
}
