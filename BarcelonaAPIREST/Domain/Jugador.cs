namespace BarcelonaAPIREST.Domain
{
    public class Jugador
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public string Posicion {  get; set; }
    }
}
