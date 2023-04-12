using Microsoft.AspNetCore.Mvc;
using ProjectIT.Server.Repositories.Interfaces;
using ProjectIT.Shared.Dtos.Users;

namespace ProjectIT.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class SupervisorsController : ControllerBase
{
    private readonly ISupervisorsRepository _repository;

    public SupervisorsController(ISupervisorsRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IEnumerable<SupervisorDetailsDto>> GetAll()
    {
        return await _repository.ReadAllAsync();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SupervisorDetailsDto), 200)]
    [ProducesResponseType(404)]
    public async Task<SupervisorDetailsDto?> GetById(int id)
    {
        var supervisor = await _repository.ReadByIdAsync(id);

        if (supervisor == null) 
            return null;

        return supervisor;
    }
}