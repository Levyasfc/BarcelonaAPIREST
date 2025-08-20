namespace BarcelonaAPIREST.Domain
{
    public class Equipo
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<Jugador>? Jugadors { get; set; } // Relacion de uno a muchos, un equipo puede tener varios jugadores
    }
}
