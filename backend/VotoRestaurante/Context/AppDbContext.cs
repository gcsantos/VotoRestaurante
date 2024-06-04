using Microsoft.EntityFrameworkCore;
using VotoRestaurante.Models;

namespace VotoRestaurante.Context;

public class AppDbContext : DbContext {

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=D:\\Projetos\\VotoRestaurante\\banco\\VotoRestauranteDb.db");
    }


    public DbSet<Restaurante> Restaurantes { get; set; }
    public DbSet<Voto> Votos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurante>().HasData(
            new Restaurante 
            {
                Id = 1,
                Nome = "Brabus",
                Participa = false
            },
            new Restaurante
            {
                Id = 2,
                Nome = "Velho Chico",
                Participa = false
            },
            new Restaurante
            {
                Id = 3,
                Nome = "Bar 29",
                Participa = false
            },
            new Restaurante
            {
                Id = 4,
                Nome = "Hakuma Batata",
                Participa = false
            },
            new Restaurante
            {
                Id = 5,
                Nome = "Bar do Peixe",
                Participa = false
            }
            );
    }


}
