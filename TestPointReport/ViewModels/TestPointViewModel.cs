using System.ComponentModel;

namespace TestPointReport.ViewModels
{
    public class TestPointViewModel
    {
        public int Id { get; set; }
        [DisplayName("Plan Name")]
        public string PlanName { get; set; }
        [DisplayName("Suite Name")]
        public string SuiteName { get; set; }
        [DisplayName("Test Case")]
        public string TestCaseName { get; set; }
        [DisplayName("Tester Name")]
        public string TesterName { get; set; }
        [DisplayName("Outcome Date")]
        public string ResultsDateCompletedDate { get; set; }

        public string ResultsState { get; set; }
        
        public string ResultsOutcome { get; set; }

        [DisplayName("Active")]
        public string isActive { get; set; }

        [DisplayName("Outcome")]
        public string OutcomeDisplay { get; set; }
        
        [DisplayName("Test Plan Id")]
        public int TestPlanId { get; set; }
        [DisplayName("Suite Id")]
        public int SuiteId { get; set; }
        [DisplayName("Last Result State")]
        public string LastResultState { get; set; }
        [DisplayName("Test Results")]
        public TestResultViewModel TestResult { get; set; }
    }
}
