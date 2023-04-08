using ProjectIT.Shared.Dtos.Requests;

namespace ProjectIT.Server.Repositories.Interfaces;

public interface IRequestsRepository
{
    Task<IEnumerable<RequestDetailsDto>> ReadAllAsync();

    Task<RequestDetailsDto?> ReadByIdAsync(int? id);

    Task<int?> CreateAsync(RequestCreateDto request);
}