using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCharacterAPI.Models.Domain
{
    [Table("Movie")]
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [MaxLength(200)] 
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        [MaxLength(200)]
        public string Director { get; set; }
        [MaxLength(200)]
        public string Genre { get; set; }
        [MaxLength(400)]
        public string? Picture { get; set; }
        [MaxLength(400)]
        public string? Trailer { get; set; }

        // one to many relationship, movie - franchise

        public int? FranchiseId { get; set; }
        public Franchise Franchise { get; set; }
        // many to many relationship, movie - character
        public ICollection<Character> Characters { get; set; }
    }
}
