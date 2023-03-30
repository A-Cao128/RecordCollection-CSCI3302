using System.ComponentModel.DataAnnotations;

namespace MyShelf.UI.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        [Required]
        public string GenreName { get; set; } = string.Empty;
    }
}
