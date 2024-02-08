using System.Text.Json.Serialization;

namespace TestPointReport.Models
{
    public class Point
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("assignedTo")]
        public AssignedTo AssignedTo { get; set; }

        [JsonPropertyName("automated")]
        public bool Automated { get; set; }

        [JsonPropertyName("configuration")]
        public Configuration Configuration { get; set; }

        [JsonPropertyName("lastTestRun")]
        public LastTestRun LastTestRun { get; set; }

        [JsonPropertyName("lastResult")]
        public LastResult LastResult { get; set; }

        [JsonPropertyName("outcome")]
        public string Outcome { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("lastResultState")]
        public string LastResultState { get; set; }

        [JsonPropertyName("testCase")]
        public TestCase TestCase { get; set; }

        [JsonPropertyName("workItemProperties")]
        public List<WorkItemProperty> WorkItemProperties { get; set; }

        [JsonPropertyName("lastResultDetails")]
        public LastResultDetail LastResultDetails { get; set; }

        [JsonPropertyName("lastRunBuildNumber")]
        public string LastRunBuildNumber { get; set; }
    }

    public class AssignedTo
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class Configuration
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class LastTestRun
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class LastResult
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class TestCase
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("webUrl")]
        public string WebUrl { get; set; }
    }

    public class WorkItemProperty
    {
        [JsonPropertyName("workItem")]
        public WorkItem WorkItem { get; set; }
    }

    public class WorkItem
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class LastResultDetail
    {
        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("dateCompleted")]
        public DateTime DateCompleted { get; set; }

        [JsonPropertyName("runBy")]
        public RunBy RunBy { get; set; }
    }

    public class RunBy
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
    public class PointsContainer
    {
        [JsonPropertyName("value")]
        public List<Point> Value { get; set; }
    }
}
