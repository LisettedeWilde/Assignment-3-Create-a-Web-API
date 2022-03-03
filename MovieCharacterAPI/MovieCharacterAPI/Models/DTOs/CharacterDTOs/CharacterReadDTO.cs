using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieCharacterAPI.Models.DTOs.CharacterDTOs
{
    public class CharacterReadDTO
    {
        public int CharacterId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Gender { get; set; }
        [MaxLength(200)]
        public string Alias { get; set; }
        [MaxLength(400)]
        public string Picture { get; set; }
        public List<int> Movies { get; set; }
    }
}
