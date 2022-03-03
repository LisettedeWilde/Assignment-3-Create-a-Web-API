using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApp.Helpers;
using MovieApp.Models;

namespace MovieApp.Data
{
    public class NolanverseDbContext : DbContext
    {        
        /// <summary>
        /// Gets or sets movies, characters and franchises.
        /// </summary>
        
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Franchise> Franchises { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(ConnectionStringHelper.GetConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Movies
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 1, Title = "Batman Begins", Genre = "Action", ReleaseYear = 2005, Director = "Christopher Nolan", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 2, Title = "The Dark Knight", Genre = "Action", ReleaseYear = 2008, Director = "Christopher Nolan", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 3, Title = "The Dark Knight Rises", Genre = "Action", ReleaseYear = 2012, Director = "Christopher Nolan", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 4, Title = "The Fellowship of the Ring", Genre = "Fantasy", ReleaseYear = 2001, Director = "Peter Jackson", PictureUrl = "", Trailer = "", FranchiseId = 2 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 5, Title = "The Two Towers", Genre = "Fantasy", ReleaseYear = 2003, Director = "Peter Jackson", PictureUrl = "", Trailer = "", FranchiseId = 2 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 6, Title = "The Return of the King", Genre = "Fantasy", ReleaseYear = 2005, Director = "Peter Jackson", PictureUrl = "", Trailer = "", FranchiseId = 2 });

            //Characters
            modelBuilder.Entity<Character>().HasData(new Character() { Id = 1, FullName = "Batman", Alias = "Human", Gender = "Male", PictureUrl = "https://static.wikia.nocookie.net/batman/images/8/8f/Christian_Bale_as_The_Dark_Knight.jpg/revision/latest/scale-to-width-down/967?cb=20210312234853" });
            modelBuilder.Entity<Character>().HasData(new Character() { Id = 2, FullName = "Commissioner Gordon", Alias = "Human", Gender = "Male", PictureUrl = "https://static.wikia.nocookie.net/batman/images/c/ca/Batman_photos_oldman.jpg/revision/latest/scale-to-width-down/300?cb=20080321214518" });
            modelBuilder.Entity<Character>().HasData(new Character() { Id = 3, FullName = "Bane", Alias = "Human", Gender = "Male", PictureUrl = "https://static.wikia.nocookie.net/batman/images/f/f0/Bane_TDKR3.jpg/revision/latest/scale-to-width-down/470?cb=20120511112335" });
            modelBuilder.Entity<Character>().HasData(new Character() { Id = 4, FullName = "Frodo Baggins", Alias = "Hobbit", Gender = "Male", PictureUrl = "https://static.wikia.nocookie.net/lotr/images/1/1a/FotR_-_Elijah_Wood_as_Frodo.png/revision/latest/scale-to-width-down/1000?cb=20130313174543" });
            modelBuilder.Entity<Character>().HasData(new Character() { Id = 5, FullName = "Saruman the White", Alias = "Maiar", Gender = "Male", PictureUrl = "https://static.wikia.nocookie.net/lotr/images/0/0c/Christopher_Lee_as_Saruman.jpg/revision/latest?cb=20170127113833" });
            modelBuilder.Entity<Character>().HasData(new Character() { Id = 6, FullName = "Aragorn", Alias = "Dunedain", Gender = "Male", PictureUrl = "https://static.wikia.nocookie.net/lotr/images/b/b6/Aragorn_profile.jpg/revision/latest?cb=20170121121423" });

            //Franchises
            modelBuilder.Entity<Franchise>().HasData(new Franchise() { Id = 1, Name = "Batman Nolanverse" });
            modelBuilder.Entity<Franchise>().HasData(new Franchise() { Id = 2, Name = "Lord of the Rings" });
        }
    }
}
