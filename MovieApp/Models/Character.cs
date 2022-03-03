using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Models
{
    [Table("Character")]
    public class Character
    {        
        /// <summary>
        /// Defining table for characters.
        /// </summary>
      
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [MaxLength(25)]
        public string Alias { get; set; }
        [MaxLength(50)]
        public string Gender { get; set; }
        [MaxLength(256)]
        public string PictureUrl { get; set; }
        public ICollection<Movie> Movies { get; set; }

    }
}
