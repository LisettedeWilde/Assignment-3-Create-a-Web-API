using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacterAPI.Models.DTOs.MovieDTOs
{
    public class MovieReadDTO
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
        public string Picture { get; set; }
        [MaxLength(400)]
        public string Trailer { get; set; }
    }
}
