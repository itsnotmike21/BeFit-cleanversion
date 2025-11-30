using System;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.DTOs
{
    public class TrainingSessionCreateDto
    {
        [Required]
        public DateTime StartedAt { get; set; }

        [Required]
        public DateTime EndedAt { get; set; }
    }
}