using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    
        public interface IFranchiseService
        {
            public Task UpdateFranchiseMoviesAsync(int franchiseId, List<int> movies);
            public bool FranchiseExists(int id);
        }
    
}
