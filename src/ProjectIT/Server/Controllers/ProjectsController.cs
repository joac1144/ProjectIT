using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectIT.Server.Repositories;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectsRepository _repository;

    public ProjectsController(IProjectsRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("all")]
    [ProducesResponseType(200)]
    public async Task<IEnumerable<ProjectDetailsDto>> GetAll()
    {
        return await _repository.ReadAllAsync();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProjectDetailsDto), 200)]
    [ProducesResponseType(404)]
    public async Task<ProjectDetailsDto?> GetById(int id)
    {
        var project = await _repository.ReadByIdAsync(id);

        if (project == null) return null;

        return project;
    }

    [HttpPost]
    public ActionResult<Project> Create(ProjectCreateDto data)
    {
        return Ok(data);
    }

    [HttpPut]
    public ActionResult<Project> Update(ProjectUpdateDto data)
    {
        return Ok(data);
    }

    [HttpDelete]
    public ActionResult<int> Delete(int id)
    {
        return Ok();
    }
}