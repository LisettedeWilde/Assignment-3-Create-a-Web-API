using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacterAPI.Models.DTOs.CharacterDTOs
{
    public class CharacterCreateDTO
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Alias { get; set; }
        public string Picture { get; set; }
    }
}
