using MoviesAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class GenreCreationDTO
    {
        [Required(ErrorMessage = "The field with {0} is required")]
        [FirstLetterUpperCaseAttribute]
        public string Name { get; set; }
    }
}
