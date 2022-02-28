using Microsoft.EntityFrameworkCore;
using Movie_Character_EF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Character_EF.Models
{
    class MovieDbContext : DbContext
    {
        public DbSet<Character> Character { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Franchise> Franchise { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = CEREBRO; Initial Catalog = MovieDb; Integrated Security = true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().HasData(SeedHelper.GetCharacterSeeds());
            modelBuilder.Entity<Movie>().HasData(SeedHelper.GetMovieSeeds());
            modelBuilder.Entity<Franchise>().HasData(SeedHelper.GetFranchiseSeeds());
        }
    }
}
