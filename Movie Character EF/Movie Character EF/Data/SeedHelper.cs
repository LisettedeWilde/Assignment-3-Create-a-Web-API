using Movie_Character_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Character_EF.Data
{
    class SeedHelper
    {
        public static IEnumerable<Character> GetCharacterSeeds()
        {
            IEnumerable<Character> SeedCharacters = new List<Character>()
            {
                new Character()
                {
                    CharacterId = 1,
                    Name = "Peter Parker",
                    Gender = "Male",
                    Alias = "Spider-Man",
                    Picture = @"https://upload.wikimedia.org/wikipedia/en/0/0f/Tom_Holland_as_Spider-Man.jpg",
                },
                new Character()
                {
                    CharacterId = 2,
                    Name = "Frodo Baggins",
                    Gender = "Male",
                    Picture = @"https://upload.wikimedia.org/wikipedia/en/thumb/4/4e/Elijah_Wood_as_Frodo_Baggins.png/170px-Elijah_Wood_as_Frodo_Baggins.png"
                },
                new Character()
{
                    CharacterId = 3,
                    Name = "Gandalf",
                    Gender = "Male",
                    Alias = "White Wizard",
                    Picture = "https://upload.wikimedia.org/wikipedia/en/thumb/e/e9/Gandalf600ppx.jpg/170px-Gandalf600ppx.jpg"
                },
                new Character()
                {
                    CharacterId = 4,
                    Name = "Legolas",
                    Gender = "Male",
                    Alias = "Greenleaf",
                    Picture = "https://upload.wikimedia.org/wikipedia/en/thumb/2/2b/Legolas600ppx.jpg/220px-Legolas600ppx.jpg"
                },
                new Character()
                {
                    CharacterId = 5,
                    Name = "Anakin Skywalker",
                    Gender = "Male",
                    Alias = "Darth Vader",
                    Picture = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/49/Anakin_Skywalker_costume_Retouched.jpg/800px-Anakin_Skywalker_costume_Retouched.jpg"
                }
            };
            return SeedCharacters;
        }

        public static IEnumerable<Movie> GetMovieSeeds()
        {
            IEnumerable<Movie> SeedMovie = new List<Movie>()
            {
                new Movie()
                {
                    MovieId = 1,
                    Title = "The Lord of the Rings: The Two Towers",
                    ReleaseYear = 2002,
                    Director = "Peter Jackson",
                    Genre = "Action, Adventure, Drama, Fantasy",
                    Picture = "https://upload.wikimedia.org/wikipedia/en/d/d0/Lord_of_the_Rings_-_The_Two_Towers_%282002%29.jpg",
                    Trailer = "https://www.youtube.com/watch?v=LbfMDwc4azU",
                    FranchiseId = 1
                },
                new Movie()
                {
                    MovieId = 2,
                    Title = "The Amazing Spider-Man",
                    ReleaseYear = 2012,
                    Director = "Marc Webb",
                    Genre = "Action, Adventure, Sci-Fi",
                    Picture = "https://upload.wikimedia.org/wikipedia/en/thumb/0/02/The_Amazing_Spider-Man_theatrical_poster.jpeg/220px-The_Amazing_Spider-Man_theatrical_poster.jpeg",
                    Trailer = "https://www.youtube.com/watch?v=-tnxzJ0SSOw",
                    FranchiseId = 2
                },
                new Movie()
                {
                    MovieId = 3,
                    Title = "Star Wars: Episode III: Revenge of the Sith",
                    ReleaseYear = 2005,
                    Director = "George Lucas",
                    Genre = "Action, Adventure, Fantasy, Sci-Fi",
                    Picture = "https://upload.wikimedia.org/wikipedia/en/thumb/9/93/Star_Wars_Episode_III_Revenge_of_the_Sith_poster.jpg/220px-Star_Wars_Episode_III_Revenge_of_the_Sith_poster.jpg",
                    Trailer = "https://www.youtube.com/watch?v=5UnjrG_N8hU",
                    FranchiseId = 3
                }
            };
            return SeedMovie;
        }

        public static IEnumerable<Franchise> GetFranchiseSeeds()
        {
            IEnumerable<Franchise> SeedFranchise = new List<Franchise>()
            {
                new Franchise()
                {
                    FranchiseId = 1,
                    Name = "Lord of the Rings",
                    Description = "The Lord of the Rings is a series of three epic fantasy adventure films directed by Peter Jackson, based on the novel written by J. R. R. Tolkien."
                },
                new Franchise()
                {
                    FranchiseId = 2,
                    Name = "Spider-Man",
                    Description = "Movies about Spider-Man"
                },
                new Franchise()
                {
                    FranchiseId = 3,
                    Name = "Star Wars",
                    Description = "Star Wars is an American epic space opera[1] multimedia franchise created by George Lucas, which began with the eponymous 1977 film[b] and quickly became a worldwide pop-culture phenomenon."
                }
            };
            return SeedFranchise;
        }
    }
}
