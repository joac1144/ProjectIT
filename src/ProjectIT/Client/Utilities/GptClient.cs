using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ProjectIT.Client.Utilities;

public class GptClient
{
    private readonly HttpClient _httpClient;
    private const string openAiApiPath = "https://api.openai.com/v1/";

    public GptClient(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri(openAiApiPath);


        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        
    }

    public async Task<string> GenerateText((string, string)[] messages)
    {
        var token = await _httpClient.GetFromJsonAsync<string>("token");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsJsonAsync($"{openAiApiPath}chat/completions", new
        {
            model = "gpt-3.5-turbo",
            messages
        });
        return await response.Content.ReadAsStringAsync();
    }
}