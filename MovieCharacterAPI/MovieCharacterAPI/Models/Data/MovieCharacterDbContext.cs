using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Models.Domain;
using System.Collections.Generic;

namespace MovieCharacterAPI.Models.Data
{
    public class MovieCharacterDbContext : DbContext
    {
        public MovieCharacterDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Character> Character { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Franchise> Franchise { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Franchise>().HasData(SeedHelper.GetFranchiseSeeds());
            modelBuilder.Entity<Character>().HasData(SeedHelper.GetCharacterSeeds());
            modelBuilder.Entity<Movie>().HasData(SeedHelper.GetMovieSeeds());

            modelBuilder.Entity<Character>() // TODO: put in method in seedhelper class
                .HasMany(p => p.Movies)
                .WithMany(m => m.Characters)
                .UsingEntity<Dictionary<string, object>>(
                "CharacterMovie",
                r => r.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
                l => l.HasOne<Character>().WithMany().HasForeignKey("CharacterId"),
                je =>
                {
                    je.HasKey("CharacterId", "MovieId");
                    je.HasData(
                        new { CharacterId = 1, MovieId = 2 },
                        new { CharacterId = 2, MovieId = 1 },
                        new { CharacterId = 3, MovieId = 1 },
                        new { CharacterId = 4, MovieId = 1 },
                        new { CharacterId = 5, MovieId = 3 }
                        );
                });
        }
    }
}
