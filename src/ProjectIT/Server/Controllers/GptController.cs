using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;
using OpenAI_API.Models;

namespace ProjectIT.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class GptController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private readonly string? _openAiApiKey;

    public GptController(IConfiguration configuration)
    {
        _configuration = configuration;
        _openAiApiKey = _configuration["OpenAiApiKey"];
    }

    [HttpPost]
    public async Task<IActionResult> ChatCompletion([FromBody]string query)
    {
        string answer = string.Empty;
        var openai = new OpenAIAPI(_openAiApiKey);

        CompletionRequest completion = new CompletionRequest();
        completion.Prompt = query;
        completion.Model = Model.ChatGPTTurbo;
        completion.MaxTokens = 4000;
        var result = await openai.Chat.CreateChatCompletionAsync(query);
        if (result != null)
        {
            return Ok(result.Choices[0].Message);
        }
        else
        {
            return BadRequest("Not found");
        }
    }
}