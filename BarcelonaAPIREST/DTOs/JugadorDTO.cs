namespace BarcelonaAPIREST.DTOs
{
    public class JugadorDTO
    {
        public int Id { get; set; }   // 👈 aquí sí se incluye
        public int Dorsal { get; set; }
        public string Name { get; set; }
        public string Posicion { get; set; }
        public string Foto { get; set; }
        public string NombreEquipo { get; set; }
    }

    public class JugadorCreateDto
    {
        public int Dorsal { get; set; }
        public string Name { get; set; }
        public string Posicion { get; set; }
        public string Foto { get; set; }
        public int EquipoId { get; set; }
    }

    public class JugadorUpdateDto
    {
        public int Dorsal { get; set; }
        public string Name { get; set; }
        public string Posicion { get; set; }
    }
}
