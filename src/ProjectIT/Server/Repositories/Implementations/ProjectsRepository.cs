using Microsoft.EntityFrameworkCore;
using ProjectIT.Server.Database;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Server.Repositories.Interfaces;
using ProjectIT.Shared.Models;
using Microsoft.IdentityModel.Tokens;
using ProjectIT.Shared.Dtos.Users;

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
            .Include(p => p.AppliedStudentGroups)!.ThenInclude(sg => sg.Students)
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
            AppliedStudentGroups = p.AppliedStudentGroups
        });
    }

    public async Task<ProjectDetailsDto?> ReadByIdAsync(int? id)
    {
        var project = await _context.Projects
            .Where(p => p.Id == id)
            .Include(p => p.Topics)
            .Include(p => p.Supervisor)
            .Include(p => p.CoSupervisor)
            .Include(p => p.AppliedStudentGroups)!.ThenInclude(sg => sg.Students)
            .SingleOrDefaultAsync();

        if (project == null)
            return null;

        return new ProjectDetailsDto
        {
            Id = project.Id,
            Title = project.Title,
            DescriptionHtml = project.DescriptionHtml,
            Topics = (project.Topics is not null && project.Topics.Any()) ? project.Topics : null,
            Languages = project.Languages,
            Programmes = project.Programmes,
            Ects = project.Ects,
            Semester = project.Semester,
            Supervisor = project.Supervisor,
            CoSupervisor = project.CoSupervisor,
            AppliedStudentGroups = project.AppliedStudentGroups
        };
    }

    public async Task<int?> CreateAsync(ProjectCreateDto project)
    {
        Supervisor supervisor = _context.Supervisors.Single(s => s.Email == project.SupervisorEmail);
        Supervisor? coSupervisor = _context.Supervisors.SingleOrDefault(s => s.Email == project.CoSupervisorEmail);
        var topics = new List<Topic>();
        if (project.Topics != null)
        {
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
        }

        var entity = new Project
        {
            Title = project.Title,
            DescriptionHtml = project.DescriptionHtml,
            Topics = topics.Any() ? topics : null,
            Languages = project.Languages,
            Programmes = project.Programmes,
            Ects = project.Ects,
            Semester = project.Semester,
            Supervisor = supervisor,
            CoSupervisor = coSupervisor
        };

        if (string.IsNullOrWhiteSpace(entity.Title) || 
            string.IsNullOrWhiteSpace(entity.DescriptionHtml) ||
            entity.Languages.IsNullOrEmpty() || 
            entity.Programmes.IsNullOrEmpty() || 
            entity.Semester is null || 
            entity.Supervisor is null)
            throw new ArgumentNullException();
        
        _context.Projects.Add(entity);
        await _context.SaveChangesAsync();
        
        return entity.Id;
    }

    public async Task<int?> UpdateAsync(ProjectUpdateDto project)
    {
        var foundProject = await _context.Projects
            .Where(p => p.Id == project.Id)
            .Include(p => p.Topics)
            .Include(p => p.Supervisor)
            .Include(p => p.CoSupervisor)
            .Include(p => p.AppliedStudentGroups)
            .SingleOrDefaultAsync();

        if (foundProject == null) return null;

        if (foundProject.CoSupervisor?.Email != project.CoSupervisor?.Email)
            foundProject.CoSupervisor = _context.Supervisors.SingleOrDefault(s => s.Email == project.CoSupervisor!.Email);

        if (project.Topics != null)
        { 
            var topics = new List<Topic>();
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
            foundProject.Topics = topics.Any() ? topics : null;
        }

        if (string.IsNullOrWhiteSpace(project.Title) || 
            string.IsNullOrWhiteSpace(project.DescriptionHtml) ||
            project.Languages.IsNullOrEmpty() || 
            project.Programmes.IsNullOrEmpty() || 
            project.Semester is null)
            throw new ArgumentNullException();

        foundProject.Title = project.Title;
        foundProject.DescriptionHtml = project.DescriptionHtml;
        foundProject.Languages = project.Languages;
        foundProject.Programmes = project.Programmes;
        foundProject.Ects = project.Ects;
        foundProject.Semester = project.Semester;
        foundProject.AppliedStudentGroups = project.AppliedStudentGroups;

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

    public async Task<int?> AddAppliedStudentGroup(int projectId, IEnumerable<StudentDetailsDto> students)
    {
        var project = await _context.Projects
            .Where(p => p.Id == projectId)
            .Include(p => p.Topics)
            .Include(p => p.Supervisor)
            .Include(p => p.CoSupervisor)
            .Include(p => p.AppliedStudentGroups)
            .SingleOrDefaultAsync();

        if (project == null) return null;

        var dbStudents = new List<Student>();

        foreach (var student in students)
        {
            var st = _context.Students.Single(s => s.Id == student.Id);

            dbStudents.Add(st);
        }

        var existingStudentGroups = project.AppliedStudentGroups is null ? new List<StudentGroup>() : project.AppliedStudentGroups.ToList();

        existingStudentGroups.Add(new StudentGroup
        {
            Students = dbStudents
        });

        project.AppliedStudentGroups = existingStudentGroups;

        await _context.SaveChangesAsync();

        return project.Id;
    }
}