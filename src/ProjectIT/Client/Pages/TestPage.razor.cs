using ProjectIT.Client.Components.Filter;
using ProjectIT.Shared.Dtos.Gpt;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages;

public partial class TestPage
{
    public IList<FilterTag> Tags { get; set; } = new List<FilterTag>();

    private string? GptResult; 

    private void FilterPanelsInitialized(IList<FilterTag> data)
    {
        Tags = Tags.Concat(data).ToList();
    }

    private async Task OnTagClickedInFilterPanel(FilterTag filterTag)
    {
        Tags.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = filterTag.Selected;

        var response = await HttpClient.PostAsJsonAsync("gpt", "Write a project description about a project which purpose is to make it easier for students to find a relevant project and supervisor for their bachelor project");

        GptResult = (await response.Content.ReadFromJsonAsync<GptResponseDto>())?.Content;
    }
}