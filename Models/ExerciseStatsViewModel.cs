namespace BeFit.Models
{
    // Model tylko do wyświetlenia statystyk - nie mapujemy go do bazy
    public class ExerciseStatsViewModel
    {
        public int ExerciseTypeId { get; set; }
        public string ExerciseTypeName { get; set; } = string.Empty;
        public int TimesPerformed { get; set; }
        public int TotalRepetitions { get; set; }
        public double? AverageWeight { get; set; }
        public double? MaxWeight { get; set; }
    }
}