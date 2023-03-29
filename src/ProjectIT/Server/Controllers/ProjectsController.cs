using Microsoft.AspNetCore.Mvc;
using ProjectIT.Server.Repositories;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Controllers;

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
    public async Task<IEnumerable<ProjectDetailsDto>> GetAll()
    {
        return await _repository.ReadAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDetailsDto>> GetById(int id)
    {
        var project = await _repository.ReadByIdAsync(id);

        if (project == null) return NotFound();

        return Ok(project);
    }

    [HttpPost]
    public ActionResult<project> Create(ProjectCreateDto data)
    {
        return Ok(data);
    }

    [HttpPut]
    public ActionResult<project> Update(ProjectUpdateDto data)
    {
        return Ok(data);
    }

    [HttpDelete]
    public ActionResult<int> Delete(int id)
    {
        return Ok();
    }
}