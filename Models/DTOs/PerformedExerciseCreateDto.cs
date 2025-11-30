using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.DTOs
{
    public class PerformedExerciseCreateDto
    {
        [Required]
        public int TrainingSessionId { get; set; }

        [Required]
        public int ExerciseTypeId { get; set; }

        [Range(0, double.MaxValue)]
        public double LoadKg { get; set; }

        [Range(1, 1000)]
        public int Sets { get; set; }

        [Range(1, 1000)]
        public int RepsPerSet { get; set; }
    }
}