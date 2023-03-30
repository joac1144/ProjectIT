using ProjectIT.Client.Components.Filter;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages.Test;

public partial class TestPage
{
    public IList<FilterTag> Tags { get; set; } = new List<FilterTag>();

    private string? GptResult;

    protected async override Task OnInitializedAsync()
    {
        await GetGptChatCompletion();
    }

    private void FilterPanelsInitialized(IList<FilterTag> data)
    {
        Tags = Tags.Concat(data).ToList();
    }

    private void OnTagClickedInFilterPanel(FilterTag filterTag)
    {
        Tags.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = filterTag.Selected;
    }

    private async Task GetGptChatCompletion()
    {
        var response = await httpClient.PostAsJsonAsync("gpt", "Write a project description about a project which purpose is to make it easier for students to find a relevant project and supervisor for their bachelor project");

        GptResult = await response.Content.ReadAsStringAsync();
    }
}