using ProjectIT.Shared.Dtos.Users;

namespace ProjectIT.Server.Repositories.Interfaces;

public interface IStudentsRepository
{
    Task<IEnumerable<StudentDetailsDto>> ReadAllAsync();

    Task<StudentDetailsDto?> ReadByIdAsync(int? id);

    Task<StudentDetailsDto?> ReadByUserEmailAsync(string? userEmail);
}