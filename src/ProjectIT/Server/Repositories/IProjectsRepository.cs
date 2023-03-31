using ProjectIT.Shared.Dtos.Projects;

namespace ProjectIT.Server.Repositories;

public interface IProjectsRepository
{
    Task<IEnumerable<ProjectDetailsDto>> ReadAllAsync();

    Task<ProjectDetailsDto?> ReadByIdAsync(int id);

    Task<int?> CreateAsync(ProjectCreateDto project);

    Task<ProjectUpdateDto> UpdateAsync(ProjectUpdateDto project);

    Task<int?> DeleteAsync(int id);
}