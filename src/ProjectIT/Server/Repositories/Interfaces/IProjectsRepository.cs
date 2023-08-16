using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Dtos.Users;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Repositories.Interfaces;

public interface IProjectsRepository
{
    Task<IEnumerable<ProjectDetailsDto>> ReadAllAsync();

    Task<ProjectDetailsDto?> ReadByIdAsync(int? id);

    Task<int?> CreateAsync(ProjectCreateDto project);

    Task<int?> UpdateAsync(ProjectUpdateDto project);

    Task<int?> DeleteAsync(int id);

    Task<int?> AddAppliedStudentGroup(int projectId, IEnumerable<StudentDetailsDto> students);
}