using MoviesAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field with {0} is required")]
        [FirstLetterUpperCaseAttribute]
        public string Name { get; set; }

    }
}
