using ProjectIT.Server.Repositories;

namespace Controller.Tests;

public class ProjectsControllerTests
{
    private readonly IProjectsRepository _repository;

    public ProjectsControllerTests(IProjectsRepository repository)
    {
        _repository = repository;
    }

    [Fact]
    public void Test1()
    {

    }
}