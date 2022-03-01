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
        public ActionResult<IEnumerable<FranchiseReadDTO>> GetAllFranchises()
        {
            var franchises = _context.Franchise.ToList();

            var readFranchises = _mapper.Map<List<FranchiseReadDTO>>(franchises);

            return Ok(readFranchises);
        }

        [HttpGet("{franchiseId}")]
        public ActionResult<FranchiseReadDTO> GetById(int franchiseId)
        {
            var franchise = _context.Franchise.Find(franchiseId);

            if (franchise == null)
                return NotFound();

            var franchiseReadDTO = _mapper.Map<FranchiseReadDTO>(franchise);

            return Ok(franchiseReadDTO);
        }

        // Get all movies in franchise
        [HttpGet("GetMoviesInFranchise/{franchiseId}")]
        public ActionResult<IEnumerable<MovieReadDTO>> GetMoviesInFranchise(int franchiseId)
        {
            //var franchise = _context.Franchise.Find(franchiseId);
            var franchise = _context.Franchise.Include(f => f.Movies).Where(f => f.FranchiseId == franchiseId).Single();

            if (franchise == null)
                return NotFound();

            var movies = franchise.Movies;

            var readMovies = _mapper.Map<List<MovieReadDTO>>(movies);

            return Ok(readMovies);
        }

        // Get all characters in franchise
        [HttpGet("GetCharactersInFranchise/{franchiseId}")]
        public ActionResult<IEnumerable<CharacterReadDTO>> GetCharactersInFranchise(int franchiseId)
        {
            var franchise = _context.Franchise.Include(f => f.Movies).ThenInclude(m => m.Characters).Where(f => f.FranchiseId == franchiseId).Single();

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
        public ActionResult<Franchise> PostFranchise([FromBody] FranchiseCreateDTO franchiseCreateDTO)
        {
            var franchise = _mapper.Map<Franchise>(franchiseCreateDTO);

            try
            {
                _context.Franchise.Add(franchise);
                _context.SaveChanges();
            }
            catch //TODO: add exception
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var newFranchise = _mapper.Map<FranchiseReadDTO>(franchise);

            return CreatedAtAction("GetById", new { franchiseId = newFranchise.FranchiseId }, franchise);
        }

        [HttpDelete("{franchiseId}")]
        public ActionResult DeleteFranchise(int franchiseId)
        {
            var franchise = _context.Franchise.Find(franchiseId);

            if (franchise == null)
                return NotFound();

            _context.Franchise.Remove(franchise);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{franchiseId}")]
        public ActionResult UpdateFranchise(int franchiseId, [FromBody] Franchise franchise)
        {
            if (franchiseId != franchise.FranchiseId)
                return BadRequest();

            _context.Entry(franchise).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // TODO: Update movies in franchise
        [HttpPut("UpdateMovies/{franchiseId}")]
        public ActionResult UpdateMoviesInFranchise(int franchiseId, [FromBody] int[] movieIds)
        {
            var newFranchise = _context.Franchise.Find(franchiseId);

            List<Movie> movies = new List<Movie>();
            foreach (int id in movieIds)
            {
                var movie = _context.Movie.Find(id);

                if (movie == null)
                    return NotFound();
                movies.Add(movie);
            }

            newFranchise.Movies = movies;
            _context.Franchise.Update(newFranchise);
            //_context.Entry(newFranchise).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
