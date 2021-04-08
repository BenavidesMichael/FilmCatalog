using System.ComponentModel.DataAnnotations;

namespace FilmCatalog.API.Models
{
    public class GenreCreateDTO
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}
