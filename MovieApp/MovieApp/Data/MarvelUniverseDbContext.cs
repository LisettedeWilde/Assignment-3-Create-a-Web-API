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
    public class MarvelUniverseDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Franchise> Franchises { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(ConnectionStringHelper.GetConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 1, Title = "Iron Man", Genre = "Action", ReleaseYear = 2015, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 2, Title = "The Incredible Hulk", Genre = "Action", ReleaseYear = 2008, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 3, Title = "Iron Man 2", Genre = "Action", ReleaseYear = 2015, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 4, Title = "Thor", Genre = "Action", ReleaseYear = 2011, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 5, Title = "Captain America", Genre = "Science Fiction", ReleaseYear = 2011, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 6, Title = "Avengers, The", Genre = "Science Fiction", ReleaseYear = 2012, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 7, Title = "Iron Man 3", Genre = "Action", ReleaseYear = 2015, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 8, Title = "Thor: The Dark World", Genre = "Science Fiction", ReleaseYear = 2013, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 9, Title = "Captain America: The Winter Soldier", Genre = "Science Fiction", ReleaseYear = 2014, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 10, Title = "Guardians of the Galaxy", Genre = "Science Fiction", ReleaseYear = 2014, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 11, Title = "Avengers: Age of Ultron", Genre = "Science Fiction", ReleaseYear = 2015, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 12, Title = "Ant-Man", Genre = "Science Fiction", ReleaseYear = 2015, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 13, Title = "Captain America: Civil War", Genre = "Science Fiction", ReleaseYear = 2016, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 14, Title = "Doctor Strange", Genre = "Science Fiction", ReleaseYear = 2016, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 15, Title = "Guardians of the Galaxy Vol. 2", Genre = "Science Fiction", ReleaseYear = 2017, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 16, Title = "Spider-Man: Homecoming", Genre = "Science Fiction", ReleaseYear = 2017, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 17, Title = "Thor: Ragnarok", Genre = "Science Fiction", ReleaseYear = 2017, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 18, Title = "Black Panther", Genre = "Science Fiction", ReleaseYear = 2018, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 19, Title = "Avengers: Infinity War", Genre = "Science Fiction", ReleaseYear = 2018, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 20, Title = "Ant-Man and the Wasp", Genre = "Science Fiction", ReleaseYear = 2018, Director = "", PictureUrl = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 21, Title = "The Fellowship of the Ring", Genre = "Fantasy", ReleaseYear = 2001, Director = "Peter Jackson", PictureUrl = "", Trailer = "", FranchiseId = 2 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 22, Title = "The Two Towers", Genre = "Fantasy", ReleaseYear = 2003, Director = "Peter Jackson", PictureUrl = "", Trailer = "", FranchiseId = 2 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 23, Title = "The Return of the King", Genre = "Fantasy", ReleaseYear = 2005, Director = "Peter Jackson", PictureUrl = "", Trailer = "", FranchiseId = 2 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { Id = 24, Title = "Hobbit", Genre = "Fantasy", ReleaseYear = 2010, Director = "Peter Jackson", PictureUrl = "", Trailer = "", FranchiseId = 2 });

            modelBuilder.Entity<Character>().HasData(new Character() { Id = 1, FullName = "Iron Man", Alias = "Human", Gender = "Male", PictureUrl = "https://vignette.wikia.nocookie.net/marvelcinematicuniverse/images/e/e9/Iron_Man_AIW_Profile.jpg/revision/latest?cb=20180518212029" });
            modelBuilder.Entity<Character>().HasData(new Character() { Id = 2, FullName = "Captain America", Alias = "Human", Gender = "Male", PictureUrl = "https://vignette.wikia.nocookie.net/marvelcinematicuniverse/images/6/66/Captain_America_AIW_Profile.jpg/revision/latest?cb=20180518211704" });
            modelBuilder.Entity<Character>().HasData(new Character() { Id = 3, FullName = "Hulk", Alias = "Human", Gender = "Male", PictureUrl = "https://vignette.wikia.nocookie.net/marvelcinematicuniverse/images/c/c3/Hulk_AIW_Profile.jpg/revision/latest?cb=20180518211829" });
            modelBuilder.Entity<Character>().HasData(new Character() { Id = 4, FullName = "Thor", Alias = "Asgardian", Gender = "Male", PictureUrl = "https://vignette.wikia.nocookie.net/marvelcinematicuniverse/images/4/45/Thor_AIW_Profile.jpg/revision/latest?cb=20180518212120" });
            modelBuilder.Entity<Character>().HasData(new Character() { Id = 5, FullName = "Black Widow", Alias = "Human", Gender = "Female", PictureUrl = "https://vignette.wikia.nocookie.net/marvelcinematicuniverse/images/5/50/Black_Widow_AIW_Profile.jpg/revision/latest?cb=20180518212205" });
            modelBuilder.Entity<Character>().HasData(new Character() { Id = 6, FullName = "Hawkeye", Alias = "Human", Gender = "Maale", PictureUrl = "https://vignette.wikia.nocookie.net/marvelcinematicuniverse/images/6/6f/CW_Textless_Shield_Poster_02.jpg/revision/latest?cb=20180417151836" });

            modelBuilder.Entity<Franchise>().HasData(new Franchise() { Id = 1, Name = "Marvel Cinematic Universe" });
            modelBuilder.Entity<Franchise>().HasData(new Franchise() { Id = 2, Name = "Lord of the Rings" });
        }
    }
}
