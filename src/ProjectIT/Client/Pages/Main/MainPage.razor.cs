using ProjectIT.Client.Components.Filter;
using ProjectIT.Shared.Dtos.Projects;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages.Main;

public partial class MainPage
{
    private IEnumerable<ProjectDetailsDto>? projects { get; set; }

    private IList<FilterTag> Tags { get; set; } = new List<FilterTag>();
    
    protected async override Task OnInitializedAsync()
    {
        projects = await httpClient.GetFromJsonAsync<IEnumerable<ProjectDetailsDto>>("projects");
    }

    private void FilterPanelsInitialized(IList<FilterTag> data)
    {
        Tags = Tags.Concat(data).ToList();
    }

    private void OnTagClickedInFilterPanel(FilterTag filterTag)
    {
        Tags.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = filterTag.Selected;
    }
}