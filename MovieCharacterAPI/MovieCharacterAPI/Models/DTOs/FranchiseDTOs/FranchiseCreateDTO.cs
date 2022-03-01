using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
