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
            .Include(r => r.Student)
            .Include(p => p.Supervisors)
            .Include(p => p.ExtraMembers)
            .ToListAsync();

        return requests.Select(r =>
            new RequestDetailsDto
            {
                Title = r.Title,
                DescriptionHtml = r.DescriptionHtml,
                Topics = r.Topics,
                Languages = r.Languages,
                Programmes = r.Programmes,
                ExtraMembers = r.ExtraMembers,
                Student = r.Student,
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
            .Include(r => r.Student)
            .Include(r => r.Supervisors)
            .Include(r => r.ExtraMembers)
            .SingleOrDefaultAsync();

        if (request == null) return null;

        return new RequestDetailsDto
        {
            Title = request.Title,
            DescriptionHtml = request.DescriptionHtml,
            Topics = request.Topics,
            Languages = request.Languages,
            Student = request.Student,
            Programmes = request.Programmes,
            ExtraMembers = request.ExtraMembers,
            Supervisors = request.Supervisors,
            Ects = request.Ects,
            Semester = request.Semester,
            Status = request.Status
        };
    }

    public async Task<int?> CreateAsync(RequestCreateDto request)
    {
        Student student = _context.Students.Single(s => s.Email == request.StudentEmail);
        var supervisorEmails = request.SupervisorEmails.ToList();
        var supervisors = _context.Supervisors.Where(s => supervisorEmails.Contains(s.Email)).ToList();
        var ExtraMembersEmails = request.ExtraMembersEmails?.ToList();
        var ExtraMembers = _context.Students.Where(s => ExtraMembersEmails != null && ExtraMembersEmails.Contains(s.Email)).ToList();
        var topics = new List<Topic>();
        if (request.Topics is not null)
        foreach (var topic in request.Topics)
        {
            topics.Add(_context.Topics.Single(t => t.Name == topic.Name));
        }

        var entity = new Request
        {
            Title = request.Title,
            DescriptionHtml = request.DescriptionHtml,
            Topics = topics,
            Languages = request.Languages,
            Programmes = request.Programmes,
            Student = student,
            ExtraMembers = ExtraMembers,
            Supervisors = supervisors,
            Ects = request.Ects,
            Semester = request.Semester,
            Status = request.Status
        };

        if (string.IsNullOrWhiteSpace(entity.Title) || string.IsNullOrWhiteSpace(entity.DescriptionHtml) ||
            entity.Languages.IsNullOrEmpty() || entity.Programmes.IsNullOrEmpty() || entity.Semester is null || entity.Student is null)
                throw new ArgumentNullException();

        _context.Requests.Add(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }
}