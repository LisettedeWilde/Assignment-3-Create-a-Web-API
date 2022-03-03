using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCharacterAPI.Models.Data;
using MovieCharacterAPI.Models.DTOs.MovieDTOs;
using MovieCharacterAPI.Models.Domain;
using MovieCharacterAPI.Models.DTOs.CharacterDTOs;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace MovieCharacterAPI.Controllers
{
    [Route("api/Movies")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MovieController : ControllerBase
    {
        private readonly MovieCharacterDbContext _context;
        private readonly IMapper _mapper;
        public MovieController(MovieCharacterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Fetches all the movies
        /// </summary>
        /// <returns>a list of movies</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetAllMovies()
        {
            // Fetch all movies, including their characters and their associated franchise from the database
            var movies = await _context.Movie.Include(m => m.Characters).Include(f => f.Franchise).ToListAsync();

            // Convert the movie objects to movieReadDTO objects
            var readMovies = _mapper.Map<List<MovieReadDTO>>(movies);

            // Return the list of movies
            return Ok(readMovies);
        }

        /// <summary>
        /// Gets a specific movie by their id
        /// </summary>
        /// <param name="movieId">The id of the movie to be fetched</param>
        /// <returns>A movie</returns>
        [HttpGet("{movieId}")]
        public async Task<ActionResult<MovieReadDTO>> GetById(int movieId)
        {
            // Fetch the movie that matches the given movieId from the database, including its characters and franchise
            var movie = await _context.Movie.Include(m => m.Characters).Include(f => f.Franchise).FirstOrDefaultAsync(m => m.MovieId == movieId);

            // Check whether a movie object had been returned from the query
            if (movie == null)
                return NotFound();

            // Convert movie object to movieReadDTO
            var movieReadDTO = _mapper.Map<MovieReadDTO>(movie);

            // Return the movie
            return Ok(movieReadDTO);
        }

        /// <summary>
        /// Fetches all the characters in a specific movie
        /// </summary>
        /// <param name="movieId">The id of the movie of which the characters need to be fetched</param>
        /// <returns>A list of characters</returns>
        [HttpGet("{movieId}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharactersInMovie(int movieId)
        {
            // Fetch the movie that matches the given movieId from the database, including its characters
            Movie movie = await _context.Movie.Include(p => p.Characters).FirstOrDefaultAsync(p => p.MovieId == movieId);

            // Check whether a movie object had been returned from the query
            if (movie == null)
                return NotFound();

            // Get the list of characters from the movie
            var characters = movie.Characters;

            // Convert the character objects to CharacterReadDTO objects
            var readCharacters = _mapper.Map<List<CharacterReadDTO>>(characters);

            // Return the list of characters
            return Ok(readCharacters);
        }

        /// <summary>
        /// Adds a new movie to the database
        /// </summary>
        /// <param name="movieCreateDTO">A movie object</param>
        /// <returns>The movie that has been added to the database</returns>
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie([FromBody] MovieCreateDTO movieCreateDTO)
        {
            // convert the given movieCreateDTO to movie object
            var movie = _mapper.Map<Movie>(movieCreateDTO);

            try
            {
                // Add movie to database and save changes
                await _context.Movie.AddAsync(movie);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            // Convert the movie object to MovieReadDTO object
            var newMovie = _mapper.Map<MovieReadDTO>(movie);

            // Returns the added movie
            return CreatedAtAction("GetById", new { movieId = newMovie.MovieId }, movie);
        }

        /// <summary>
        /// Deletes a movie from the database
        /// </summary>
        /// <param name="movieId">id of the movie that needs to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{movieId}")]
        public async Task<ActionResult> DeleteMovie(int movieId)
        {
            // Fetch the movie that matches the given movieId from the database
            var movie = await _context.Movie.FindAsync(movieId);

            // Check whether a movie object had been returned from the query
            if (movie == null)
                return NotFound();

            try
            {
                // Delete the movie from the database
                _context.Movie.Remove(movie);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        /// <summary>
        /// Updates a movie
        /// </summary>
        /// <param name="movieId">Id of the movie that needs to be updated</param>
        /// <param name="movie">A movie object which replaces the original movie object in the database</param>
        /// <returns></returns>
        [HttpPut("{movieId}")]
        public async Task<ActionResult> UpdateMovie(int movieId, [FromBody] MovieEditDTO movie)
        {
            // Check if the given movieId matches the given Movie object
            if (movieId != movie.MovieId)
                return BadRequest();

            // Convert the given MovieEditDTO to a movie object
            Movie domainMovie = _mapper.Map<Movie>(movie);

            /// Mark all the properties of the entity as modified, so that all the property values will be sent to the database when SaveChanges is called
            _context.Entry(domainMovie).State = EntityState.Modified;

            try
            {
                // Update the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check whether a movie object had been returned from the query
                if (domainMovie == null)
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Updates the characters in a movie
        /// </summary>
        /// <param name="movieId">id of the movie of which the characters need to be updated</param>
        /// <param name="characterIds">a list of characterids which will replace the current characters associated with the movie</param>
        /// <returns></returns>
        [HttpPut("{movieId}/characters")]
        public async Task<ActionResult> UpdateCharactersInMovie(int movieId, [FromBody] int[] characterIds)
        {
            // Fetch the movie that matches the given movieId from the database, including its characters
            var movieToUpdate = await _context.Movie.Include(m => m.Characters).FirstOrDefaultAsync(m => m.MovieId == movieId);

            // Check whether a movie object had been returned from the query
            if (movieToUpdate == null)
                return NotFound();

            // Create a list of character objects based on the given list of CharacterIds
            List<Character> characters = new();
            foreach (int characterId in characterIds)
            {
                Character character = await _context.Character.FindAsync(characterId);
                if (character == null)
                    return BadRequest("Character doesn't exist");
                characters.Add(character);
            }

            // Set the movies characters to the new list of characters
            movieToUpdate.Characters = characters;

            try
            {
                // Update the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }
    }
}
