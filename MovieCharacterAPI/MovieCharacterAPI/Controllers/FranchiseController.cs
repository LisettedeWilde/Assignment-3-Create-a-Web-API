using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MovieCharacterAPI.Models.Data;
using System.Collections.Generic;
using System.Linq;
using MovieCharacterAPI.Models.DTOs.FranchiseDTOs;
using MovieCharacterAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Models.DTOs.MovieDTOs;
using MovieCharacterAPI.Models.DTOs.CharacterDTOs;
using System.Threading.Tasks;
using System.Net.Mime;

namespace MovieCharacterAPI.Controllers
{
    [Route("api/Franchises")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FranchiseController : ControllerBase
    {
        private readonly MovieCharacterDbContext _context;
        private readonly IMapper _mapper;
        public FranchiseController(MovieCharacterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Fetches all the franchises
        /// </summary>
        /// <returns>A list of franchises</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseReadDTO>>> GetAllFranchises()
        {
            // Fetch all franchises, including their movies from the database
            var franchises = await _context.Franchise.Include(f => f.Movies).ToListAsync();

            // Convert the franchise objects to FranchiseReadDTO objects
            var readFranchises = _mapper.Map<List<FranchiseReadDTO>>(franchises);

            // Return the list of movies
            return Ok(readFranchises);
        }

        /// <summary>
        /// Gets a specific franchise by their id
        /// </summary>
        /// <param name="franchiseId">The id of the franchise to be fetched</param>
        /// <returns>A franchise object</returns>
        [HttpGet("{franchiseId}")]
        public async Task<ActionResult<FranchiseReadDTO>> GetById(int franchiseId)
        {
            // Fetch the franchise that matches the given franchiseId from the database
            var franchise = await _context.Franchise.Include(f => f.Movies).FirstOrDefaultAsync(f => f.FranchiseId == franchiseId);

            // Check whether a franchise has been returned from the query
            if (franchise == null)
                return NotFound();
           
            // Convert franchise object to franchiseReadDTO
            var franchiseReadDTO = _mapper.Map<FranchiseReadDTO>(franchise);

            // Return the franchise
            return Ok(franchiseReadDTO);
        }

        /// <summary>
        /// Fetches all the movies in a specific franchise
        /// </summary>
        /// <param name="franchiseId">A franchise id</param>
        /// <returns>A list of movies</returns>
        [HttpGet("{franchiseId}/movies")]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetMoviesInFranchise(int franchiseId)
        {
            // Fetch the franchise that matches the given franchiseId from the database, including its movies
            var franchise = await _context.Franchise.Include(f => f.Movies).ThenInclude(m => m.Characters).FirstOrDefaultAsync(f => f.FranchiseId == franchiseId);

            // Check whether a franchise has been returned from the query
            if (franchise == null)
                return NotFound();

            // Get the list of movies from the franchise
            var movies = franchise.Movies;

            // Convert the movie objects to MovieReadDTO object
            var readMovies = _mapper.Map<List<MovieReadDTO>>(movies);

            // Return the list of movies
            return Ok(readMovies);
        }

        /// <summary>
        /// Fetches all the characters in a specific franchise
        /// </summary>
        /// <param name="franchiseId">A franchise id</param>
        /// <returns>A list of characters</returns>
        [HttpGet("{franchiseId}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharactersInFranchise(int franchiseId)
        {
            // Fetch the franchise that matches the given franchiseId from the database, including its movies and characters
            var franchise = await _context.Franchise.Include(f => f.Movies).ThenInclude(m => m.Characters).FirstOrDefaultAsync(f => f.FranchiseId == franchiseId);

            // Check whether a franchise has been returned from the query
            if (franchise == null)
                return NotFound();

            // Get the list of movies from the franchise
            var movies = franchise.Movies;

            // For each movie in the franchise, get its characters and add it to a list
            List<Character> characters = new List<Character>();
            foreach (Movie movie in movies)
            {
                foreach (Character character in movie.Characters)
                {
                    if (characters.Contains(character))
                        continue;
                    characters.Add(character);
                }    
            }

            // Convert the character objects to CharacterReadDTO object
            var readCharacters = _mapper.Map<List<CharacterReadDTO>>(characters);

            // Return the list of characters
            return Ok(readCharacters);
        }

        /// <summary>
        /// Adds a new franchise to the database
        /// </summary>
        /// <param name="franchiseCreateDTO">A franchise object</param>
        /// <returns>The franchise that has been added to the database</returns>
        [HttpPost]
        public async Task<ActionResult<Franchise>> PostFranchise([FromBody] FranchiseCreateDTO franchiseCreateDTO)
        {
            // Convert the given franchiseCreateDTO to franchise object
            var franchise = _mapper.Map<Franchise>(franchiseCreateDTO);

            try
            {
                // Add the franchise to the database and save changes
                await _context.Franchise.AddAsync(franchise);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            // Convert the franchise object to FranchiseReadDTO object
            var newFranchise = _mapper.Map<FranchiseReadDTO>(franchise);

            // Returns the added franchise
            return CreatedAtAction("GetById", new { franchiseId = newFranchise.FranchiseId }, franchise);
        }

        /// <summary>
        /// Deletes a franchise from the databse
        /// </summary>
        /// <param name="franchiseId">Id of the franchise that needs to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{franchiseId}")]
        public async Task<ActionResult> DeleteFranchise(int franchiseId)
        {
            // Fetch the franchise that matches the given franchiseId from the database
            var franchise = await _context.Franchise.FindAsync(franchiseId);

            // Check whether a franchise object had been returned from the query
            if (franchise == null)
                return NotFound();

            try
            {
                // Delete the franchise from the database
                _context.Franchise.Remove(franchise);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        /// <summary>
        /// Updates a franchise
        /// </summary>
        /// <param name="franchiseId">Id of the franchise that needs to be updated</param>
        /// <param name="franchise">A franchise object which replaces the original franchise object in the database</param>
        /// <returns></returns>
        [HttpPut("{franchiseId}")]
        public async Task<ActionResult> UpdateFranchise(int franchiseId, [FromBody] FranchiseEditDTO franchise)
        {
            // Check if the given franchiseId matches the given Franchise object
            if (franchiseId != franchise.FranchiseId)
                return BadRequest();

            // Convert the given franchiseEditDTO to a franchise object
            Franchise domainFranchise = _mapper.Map<Franchise>(franchise);

            // Mark all the properties of the entity as modified, so that all the property values will be sent to the database when SaveChanges is called
            _context.Entry(domainFranchise).State = EntityState.Modified;

            try
            {
                // Update the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check whether a franchise object had been returned from the query
                if (domainFranchise == null)
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Updates the movies in a franchise
        /// </summary>
        /// <param name="franchiseId">Id of the franchise that needs to be updated</param>
        /// <param name="movieIds">A list of movieids which will replace the currect movies associated with the franchise</param>
        /// <returns></returns>
        [HttpPut("{franchiseId}/movies")]
        public async Task<ActionResult> UpdateMoviesInFranchise(int franchiseId, [FromBody] int[] movieIds)
        {
            // Fetch the franchise that matches the given franchiseId from the database, including its movies and franchiseId
            var franchiseToUpdate = await _context.Franchise.Include(f => f.Movies).FirstOrDefaultAsync(f => f.FranchiseId == franchiseId);

            // Check whether a franchise object had been returned from the query
            if (franchiseToUpdate == null)
                return NotFound();

            // Create a list of movie objects based on the given list of movieIds
            List<Movie> movies = new();
            foreach (int movieId in movieIds)
            {
                Movie movie = await _context.Movie.FindAsync(movieId);
                if (movie == null)
                    return BadRequest("Movie doesn't exist");
                movies.Add(movie);
            }

            // Set the franchise movies to the new list of movies
            franchiseToUpdate.Movies = movies;

            try
            {
                // Update the database
                await _context.SaveChangesAsync();
            }
            catch
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
