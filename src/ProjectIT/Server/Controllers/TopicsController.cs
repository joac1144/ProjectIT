using Microsoft.AspNetCore.Mvc;
using ProjectIT.Shared.Models;
using ProjectIT.Server.Repositories.Interfaces;

namespace ProjectIT.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicsController : ControllerBase
{
    private readonly ITopicsRepository _repository;

    public TopicsController(ITopicsRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IEnumerable<Topic>> GetAll()
    {
        return await _repository.ReadAllAsync();
    }
}