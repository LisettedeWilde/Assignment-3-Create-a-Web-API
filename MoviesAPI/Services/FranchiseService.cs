using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;
using MoviesAPI.DTO;
using MovieApp.Data;

namespace MoviesAPI.Services
{
    public class FranchiseService : IFranchiseService
    {
        private readonly NolanverseDbContext _context;

        public FranchiseService(NolanverseDbContext context)
        {
            _context = context;
        }

        public bool FranchiseExists(int id)
        {
            return false;
        }

        public async Task UpdateFranchiseMoviesAsync(int franchiseId, List<int> movies)
        {
            Franchise franchiseToUpdateMovies = await _context.Franchises
                .Include(f => f.Movies)
                .Where(f => f.Id == franchiseId)
                .FirstAsync();

            List<Movie> moviesToUpdate = new List<Movie>();
            foreach (var movieId in movies)
            {
                Movie movie = await _context.Movies.FindAsync(movieId);
                if (movie == null)
                {
                    throw new KeyNotFoundException();
                }

                moviesToUpdate.Add(movie);
            }

            franchiseToUpdateMovies.Movies = moviesToUpdate;
            await _context.SaveChangesAsync();
        }


    }
}
