using ProjectIT.Shared.Dtos.Users;

namespace ProjectIT.Server.Repositories.Interfaces;

public interface ISupervisorsRepository
{
    Task<IEnumerable<SupervisorDetailsDto>> ReadAllAsync();

    Task<SupervisorDetailsDto?> ReadByIdAsync(int? id);
}