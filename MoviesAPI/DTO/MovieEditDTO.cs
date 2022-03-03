using System.Collections.Generic;
using MoviesAPI.Models;

namespace MoviesAPI.DTO
{
    public class MovieEditDTO
    {
		public int Id { get; set; }
		public string Title { get; set; }
		public string Genre { get; set; }
		public int ReleaseYear { get; set; }
		public string Director { get; set; }
		public string PictureUrl { get; set; }
		public string Trailer { get; set; }
		public int FranchiseId { get; set; }
		public Franchise Franchise { get; set; }
		public ICollection<Character> Characters { get; set; }

	}
}
