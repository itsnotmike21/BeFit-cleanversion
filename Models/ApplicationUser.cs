using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BeFit.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Dodatkowe pola użytkownika (przykład)
        public string? FullName { get; set; }

        // Nawigacje do sesji i wykonań
        public ICollection<TrainingSession>? TrainingSessions { get; set; }
        public ICollection<PerformedExercise>? PerformedExercises { get; set; }
    }
}