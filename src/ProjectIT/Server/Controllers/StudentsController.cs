using Microsoft.AspNetCore.Mvc;
using ProjectIT.Server.Repositories.Interfaces;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Users;

namespace ProjectIT.Server.Controllers;

[ApiController]
[Route(ApiEndpoints.Students)]
public class StudentsController : ControllerBase
{
    private readonly IStudentsRepository _repository;

    public StudentsController(IStudentsRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IEnumerable<StudentDetailsDto>> GetAll()
    {
        return await _repository.ReadAllAsync();
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(StudentDetailsDto), 200)]
    [ProducesResponseType(404)]
    public async Task<StudentDetailsDto?> GetById(int id)
    {
        var student = await _repository.ReadByIdAsync(id);

        if (student == null)
            return null;

        return student;
    }

    [HttpGet("{email}")]
    [ProducesResponseType(typeof(StudentDetailsDto), 200)]
    [ProducesResponseType(404)]
    public async Task<StudentDetailsDto?> GetByEmail(string email)
    {
        var student = await _repository.ReadByUserEmailAsync(email);

        if (student == null)
            return null;

        return student;
    }
}