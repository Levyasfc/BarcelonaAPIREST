using BarcelonaAPIREST.Domain;
using Microsoft.EntityFrameworkCore;

namespace BarcelonaAPIREST.Dal
{
    public class SglDbContext : DbContext
    {
        public SglDbContext(DbContextOptions<SglDbContext> options) : base(options)
        {
        }

        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Jugador> Jugadors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipo>().HasData(
                new Equipo() { Id = 1, Name = "Barcelona" },
                new Equipo() { Id = 2, Name = "RealMadrid" },
                new Equipo() { Id = 3, Name = "Milan" }
            );

            modelBuilder.Entity<Jugador>().HasData(
                new Jugador() { Id = 1, Dorsal = 10, Name = "LamineYamal", Posicion = "ED", EquipoId = 1, Foto = "https://www.fcbarcelona.com/photo-resources/2025/07/09/47ddeac8-9a36-4618-af27-5b1310184370/19-Lamine.jpg?width=1200&height=750" },
                new Jugador() { Id = 2, Dorsal = 14, Name = "MarcusRashford", Posicion = "EI", EquipoId = 1, Foto = "https://www.fcbarcelona.com/photo-resources/2025/07/28/75f02288-6747-4d91-8a1f-fcbd3857fd1a/Fitxa-14-Blau-Rashford.jpg?width=1680&height=1050" },
                new Jugador() { Id = 3, Dorsal = 9, Name = "RobertLewandoski", Posicion = "DC", EquipoId = 1, Foto = "https://www.fcbarcelona.com/photo-resources/2025/07/09/e8378527-14fe-4dc7-a0c2-e3cea291a6e2/09-Lewandowski.jpg?width=1680&height=1050" },
                new Jugador() { Id = 4, Dorsal = 11, Name = "Raphinha", Posicion = "EI", EquipoId = 1, Foto = "https://www.fcbarcelona.com/photo-resources/2025/07/09/87585699-3a8f-41d4-9912-e63f569f6fd6/11-Raphinha.jpg?width=1680&height=1050" },
                new Jugador() { Id = 5, Dorsal = 13, Name = "JoanGarcia", Posicion = "POR", EquipoId = 1, Foto = "https://www.fcbarcelona.com/photo-resources/2025/07/09/3337817d-b39f-4dfa-924b-89994d15eee1/00-Joan_Garcia.jpg?width=1680&height=1050" }
            );
        }
    }
}