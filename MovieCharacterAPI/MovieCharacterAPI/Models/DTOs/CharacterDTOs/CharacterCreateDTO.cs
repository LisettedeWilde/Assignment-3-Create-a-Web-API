using System.ComponentModel.DataAnnotations;

namespace MovieCharacterAPI.Models.DTOs.CharacterDTOs
{
    public class CharacterCreateDTO
    {
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Gender { get; set; }
        [MaxLength(200)]
        public string Alias { get; set; }
        [MaxLength(400)]
        public string Picture { get; set; }
    }
}
