using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Models.Data;
using MovieCharacterAPI.Models.DTOs.CharacterDTOs;
using MovieCharacterAPI.Models.Domain;
using System.Collections.Generic;
using System.Linq;

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
        public ActionResult<IEnumerable<CharacterReadDTO>> GetAllCharacters()
        {
            var characters = _context.Character.ToList();

            var readCharacters = _mapper.Map<List<CharacterReadDTO>>(characters);

            return Ok(readCharacters);
        }

        [HttpGet("{characterId}")]
        public ActionResult<CharacterReadDTO> GetById(int characterId)
        {
            var character = _context.Character.Find(characterId);

            if (character == null)
                return NotFound();

            var characterReadDTO = _mapper.Map<CharacterReadDTO>(character);

            return Ok(characterReadDTO);
        }

        [HttpPost]
        public ActionResult<Character> PostCharacter([FromBody] CharacterCreateDTO characterCreateDTO)
        {
            var character = _mapper.Map<Character>(characterCreateDTO);

            try
            {
                _context.Character.Add(character);
                _context.SaveChanges();
            }
            catch //TODO: add exception
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var newCharacter = _mapper.Map<CharacterReadDTO>(character);
            
            return CreatedAtAction("GetById", new { CharacterId = newCharacter.CharacterId }, character);
        }

        [HttpDelete("{characterId}")]
        public ActionResult DeleteCharacter(int characterId)
        {
            var character = _context.Character.Find(characterId);

            if (character == null)
                return NotFound();

            _context.Character.Remove(character);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{characterId}")]
        public ActionResult UpdateCharacter(int characterId, [FromBody] Character character)
        {
            var oldCharacter = _context.Character.Find(characterId);

            if (oldCharacter == null)
                return NotFound();

            if (characterId != character.CharacterId)
                return BadRequest();

            _context.Entry(character).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }
    }
}
