using Newtonsoft.Json;

namespace ProjectIT.Shared.Dtos.Gpt;

public record GptResponseDto
{
    [JsonProperty("role")]
    public string? Role { get; set; }

    [JsonProperty("content")]
    public string? Content { get; set; }
}