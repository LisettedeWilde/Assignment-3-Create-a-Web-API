using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacterAPI.Models.Domain
{
    public class Character
    {
        [Key]
        public int CharacterId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Gender { get; set; }
        [MaxLength(200)]
        public string Alias { get; set; }
        [MaxLength(400)]
        public string Picture { get; set; }

        // many to many relationship, movie - character
        public ICollection<Movie> Movies { get; set; }
    }
}
