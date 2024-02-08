namespace TestPointReport.ViewModels
{
    public class TestResultViewModel
    {
        public string State { get; set; }
        public string Outcome { get; set; }
        public int Count { get; set; }

        // Additional properties for the second API call
        public int TestRunId { get; set; }
        public int LastResultId { get; set; }
    }
}
