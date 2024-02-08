namespace TestPointReport.ViewModels
{
    public class PointViewModel
    {
        public string PlanName { get; set; }
        public string TestCaseName { get; set; }
        public string OutcomeDisplay { get; set; }
        public string TestCaseId { get; set; }
        public string ResultsDateCompletedDate { get; set; }
        public string TesterName { get; set; }
        public string? TestPlanId { get; set; } // Change to nullable int
        public string webUrl { get; set; } // Change to string
    }
}
