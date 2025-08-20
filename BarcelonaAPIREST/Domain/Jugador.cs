namespace BarcelonaAPIREST.Domain
{
    public class Jugador
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public string Posicion {  get; set; }

        public string? Foto { get; set; } 

        public int EquipoId { get; set; } // Relacion de uno a muchos, un jugador puede pertenecer a un equipo

        public Equipo Equipo { get; set; } 
    }
}
