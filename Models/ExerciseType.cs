using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExerciseType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)] // ograniczenie długości nazwy
        public string Name { get; set; } = string.Empty;
    }
}