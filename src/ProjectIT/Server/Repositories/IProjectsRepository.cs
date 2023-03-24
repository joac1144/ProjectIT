using ProjectIT.Shared.Dtos.ProjectDto;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Repositories;

public interface IProjectsRepository
{
    IEnumerable<Project> ReadAll();

    Project ReadById(int id);

    ProjectCreateDto Create(ProjectCreateDto projectCreateDto);

    ProjectUpdateDto Update(ProjectUpdateDto projectUpdateDto);

    int Delete(int id);
}