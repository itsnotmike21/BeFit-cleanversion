using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class PerformedExercise
    {
        public int Id { get; set; }

        // Klucze obce
        [Required]
        [Display(Name = "Sesja treningowa", Description = "Wybierz sesję treningową (data)")]
        public int TrainingSessionId { get; set; }
        public TrainingSession? TrainingSession { get; set; }

        [Required]
        [Display(Name = "Typ ćwiczenia", Description = "Wybierz rodzaj ćwiczenia")]
        public int ExerciseTypeId { get; set; }
        public ExerciseType? ExerciseType { get; set; }

        // Powiązanie z użytkownikiem
        [Display(Name = "Użytkownik")]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        // Parametry wykonania
        [Range(0, double.MaxValue)]
        [Display(Name = "Obciążenie (kg)", Description = "Obciążenie w kilogramach")]
        public double LoadKg { get; set; } // obciążenie w kg

        [Range(1, 1000)]
        [Display(Name = "Liczba serii", Description = "Ile serii wykonano")]
        public int Sets { get; set; } // liczba serii

        [Range(1, 1000)]
        [Display(Name = "Powtórzenia w serii", Description = "Liczba powtórzeń w pojedynczej serii")]
        public int RepsPerSet { get; set; } // liczba powtórzeń w serii
    }
}