using ProjectIT.Server.Database;

namespace Repository.Tests;

public class ProjectsRepositoryTests : IDisposable
{
    private readonly IProjectITDbContext _context;

    public ProjectsRepositoryTests(IProjectITDbContext context)
    {
        _context = context;
    }

    [Fact]
    public void Test1()
    {

    }

    public void Dispose()
    {
        _context.Dispose();
    }
}