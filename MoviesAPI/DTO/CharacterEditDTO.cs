using System.Collections.Generic;
using MoviesAPI.Models;


namespace MoviesAPI.DTO
{
    public class CharacterEditDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string PictureUrl { get; set; }
        public ICollection<Movie> Movies { get; set; }


    }
}
