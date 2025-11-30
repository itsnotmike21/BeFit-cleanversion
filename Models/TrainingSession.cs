using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class TrainingSession : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartedAt { get; set; }

        [Required]
        public DateTime EndedAt { get; set; }

        // Walidacja: koniec nie może być wcześniejszy niż start
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