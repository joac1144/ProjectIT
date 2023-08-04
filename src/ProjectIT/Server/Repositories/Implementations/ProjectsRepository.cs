using Microsoft.EntityFrameworkCore;
using ProjectIT.Server.Database;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Server.Repositories.Interfaces;
using ProjectIT.Shared.Models;
using Microsoft.IdentityModel.Tokens;

namespace ProjectIT.Server.Repositories.Implementations;

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
            Id = p.Id,
            Title = p.Title,
            DescriptionHtml = p.DescriptionHtml,
            Topics = p.Topics,
            Languages = p.Languages,
            Programmes = p.Programmes,
            Ects = p.Ects,
            Semester = p.Semester,
            Supervisor = p.Supervisor,
            CoSupervisor = p.CoSupervisor,
            Students = p.Students
        });
    }

    public async Task<ProjectDetailsDto?> ReadByIdAsync(int? id)
    {
        var project = await _context.Projects
            .Where(p => p.Id == id)
            .Include(p => p.Topics)
            .Include(p => p.Supervisor)
            .Include(p => p.CoSupervisor)
            .Include(p => p.Students)
            .SingleOrDefaultAsync();

        if (project == null)
            return null;

        return new ProjectDetailsDto
        {
            Id = project.Id,
            Title = project.Title,
            DescriptionHtml = project.DescriptionHtml,
            Topics = project.Topics,
            Languages = project.Languages,
            Programmes = project.Programmes,
            Ects = project.Ects,
            Semester = project.Semester,
            Supervisor = project.Supervisor,
            CoSupervisor = project.CoSupervisor,
            Students = project.Students
        };
    }

    public async Task<int?> CreateAsync(ProjectCreateDto project)
    {
        Supervisor supervisor = _context.Supervisors.Single(s => s.Email == project.SupervisorEmail);
        Supervisor? coSupervisor = _context.Supervisors.SingleOrDefault(s => s.Email == project.CoSupervisorEmail);
        var topics = new List<Topic>();
        if (project.Topics != null)
        foreach (var topic in project.Topics)
        {
            var dbTopic = _context.Topics.SingleOrDefault(t => t.Name == topic.Name);
            if (dbTopic == null)
            {
                topics.Add(new Topic { Name = topic.Name, Category = topic.Category });
            }
            else
            {
                topics.Add(dbTopic);
            }
        }

        var entity = new Project
        {
            Title = project.Title,
            DescriptionHtml = project.DescriptionHtml,
            Topics = topics,
            Languages = project.Languages,
            Programmes = project.Programmes,
            Ects = project.Ects,
            Semester = project.Semester,
            Supervisor = supervisor,
            CoSupervisor = coSupervisor
        };

        if (string.IsNullOrWhiteSpace(entity.Title) || string.IsNullOrWhiteSpace(entity.DescriptionHtml) ||
            entity.Languages.IsNullOrEmpty() || entity.Programmes.IsNullOrEmpty() || entity.Semester is null || entity.Supervisor is null)
                throw new ArgumentNullException();
        
        _context.Projects.Add(entity);
        await _context.SaveChangesAsync();
        
        return entity.Id;
    }

    public async Task<int?> UpdateAsync(ProjectUpdateDto project)
    {
        var foundProject = await _context.Projects.FindAsync(project.Id);

        if (foundProject == null) return null;

        foundProject.Title = project.Title;
        foundProject.DescriptionHtml = project.DescriptionHtml;
        foundProject.Topics = project.Topics;
        foundProject.Languages = project.Languages;
        foundProject.Programmes = project.Programmes;
        foundProject.Ects = project.Ects;
        foundProject.Semester = project.Semester;
        foundProject.Supervisor = project.Supervisor;
        foundProject.CoSupervisor = project.CoSupervisor;

        await _context.SaveChangesAsync();

        return project.Id;
    }

    public async Task<int?> DeleteAsync(int id)
    {
        var project = await _context.Projects
            .Where(project => project.Id == id)
            .Include(p => p.Topics)
            .SingleOrDefaultAsync();

        if (project == null)
            return null;

        _context.Projects.Remove(project);

        await _context.SaveChangesAsync();

        return project.Id;
    }
}