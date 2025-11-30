using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BeFit.Models
{
    public class TrainingSession : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Rozpoczęcie sesji")]
        public DateTime StartedAt { get; set; }

        [Required]
        [Display(Name = "Zakończenie sesji")]
        public DateTime EndedAt { get; set; }

        // Powiązanie z użytkownikiem
        [Display(Name = "Użytkownik")]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        // (opcjonalne) lista wykonanych ćwiczeń w tej sesji
        public ICollection<PerformedExercise>? PerformedExercises { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndedAt < StartedAt)
            {
                yield return new ValidationResult(
                    "Data zakończenia nie może być wcześniejsza niż data rozpoczęcia.",
                    new[] { nameof(EndedAt), nameof(StartedAt) }
                );
            }
        }
    }
}