using System;
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

namespace MovieCharacterAPI.Controllers
{
    [Route("api/Movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieCharacterDbContext _context;
        private readonly IMapper _mapper;
        public MovieController(MovieCharacterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetAllMovies()
        {
            // get all movies from database
            var movies = await _context.Movie.ToListAsync();

            // convert movies object to movieReadDTO
            var readMovies = _mapper.Map<List<MovieReadDTO>>(movies);

            // return movies
            return Ok(readMovies);
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<MovieReadDTO>> GetById(int movieId)
        {
            //get movie from database by movieId
            var movie = await _context.Movie.FindAsync(movieId);
            // check whether a movie object had been returned from the query
            if (movie == null)
                return NotFound();
            // convert movie object to movieReadDTO
            var movieReadDTO = _mapper.Map<MovieReadDTO>(movie);
            // return movie
            return Ok(movieReadDTO);
        }

        // Get all characters in a movie
        [HttpGet("GetCharactersInMovie/{movieId}")]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharactersInMovie(int movieId)
        {
            Movie movie = await _context.Movie.Include(p => p.Characters).Where(p => p.MovieId == movieId).SingleAsync();

            if (movie == null)
                return NotFound();

            var characters = movie.Characters;

            var readCharacters = _mapper.Map<List<CharacterReadDTO>>(characters);

            return Ok(readCharacters);
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie([FromBody] MovieCreateDTO movieCreateDTO)
        {
            // convert movieCreateDTO to movie object
            var movie = _mapper.Map<Movie>(movieCreateDTO);

            try
            {
                // add movie to database and save changes
                _context.Movie.Add(movie);
                await _context.SaveChangesAsync();
            }
            catch // TODO: add exception
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var newMovie = _mapper.Map<MovieReadDTO>(movie);

            return CreatedAtAction("GetById", new { movieId = newMovie.MovieId }, movie);
        }

        [HttpDelete("{movieId}")]
        public async Task<ActionResult> DeleteMovie(int movieId)
        {
            var movie = await _context.Movie.FindAsync(movieId);

            if (movie == null)
                return NotFound();

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{movieId}")]
        public async Task<ActionResult> UpdateMovie(int movieId, [FromBody] MovieEditDTO movie)
        {
            if (movieId != movie.MovieId)
                return BadRequest();

            Movie domainMovie = _mapper.Map<Movie>(movie);

            _context.Entry(domainMovie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (domainMovie == null)
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // Update characters in movie
        [HttpPut("UpdateCharacter/{movieId}")]
        public async Task<ActionResult> UpdateCharactersInMovie(int movieId, [FromBody] int[] characterIds)
        {
            var movieToUpdate = await _context.Movie.Include(m => m.Characters).Where(m => m.MovieId == movieId).FirstAsync();

            if (movieToUpdate == null)
                return NotFound();

            List<Character> characters = new();
            foreach (int characterId in characterIds)
            {
                Character character = await _context.Character.FindAsync(characterId);
                if (character == null)
                    return BadRequest("Character doesn't exist");
                characters.Add(character);
            }

            movieToUpdate.Characters = characters;

            try
            {
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
