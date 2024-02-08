namespace TestPointReport.Models
{
    public class TestPointsContainer
    {
        public List<TestPoint> Value { get; set; }
    }

    public class TestPoint
    {
        public int Id { get; set; }
        public string TestPlanName { get; set; }
        public Tester Tester { get; set; }
        public ConfigurationForPlan ConfigurationForPlan { get; set; }
        public bool IsAutomated { get; set; }
        public Project Project { get; set; }
        public TestCasePlan TestPlan { get; set; }
        public TestSuite TestSuite { get; set; }
        public TestCasePlan TestCase { get; set; }
        public LastUpdatedBy LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public Results Results { get; set; }
        public DateTime LastResetToActive { get; set; }
        public bool IsActive { get; set; }
        public Links Links { get; set; }
        public TestCaseReference TestCaseReference { get; set; }
        public string OutcomeDisplay { get; set; } // New property
    }

    public class Tester
    {
        public string DisplayName { get; set; }
        public string Url { get; set; }
        public Links1 _links { get; set; }
        public string Id { get; set; }
        public string UniqueName { get; set; }
        public string ImageUrl { get; set; }
        public string Descriptor { get; set; }
    }

    public class Links1
    {
        public Avatar Avatar { get; set; }
    }

    public class Avatar
    {
        public string Href { get; set; }
    }

    public class ConfigurationForPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Visibility { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }

    public class TestPlanIdName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TestCasePlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TestSuite
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class LastUpdatedBy
    {
        public string DisplayName { get; set; }
        public string Url { get; set; }
        public Links2 _links { get; set; }
        public string Id { get; set; }
        public string UniqueName { get; set; }
        public string ImageUrl { get; set; }
        public string Descriptor { get; set; }
    }

    public class Links2
    {
        public Avatar Avatar { get; set; }
    }

    public class Results
    {
        //public LastResultDetails LastResultDetails { get; set; }
        public string State { get; set; }
        public string Outcome { get; set; }
        public string LastResultState { get; set; }
        public string RunByDisplayName { get; set; }
        public string RunById { get; set; }
        public string isActive { get; set; } // New property
    }

    //public class LastResultDetails
    //{
    //    public int Duration { get; set; }
    //    public DateTime DateCompleted { get; set; }
    //    public RunBy RunBy { get; set; }
    //}

    //public class RunBy
    //{
    //    public string DisplayName { get; set; }
    //    public string Id { get; set; }
    //}

    public class Links3
    {
        public Self _self { get; set; }
        public SourcePlan SourcePlan { get; set; }
        public SourceSuite SourceSuite { get; set; }
        public SourceProject SourceProject { get; set; }
        public TestCases TestCases { get; set; }
        public Run Run { get; set; }
        public Result Result { get; set; }
    }

    public class Self
    {
        public string Href { get; set; }
    }

    public class SourcePlan
    {
        public string Href { get; set; }
    }

    public class SourceSuite
    {
        public string Href { get; set; }
    }

    public class SourceProject
    {
        public string Href { get; set; }
    }

    public class TestCases
    {
        public string Href { get; set; }
    }

    public class Run
    {
        public string Href { get; set; }
    }

    public class Result
    {
        public string Href { get; set; }
    }

    public class TestCaseReference
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
    }

    public class Links
    {
        public Self _self { get; set; }
        public SourcePlan1 SourcePlan { get; set; }
        public SourceSuite1 SourceSuite { get; set; }
        public SourceProject1 SourceProject { get; set; }
        public TestCases1 TestCases { get; set; }
    }

    public class Self1
    {
        public string Href { get; set; }
    }

    public class SourcePlan1
    {
        public string Href { get; set; }
    }

    public class SourceSuite1
    {
        public string Href { get; set; }
    }

    public class SourceProject1
    {
        public string Href { get; set; }
    }

    public class TestCases1
    {
        public string Href { get; set; }
    }
}
