namespace TestPointReport.Models
{
    public class TestPlan
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectUrl { get; set; }
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public string Iteration { get; set; }
        public string Owner { get; set; }
        public int Revision { get; set; }
        public string State { get; set; }
        public string RootSuiteId { get; set; }
        public string ClientUrl { get; set; }
    }
}
