using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacterAPI.Models.Domain
{
    public class Franchise
    {
        [Key]
        public int FranchiseId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(400)]
        public string Description { get; set; }

        // one to many relationship, movie - franchise
        public ICollection<Movie> Movies { get; set; }
    }
}
