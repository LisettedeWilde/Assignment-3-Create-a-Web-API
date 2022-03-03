using System.ComponentModel.DataAnnotations;

namespace MovieCharacterAPI.Models.DTOs.FranchiseDTOs
{
    public class FranchiseCreateDTO
    {
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(400)]
        public string Description { get; set; }
    }
}
