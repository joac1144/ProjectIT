using Microsoft.EntityFrameworkCore;
using ProjectIT.Server.Database;
using ProjectIT.Server.Repositories.Interfaces;
using ProjectIT.Shared.Dtos.Requests;
using ProjectIT.Shared.Models;
using Microsoft.IdentityModel.Tokens;

namespace ProjectIT.Server.Repositories.Implementations;

public class RequestRepository : IRequestsRepository
{
    private readonly IProjectITDbContext _context;

    public RequestRepository(IProjectITDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RequestDetailsDto>> ReadAllAsync()
    {
        var requests = await _context.Requests
            .Include(p => p.Topics)
            .Include(p => p.Supervisors)
            .Include(r => r.StudentGroup).ThenInclude(sg => sg.Students)
            .ToListAsync();

        return requests.Select(r =>
            new RequestDetailsDto
            {
                Id = r.Id,
                Title = r.Title,
                DescriptionHtml = r.DescriptionHtml,
                Topics = r.Topics,
                Languages = r.Languages,
                Programmes = r.Programmes,
                StudentGroup = r.StudentGroup,
                Supervisors = r.Supervisors,
                Ects = r.Ects,
                Semester = r.Semester,
                Status = r.Status
            });
    }

    public async Task<RequestDetailsDto?> ReadByIdAsync(int? id)
    {
        var request = await _context.Requests
            .Where(r => r.Id == id)
            .Include(r => r.Topics)
            .Include(r => r.Supervisors)
            .Include(r => r.StudentGroup).ThenInclude(sg => sg.Students)
            .SingleOrDefaultAsync();

        if (request == null) return null;

        return new RequestDetailsDto
        {
            Id = request.Id,
            Title = request.Title,
            DescriptionHtml = request.DescriptionHtml,
            Topics = request.Topics,
            Languages = request.Languages,
            StudentGroup = request.StudentGroup,
            Programmes = request.Programmes,
            Supervisors = request.Supervisors,
            Ects = request.Ects,
            Semester = request.Semester,
            Status = request.Status
        };
    }

    public async Task<int?> CreateAsync(RequestCreateDto request)
    {
        var supervisorEmails = request.SupervisorEmails.ToList();
        var supervisors = _context.Supervisors.Where(s => supervisorEmails.Contains(s.Email)).ToList();
        var topics = new List<Topic>();
        if (request.Topics is not null)
        foreach (var topic in request.Topics)
        {
            topics.Add(_context.Topics.Single(t => t.Name == topic.Name));
        }

        var dbStudents = new List<Student>();

        foreach (var email in request.StudentEmails)
        {
            var student = _context.Students.Single(s => s.Email == email);

            dbStudents.Add(student);
        }

        var studentGroup = new StudentGroup
        {
            Students = dbStudents
        };

        var entity = new Request
        {
            Title = request.Title,
            DescriptionHtml = request.DescriptionHtml,
            Topics = topics,
            Languages = request.Languages,
            Programmes = request.Programmes,
            StudentGroup = studentGroup,
            Supervisors = supervisors,
            Ects = request.Ects,
            Semester = request.Semester,
            Status = request.Status
        };

        if (string.IsNullOrWhiteSpace(entity.Title) || string.IsNullOrWhiteSpace(entity.DescriptionHtml) || entity.Supervisors.IsNullOrEmpty() ||
            entity.Languages.IsNullOrEmpty() || entity.Programmes.IsNullOrEmpty() || entity.Semester is null)
                throw new ArgumentNullException();

        _context.Requests.Add(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }
}