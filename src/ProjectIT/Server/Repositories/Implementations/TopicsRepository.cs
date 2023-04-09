using Microsoft.EntityFrameworkCore;
using ProjectIT.Server.Database;
using ProjectIT.Server.Repositories.Interfaces;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Repositories.Implementations;

public class TopicsRepository : ITopicsRepository
{
    private readonly IProjectITDbContext _context;

    public TopicsRepository(IProjectITDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Topic>> ReadAllAsync()
    {
        return await _context.Topics.ToListAsync();
    }
}