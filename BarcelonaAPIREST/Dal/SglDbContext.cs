using BarcelonaAPIREST.Domain;
using Microsoft.EntityFrameworkCore;

namespace BarcelonaAPIREST.Dal
{
    public class SglDbContext: DbContext
    {
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Jugador> Jugadors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Sgl");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipo>().HasData(
                new Equipo() { Id = 1, Name = "Barcelona" },
                new Equipo() { Id = 2, Name = "Getafe" },
                new Equipo() { Id = 3, Name = "Milan" }
                );

            modelBuilder.Entity<Jugador>().HasData(
                new Jugador() { Id = 1, Name = "LamineYamal", Posicion = "ED"},
                new Jugador() { Id = 2, Name = "GonzaloGarcia", Posicion = "DC" },
                new Jugador() { Id = 3, Name = "RafaelLeao", Posicion = "EI" }
                );
        }
    }
}
