using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Models
{
	[Table("Movie")]
	public class Movie
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[MaxLength(150)]
		public string Title { get; set; }
		[MaxLength(120)]
		public string Genre { get; set; }
		public int ReleaseYear { get; set; }
		[MaxLength(50)]
		public string Director { get; set; }
		[MaxLength(256)]
		public string PictureUrl { get; set; }
		[MaxLength(256)]
		public string Trailer { get; set; }
		public int FranchiseId { get; set; }
		public Franchise Franchise { get; set; }
		public ICollection<Character> Characters { get; set; }

	}
}
