using ProjectIT.Server.Database;
using ProjectIT.Server.Repositories;

namespace Controller.Tests;

public class ProjectsControllerTests : IDisposable
{
    private readonly ProjectITDbContext _context = null!;
    private readonly ProjectsRepository _repository = null!;


    [Fact]
    public void Test1()
    {

    }

    public void Dispose()
    {
        _context.Dispose();
    }
}