using Microsoft.EntityFrameworkCore;
using ProjectIT.Server.Database;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Repositories;

public class ProjectsRepository : IProjectsRepository
{
    private readonly IProjectITDbContext _context;

    public ProjectsRepository(IProjectITDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectDetailsDto>> ReadAllAsync()
    {
        var projects = await _context.Projects
            .Include(p => p.Topics)
            .Include(p => p.Supervisor)
            .Include(p => p.CoSupervisor)
            .Include(p => p.Students)
            .ToListAsync();

        return projects.Select(p => new ProjectDetailsDto
        {
            Title = p.Title,
            Description = p.Description,
            Topics = p.Topics,
            Languages = p.Languages,
            Educations = p.Educations,
            Ects = p.Ects,
            Semester = p.Semester,
            Supervisor = p.Supervisor,
            CoSupervisor = p.CoSupervisor,
            Students = p.Students
        });
    }

    public async Task<ProjectDetailsDto> ReadByIdAsync(int id)
    {
        var project = await _context.Projects
            .Include(p => p.Topics)
            .Include(p => p.Supervisor)
            .Include(p => p.CoSupervisor)
            .Include(p => p.Students)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project == null)
        {
            throw new ArgumentException($"Project with ID {id} was not found!");
        }

        return new ProjectDetailsDto
        {
            Title = project.Title,
            Description = project.Description,
            Topics = project.Topics,
            Languages = project.Languages,
            Educations = project.Educations,
            Ects = project.Ects,
            Semester = project.Semester,
            Supervisor = project.Supervisor,
            CoSupervisor = project.CoSupervisor,
            Students = project.Students
        };
    }
    
    public async Task<ProjectCreateDto> CreateAsync(ProjectCreateDto projectCreateDto)
    {
        throw new NotImplementedException();
    }

    public async Task<ProjectUpdateDto> UpdateAsync(ProjectUpdateDto projectUpdateDto)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}