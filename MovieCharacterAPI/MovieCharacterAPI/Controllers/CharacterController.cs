using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Models.Data;
using MovieCharacterAPI.Models.DTOs.CharacterDTOs;
using MovieCharacterAPI.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Mime;
using System.Linq;

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
            // Fetch all characters, including their movies from the database
            var characters = await _context.Character.Include(c => c.Movies).ToListAsync();

            // Convert the character objects to CharacterReadDTO objects
            var readCharacters = _mapper.Map<List<CharacterReadDTO>>(characters);

            // return the list of characters
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
            // Fetch the character that matches the given characterId from the database, including their movies
            var character = await _context.Character.Include(c => c.Movies).Where(c => c.CharacterId == characterId).SingleAsync();

            // Check whether there is a character at the id location in the database
            if (character == null)
                return NotFound();

            // Convert the character object to CharacterReadDTO object
            var characterReadDTO = _mapper.Map<CharacterReadDTO>(character);

            // return the Character
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
            // Convert given CharacterCreateDTO to character object
            var character = _mapper.Map<Character>(characterCreateDTO);

            try
            {
                // Add the character to the database
                await _context.Character.AddAsync(character);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            // Convert the character object to CharacterReadDTO object
            var newCharacter = _mapper.Map<CharacterReadDTO>(character);
            
            // Returns the added character
            return CreatedAtAction("GetById", new { CharacterId = newCharacter.CharacterId }, character);
        }

        /// <summary>
        /// Deletes a character from the database
        /// </summary>
        /// <param name="characterId">Id of the character that needs to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{characterId}")]
        public async Task<ActionResult> DeleteCharacter(int characterId)
        {
            // Fetch the character that matches the given characterId from the database
            var character = await _context.Character.FindAsync(characterId);

            // Check whether there is a character at the id location in the database
            if (character == null)
                return NotFound();

            try
            {
                // Delete the character from the database
                _context.Character.Remove(character);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

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
            // Check if the given characterId matches the given Character object
            if (characterId != character.CharacterId)
                return BadRequest();

            // Convert the given CharacterEditDTO to a character object
            Character domainCharacter = _mapper.Map<Character>(character);

            // Mark all the properties of the entity as modified, so that all the property values will be sent to the database when SaveChanges is called
            _context.Entry(domainCharacter).State = EntityState.Modified;

            try
            {
                // Update the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check whether there is a character at the id location in the database
                if (domainCharacter == null)
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }
    }
}
