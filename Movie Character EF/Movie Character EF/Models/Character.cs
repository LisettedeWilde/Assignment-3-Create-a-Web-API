using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Character_EF.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Alias { get; set; }
        public string Gender { get; set; }
        public string? Picture { get; set; }

        // many to many relationship, movie - character
        public ICollection<Movie> Movies { get; set; }
    }
}
