using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Models.Data;
using MovieCharacterAPI.Models.DTOs.CharacterDTOs;
using MovieCharacterAPI.Models.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacterAPI.Controllers
{
    [Route("api/Characters")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly MovieCharacterDbContext _context;
        private readonly IMapper _mapper;
        public CharacterController(MovieCharacterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetAllCharacters()
        {
            var characters = await _context.Character.Include(c => c.Movies).ToListAsync();

            var readCharacters = _mapper.Map<List<CharacterReadDTO>>(characters);

            return Ok(readCharacters);
        }

        [HttpGet("{characterId}")]
        public async Task<ActionResult<CharacterReadDTO>> GetById(int characterId)
        {
            var character = await _context.Character.FindAsync(characterId);

            if (character == null)
                return NotFound();

            var characterReadDTO = _mapper.Map<CharacterReadDTO>(character);

            return Ok(characterReadDTO);
        }

        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter([FromBody] CharacterCreateDTO characterCreateDTO)
        {
            var character = _mapper.Map<Character>(characterCreateDTO);

            try
            {
                _context.Character.Add(character);
                await _context.SaveChangesAsync();
            }
            catch //TODO: add exception
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var newCharacter = _mapper.Map<CharacterReadDTO>(character);
            
            return CreatedAtAction("GetById", new { CharacterId = newCharacter.CharacterId }, character);
        }

        [HttpDelete("{characterId}")]
        public async Task<ActionResult> DeleteCharacter(int characterId)
        {
            var character = await _context.Character.FindAsync(characterId);

            if (character == null)
                return NotFound();

            _context.Character.Remove(character);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{characterId}")]
        public async Task<ActionResult> UpdateCharacter(int characterId, [FromBody] CharacterEditDTO character)
        {
            if (characterId != character.CharacterId)
                return BadRequest();

            Character domainCharacter = _mapper.Map<Character>(character);

            _context.Entry(domainCharacter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (domainCharacter == null)
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }
    }
}
