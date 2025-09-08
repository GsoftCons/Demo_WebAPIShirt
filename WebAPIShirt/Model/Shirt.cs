using System.ComponentModel.DataAnnotations;
using WebAPIShirt.Model.Validation;

namespace WebAPIShirt.Model
{
    public class Shirt
    {
        public int ShirtId { get; set; }

        [Required]
        public string? Brand { get; set; }

        [Required]
        public string? Color { get; set; }

        [Shirt_EnsureCorrectSizing]
        public int Size { get; set; }

        [Required]
        public string? Gender { get; set; }

        public double Price { get; set; }

    }
}
