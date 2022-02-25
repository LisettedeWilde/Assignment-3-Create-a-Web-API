using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Character_EF.Models
{
    public class Franchise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        // one to many relationship, movie - franchise
        public int MovieId { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
