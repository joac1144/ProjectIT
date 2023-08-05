using ProjectIT.Shared.Dtos.Projects;

namespace ProjectIT.Server.Repositories.Interfaces;

public interface IProjectsRepository
{
    Task<IEnumerable<ProjectDetailsDto>> ReadAllAsync();

    Task<ProjectDetailsDto?> ReadByIdAsync(int? id);

    Task<int?> CreateAsync(ProjectCreateDto project);

    Task<int?> UpdateAsync(ProjectUpdateDto project);

    Task<int?> UpdateByApplicantAsync(ProjectUpdateByApplicantsDto project);
    
    Task<int?> DeleteAsync(int id);
}