using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using ProjectIT.Shared;

namespace ProjectIT.Server.Controllers;

[ApiController]
[Route(ApiEndpoints.Gpt)]
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
    public async Task<IActionResult> ChatCompletion([FromBody] string query)
    {
        var openai = new OpenAIAPI(_openAiApiKey);

        ChatRequest chatRequest = new()
        {
            Model = Model.ChatGPTTurbo,
            Messages = new ChatMessage[] { new(ChatMessageRole.User, query) }
        };
        var result = await openai.Chat.CreateChatCompletionAsync(chatRequest);
        if (result != null)
        {
            return Ok(result.Choices[0].Message.Content);
        }
        else
        {
            return BadRequest("Bad request");
        }
    }
}