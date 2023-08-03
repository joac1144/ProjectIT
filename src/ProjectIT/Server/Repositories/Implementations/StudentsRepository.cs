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
            .ToListAsync();

        return students.Select(p => new StudentDetailsDto
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Email = p.Email,
            Programme = p.Programme
        });
    }

    public async Task<StudentDetailsDto?> ReadByIdAsync(int? id)
    {
        var student = await _context.Students
            .Where(p => p.Id == id)
            .SingleOrDefaultAsync();

        if (student == null)
            return null;

        return new StudentDetailsDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            Programme = student.Programme
        };
    }

    public async Task<StudentDetailsDto?> ReadByUserEmailAsync(string? userEmail)
    {
        var student = await _context.Students
            .Where(p => p.Email == userEmail)
            .SingleOrDefaultAsync();

        if (student == null)
            return null;

        return new StudentDetailsDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            Programme = student.Programme
        };
    }
}
