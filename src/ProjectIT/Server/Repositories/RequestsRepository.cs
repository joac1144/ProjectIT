using Microsoft.EntityFrameworkCore;
using ProjectIT.Server.Database;
using ProjectIT.Shared.Dtos.Requests;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Repositories;

public class RequestRepository : IRequestsRepository
{
    private readonly IProjectITDbContext _context;

    public RequestRepository(IProjectITDbContext context)
    {
        _context = context;
    }

    public async Task<int?> CreateAsync(RequestCreateDto request)
    {
        var entity = new Request
        {
            Title = request.Title,
            Description = request.Description,
            Topics = request.Topics,
            Languages = request.Languages,
            Programmes = request.Programmes,
            Members = request.Members,
            Supervisors = request.Supervisors,
            Ects = request.Ects,
            Semester = request.Semester
        };

        _context.Requests.Add(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public Task<int?> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<RequestDetailsDto>> ReadAllAsync()
    {

        var requests = await _context.Requests
            .Include(p => p.Topics)
            .Include(p => p.Supervisors)
            .Include(p => p.Semester)
            .Include(p => p.Members)
            .ToListAsync();

        return requests.Select(r =>
            new RequestDetailsDto
            {
                Title = r.Title,
                Description = r.Description,
                Topics = r.Topics,
                Languages = r.Languages,
                Programmes = r.Programmes,
                Members = r.Members,
                Supervisors = r.Supervisors,
                Ects = r.Ects,
                Semester = r.Semester
            });

    }
    public async Task<RequestDetailsDto?> ReadByIdAsync(int? id)
    {
        var request = await _context.Requests.FindAsync(id);

        if (request == null) return null;

        return new RequestDetailsDto
        {
            Title = request.Title,
            Description = request.Description,
            Topics = request.Topics,
            Languages = request.Languages,
            Programmes = request.Programmes,
            Members = request.Members,
            Supervisors = request.Supervisors,
            Ects = request.Ects,
            Semester = request.Semester
        };


    }

    public Task<int?> UpdateAsync(RequestUpdateDto request)
    {
        throw new NotImplementedException();
    }
}