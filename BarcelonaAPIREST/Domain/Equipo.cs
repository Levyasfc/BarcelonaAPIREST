namespace BarcelonaAPIREST.Domain
{
    public class Equipo
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public string? Escudo { get; set; } // URL de la imagen del escudo del equipo

        public ICollection<Jugador>? Jugadors { get; set; } // Relacion de uno a muchos, un equipo puede tener varios jugadores
    }
}
