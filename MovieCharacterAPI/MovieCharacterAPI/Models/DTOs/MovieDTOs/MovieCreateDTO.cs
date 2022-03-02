using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacterAPI.Models.DTOs.MovieDTOs
{
    public class MovieCreateDTO
    {
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public string Picture { get; set; }
        public string Trailer { get; set; }
    }
}
