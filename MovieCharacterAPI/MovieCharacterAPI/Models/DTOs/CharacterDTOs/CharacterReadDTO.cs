using MovieCharacterAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacterAPI.Models.DTOs.CharacterDTOs
{
    public class CharacterReadDTO
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Alias { get; set; }
        public string Picture { get; set; }
        public List<string> Movies { get; set; }
    }
}
