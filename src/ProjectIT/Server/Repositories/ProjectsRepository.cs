using ProjectIT.Server.Database;
using ProjectIT.Shared.Dtos.ProjectDto;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Repositories;

public class ProjectsRepository : IProjectsRepository
{
    private readonly IProjectITDbContext _context;

    public ProjectsRepository(IProjectITDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Project> ReadAll()
    {
        return _context.Projects;
    }

    public Project ReadById(int id)
    {
        throw new NotImplementedException();
    }
    
    public ProjectCreateDto Create(ProjectCreateDto projectCreateDto)
    {
        throw new NotImplementedException();
    }

    public ProjectUpdateDto Update(ProjectUpdateDto projectUpdateDto)
    {
        throw new NotImplementedException();
    }

    public int Delete(int id)
    {
        throw new NotImplementedException();
    }
}