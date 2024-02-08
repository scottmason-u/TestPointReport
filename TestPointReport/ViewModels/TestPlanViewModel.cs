public class TestPlanViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public TestPlanProjectViewModel Project { get; set; }
    public TestPlanAreaViewModel Area { get; set; }
    public string Iteration { get; set; }
    public string Owner { get; set; }
    public int Revision { get; set; }
    public string State { get; set; }
    public TestPlanSuiteViewModel RootSuite { get; set; }
    public string ClientUrl { get; set; }
}

public class TestPlanProjectViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}

public class TestPlanAreaViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class TestPlanSuiteViewModel
{
    public string Id { get; set; }
}
public class TestPlansContainerViewModel
{
    public List<TestPlanViewModel> Value { get; set; }
}