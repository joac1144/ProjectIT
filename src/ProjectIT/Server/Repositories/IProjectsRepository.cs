using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Repositories;

public interface IProjectsRepository
{
    Task<IEnumerable<ProjectDetailsDto>> ReadAllAsync();

    Task<ProjectDetailsDto> ReadByIdAsync(int id);

    Task<ProjectCreateDto> CreateAsync(ProjectCreateDto projectCreateDto);

    Task<ProjectUpdateDto> UpdateAsync(ProjectUpdateDto projectUpdateDto);

    Task<int> DeleteAsync(int id);
}