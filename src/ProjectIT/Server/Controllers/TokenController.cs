using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectIT.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TokenController : ControllerBase
{
    private readonly IConfiguration Configuration;

    public TokenController(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    [HttpGet]
    public string? Get() => Configuration["OpenAiApiKey"];
}