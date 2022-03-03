using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;
using MoviesAPI.DTO;
using MovieApp.Data;

namespace MoviesAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly NolanverseDbContext _context;

        public MovieService(NolanverseDbContext context)
        {
            _context = context;
        }

        public bool MovieExists(int id)
        {
            return false;
        }

        public async Task UpdateMovieCharactersAsync(int movieId, List<int> characters)
        {
            Movie movieToUpdateCharacters = await _context.Movies
                .Include(m => m.Characters)
                .Where(m => m.Id == movieId)
                .FirstAsync();

            List<Character> charactersToUpdate = new List<Character>();
            foreach (int characterId in characters)
            {
                Character character = await _context.Characters.FindAsync(characterId);
                if (character == null)
                {
                    throw new KeyNotFoundException();
                }
                charactersToUpdate.Add(character);
            }

            movieToUpdateCharacters.Characters = charactersToUpdate;
            await _context.SaveChangesAsync();
        }
    }
}
