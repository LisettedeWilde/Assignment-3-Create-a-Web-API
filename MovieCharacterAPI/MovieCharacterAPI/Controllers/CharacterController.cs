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
using System.Net.Mime;

namespace MovieCharacterAPI.Controllers
{
    [Route("api/Characters")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CharacterController : ControllerBase
    {
        private readonly MovieCharacterDbContext _context;
        private readonly IMapper _mapper;
        public CharacterController(MovieCharacterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Fetches all the characters from the database
        /// </summary>
        /// <returns>a list of characters</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetAllCharacters()
        {
            var characters = await _context.Character.ToListAsync();

            var readCharacters = _mapper.Map<List<CharacterReadDTO>>(characters);

            return Ok(readCharacters);
        }

        /// <summary>
        /// Gets a specific character by their id
        /// </summary>
        /// <param name="characterId">The id of the character to be fetched</param>
        /// <returns>A character</returns>
        [HttpGet("{characterId}")]
        public async Task<ActionResult<CharacterReadDTO>> GetById(int characterId)
        {
            var character = await _context.Character.FindAsync(characterId);

            if (character == null)
                return NotFound();

            var characterReadDTO = _mapper.Map<CharacterReadDTO>(character);

            return Ok(characterReadDTO);
        }

        /// <summary>
        /// Adds a new character to the database
        /// </summary>
        /// <param name="characterCreateDTO">A character object</param>
        /// <returns>The character that has been added to the database</returns>
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

        /// <summary>
        /// Deletes a character from the database
        /// </summary>
        /// <param name="characterId">Id of the character that needs to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{characterId}")]
        public async Task<ActionResult> DeleteCharacter(int characterId) // TODO: Check DTO
            //TODO: check if foreign keys are not deleted
        {
            var character = await _context.Character.FindAsync(characterId);

            if (character == null)
                return NotFound();

            _context.Character.Remove(character);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Updates a character
        /// </summary>
        /// <param name="characterId">Id of the character that needs to be updated</param>
        /// <param name="character">A character object which replaces the original character object in the database</param>
        /// <returns></returns>
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
