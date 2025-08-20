using BarcelonaAPIREST.Domain;
using Microsoft.EntityFrameworkCore;

namespace BarcelonaAPIREST.Dal
{
    public class SglDbContext : DbContext
    {
        // Se mantiene solo el constructor para la inyección de dependencias.
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
                new Jugador() { Id = 1, Name = "LamineYamal", Posicion = "ED", EquipoId = 1 },
                new Jugador() { Id = 2, Name = "GonzaloGarcia", Posicion = "DC", EquipoId = 2 },
                new Jugador() { Id = 3, Name = "RafaelLeao", Posicion = "EI", EquipoId = 3 }
            );
        }
    }
}