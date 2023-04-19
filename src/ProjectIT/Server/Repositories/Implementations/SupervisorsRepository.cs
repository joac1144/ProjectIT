using Microsoft.EntityFrameworkCore;
using ProjectIT.Server.Database;
using ProjectIT.Shared.Dtos.Users;
using ProjectIT.Server.Repositories.Interfaces;

namespace ProjectIT.Server.Repositories.Implementations;

public class SupervisorsRepository : ISupervisorsRepository
{
    private readonly IProjectITDbContext _context;

    public SupervisorsRepository(IProjectITDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SupervisorDetailsDto>> ReadAllAsync()
    {
         var supervisors = await _context.Supervisors
            .Include(p => p.Topics)
            .ToListAsync();

        return supervisors.Select(p => new SupervisorDetailsDto
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Email = p.Email,
            Topics = p.Topics,
            Profession = p.Profession,
            Status = p.Status
        });
    }

    public async Task<SupervisorDetailsDto?> ReadByIdAsync(int? id)
    {
        var supervisor = await _context.Supervisors
            .Where(p => p.Id == id)
            .Include(p => p.Topics)
            .SingleOrDefaultAsync();

        if (supervisor == null)
            return null;

        return new SupervisorDetailsDto
        {
            Id = supervisor.Id,
            FirstName = supervisor.FirstName,
            LastName = supervisor.LastName,
            Email = supervisor.Email,
            Topics = supervisor.Topics,
            Profession = supervisor.Profession,
            Status = supervisor.Status
        };
    }
}