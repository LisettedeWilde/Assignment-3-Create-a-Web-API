using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieCharacterAPI.Models.DTOs.FranchiseDTOs
{
    public class FranchiseReadDTO
    {
        public int FranchiseId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(400)]
        public string Description { get; set; }
        public List<int> Movies { get; set; }
    }
}
