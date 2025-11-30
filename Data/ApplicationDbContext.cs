using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BeFit.Models;

namespace BeFit.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ExerciseType> ExerciseTypes { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }
        public DbSet<PerformedExercise> PerformedExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Dodatkowe ograniczenia/relacje (opcjonalnie, ale czytelnie)
            builder.Entity<ExerciseType>()
                .Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Entity<PerformedExercise>()
                .HasOne(pe => pe.TrainingSession)
                .WithMany() // w razie potrzeby zmień na .WithMany(s => s.PerformedExercises)
                .HasForeignKey(pe => pe.TrainingSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PerformedExercise>()
                .HasOne(pe => pe.ExerciseType)
                .WithMany()
                .HasForeignKey(pe => pe.ExerciseTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}