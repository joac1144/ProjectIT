using Microsoft.AspNetCore.Mvc;
using ProjectIT.Server.Repositories;
using ProjectIT.Shared.Dtos.ProjectDto;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController : Controller
{
    private readonly IProjectsRepository _repository;
    
    public ProjectsController(IProjectsRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Project>> GetAll()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<Project> GetById(int id)
    {
        return Ok();
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