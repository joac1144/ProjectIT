using ProjectIT.Shared.Dtos.Requests;

namespace ProjectIT.Server.Repositories;

public interface IRequestsRepository
{
    Task<IEnumerable<RequestDetailsDto>> ReadAllAsync();

    Task<RequestDetailsDto?> ReadByIdAsync(int? id);

    Task<int?> CreateAsync(RequestCreateDto request);

    Task<int?> UpdateAsync(RequestUpdateDto request);

    Task<int?> DeleteAsync(int id);
}