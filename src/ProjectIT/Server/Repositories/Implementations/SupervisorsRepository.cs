using Microsoft.EntityFrameworkCore;
using ProjectIT.Server.Database;
using ProjectIT.Shared.Dtos.Users;
using ProjectIT.Server.Repositories.Interfaces;
using ProjectIT.Shared.Models;

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
         var supervisors = await _context.Users.OfType<Supervisor>()
            .Include(p => p.Topics)
            .ToListAsync();

        return supervisors.Select(p => new SupervisorDetailsDto
        {
            Id = p.Id,
            FullName = p.FullName,
            Email = p.Email,
            Topics = p.Topics,
            Profession = p.Profession,
            Programme = p.Programme
        });

    }

    public async Task<SupervisorDetailsDto?> ReadByIdAsync(int? id)
    {
        var supervisor = await _context.Users.OfType<Supervisor>()
            .Where(p => p.Id == id)
            .Include(p => p.Topics)
            .SingleOrDefaultAsync();

        if (supervisor == null)
            return null;

        return new SupervisorDetailsDto
        {
            Id = supervisor.Id,
            FullName = supervisor.FullName,
            Email = supervisor.Email,
            Topics = supervisor.Topics,
            Profession = supervisor.Profession,
            Programme = supervisor.Programme
        };
    }
}