using Microsoft.AspNetCore.Mvc;
using ProjectIT.Server.Repositories.Interfaces;
using ProjectIT.Shared.Dtos.Requests;

namespace ProjectIT.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RequestsController : ControllerBase
{
    private readonly IRequestsRepository _repository;

    public RequestsController(IRequestsRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IEnumerable<RequestDetailsDto>> GetAll()
    {
        return await _repository.ReadAllAsync();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RequestDetailsDto), 200)]
    [ProducesResponseType(404)]
    public async Task<RequestDetailsDto?> GetById(int id)
    {
        var request = await _repository.ReadByIdAsync(id);

        if (request == null)
            return null;

        return request;
    }

    [HttpPost]
    public async Task<int?> Post(RequestCreateDto request)
    {
        return await _repository.CreateAsync(request);
    }
}
