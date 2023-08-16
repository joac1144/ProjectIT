using Microsoft.EntityFrameworkCore;
using ProjectIT.Server.Database;
using ProjectIT.Shared.Dtos.Users;
using ProjectIT.Server.Repositories.Interfaces;

namespace ProjectIT.Server.Repositories.Implementations;

public class StudentsRepository : IStudentsRepository
{
    private readonly IProjectITDbContext _context;

    public StudentsRepository(IProjectITDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StudentDetailsDto>> ReadAllAsync()
    {
        var students = await _context.Students
            .Include(s => s.AppliedProjects)
            .ToListAsync();

        return students.Select(p => new StudentDetailsDto
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Email = p.Email,
            Programme = p.Programme,
            AppliedProjects = p.AppliedProjects
        });
    }

    public async Task<StudentDetailsDto?> ReadByIdAsync(int? id)
    {
        var student = await _context.Students
            .Where(p => p.Id == id)
            .Include(s => s.AppliedProjects)
            .SingleOrDefaultAsync();

        if (student == null)
            return null;

        return new StudentDetailsDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            Programme = student.Programme,
            AppliedProjects = student.AppliedProjects
        };
    }

    public async Task<StudentDetailsDto?> ReadByUserEmailAsync(string? userEmail)
    {
        var student = await _context.Students
            .Where(p => p.Email == userEmail)
            .Include(s => s.AppliedProjects)
            .SingleOrDefaultAsync();

        if (student == null)
            return null;

        return new StudentDetailsDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            Programme = student.Programme,
            AppliedProjects = student.AppliedProjects
        };
    }
}