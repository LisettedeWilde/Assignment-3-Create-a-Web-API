using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Character_EF.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string? Picture { get; set; }
        public string? Trailer { get; set; }

        // one to many relationship, movie - franchise
        public Franchise Franchise { get; set; }
        // many to many relationship, movie - character
        public ICollection<Character> Characters { get; set; }

    }
}
