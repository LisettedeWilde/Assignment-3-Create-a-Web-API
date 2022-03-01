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

namespace MovieCharacterAPI.Controllers
{
    [Route("api/Franchises")]
    [ApiController]
    public class FranchiseController : ControllerBase
    {
        private readonly MovieCharacterDbContext _context;
        private readonly IMapper _mapper;
        public FranchiseController(MovieCharacterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseReadDTO>>> GetAllFranchises()
        {
            var franchises = await _context.Franchise.Include(f => f.Movies).ThenInclude(m => m.Characters).ToListAsync();

            var readFranchises = _mapper.Map<List<FranchiseReadDTO>>(franchises);

            return Ok(readFranchises);
        }

        [HttpGet("{franchiseId}")]
        public async Task<ActionResult<FranchiseReadDTO>> GetById(int franchiseId)
        {
            var franchise = await _context.Franchise.FindAsync(franchiseId);

            if (franchise == null)
                return NotFound();

            var franchiseReadDTO = _mapper.Map<FranchiseReadDTO>(franchise);

            return Ok(franchiseReadDTO);
        }

        // Get all movies in franchise
        [HttpGet("GetMoviesInFranchise/{franchiseId}")]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetMoviesInFranchise(int franchiseId)
        {
            var franchise = await _context.Franchise.Include(f => f.Movies).Where(f => f.FranchiseId == franchiseId).SingleAsync();

            if (franchise == null)
                return NotFound();

            var movies = franchise.Movies;

            var readMovies = _mapper.Map<List<MovieReadDTO>>(movies);

            return Ok(readMovies);
        }

        // Get all characters in franchise
        [HttpGet("GetCharactersInFranchise/{franchiseId}")]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharactersInFranchise(int franchiseId)
        {
            var franchise = await _context.Franchise.Include(f => f.Movies).ThenInclude(m => m.Characters).Where(f => f.FranchiseId == franchiseId).SingleAsync();

            if (franchise == null)
                return NotFound();

            var movies = franchise.Movies;

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

            var readCharacters = _mapper.Map<List<CharacterReadDTO>>(characters);

            return Ok(readCharacters);
        }

        [HttpPost]
        public async Task<ActionResult<Franchise>> PostFranchise([FromBody] FranchiseCreateDTO franchiseCreateDTO)
        {
            var franchise = _mapper.Map<Franchise>(franchiseCreateDTO);

            try
            {
                _context.Franchise.Add(franchise);
                await _context.SaveChangesAsync();
            }
            catch //TODO: add exception
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var newFranchise = _mapper.Map<FranchiseReadDTO>(franchise);

            return CreatedAtAction("GetById", new { franchiseId = newFranchise.FranchiseId }, franchise);
        }

        [HttpDelete("{franchiseId}")]
        public async Task<ActionResult> DeleteFranchise(int franchiseId)
        {
            var franchise = await _context.Franchise.FindAsync(franchiseId);

            if (franchise == null)
                return NotFound();

            _context.Franchise.Remove(franchise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{franchiseId}")]
        public async Task<ActionResult> UpdateFranchise(int franchiseId, [FromBody] FranchiseEditDTO franchise)
        {
            if (franchiseId != franchise.FranchiseId)
                return BadRequest();

            Franchise domainFranchise = _mapper.Map<Franchise>(franchise);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (domainFranchise == null)
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // TODO: Update movies in franchise
        [HttpPut("UpdateMovies/{franchiseId}")]
        public async Task<ActionResult> UpdateMoviesInFranchise(int franchiseId, [FromBody] int[] movieIds)
        {
            var franchiseToUpdate = await _context.Franchise.Include(f => f.Movies).Where(f => f.FranchiseId == franchiseId).SingleAsync();

            if (franchiseToUpdate == null)
                return NotFound();

            List<Movie> movies = new();
            foreach (int movieId in movieIds)
            {
                Movie movie = await _context.Movie.FindAsync(movieId);
                if (movie == null)
                    return BadRequest("Movie doesn't exist");
                movies.Add(movie);
            }

            franchiseToUpdate.Movies = movies;

            try
            {
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
