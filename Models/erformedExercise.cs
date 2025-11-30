using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class PerformedExercise
    {
        public int Id { get; set; }

        // Klucze obce
        [Required]
        public int TrainingSessionId { get; set; }
        public TrainingSession? TrainingSession { get; set; }

        [Required]
        public int ExerciseTypeId { get; set; }
        public ExerciseType? ExerciseType { get; set; }

        // Parametry wykonania
        [Range(0, double.MaxValue)]
        public double LoadKg { get; set; } // obciążenie w kg

        [Range(1, 1000)]
        public int Sets { get; set; } // liczba serii

        [Range(1, 1000)]
        public int RepsPerSet { get; set; } // liczba powtórzeń w serii
    }
}