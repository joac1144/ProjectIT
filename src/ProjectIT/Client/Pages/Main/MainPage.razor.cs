using ProjectIT.Client.Components.Filter;
using ProjectIT.Shared.Dtos.Projects;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages.Main;

public partial class MainPage
{
    private ICollection<ProjectDetailsDto>? projects;

    private ICollection<ProjectDetailsDto>? shownProjects;

    private IList<FilterTag> Tags { get; set; } = new List<FilterTag>();

    private int projectCardCount;

    protected async override Task OnParametersSetAsync()
    {
        projects = (await anonymousClient.Client.GetFromJsonAsync<IEnumerable<ProjectDetailsDto>>("projects"))?.ToList();
        shownProjects = projects;
    }

    private void FilterPanelsInitialized(IList<FilterTag> data)
    {
        Tags = Tags.Concat(data).ToList();
    }

    private void OnTagClickedInFilterPanel(FilterTag filterTag)
    {
        Tags.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = filterTag.Selected;
    }

    private void FilterProjectsBySearch(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            shownProjects = projects;
        }
        else
        {
            shownProjects = projects?.Where(
                p => p.Topics.Where(t => t.Name == query).Any() 
                || p.Title.Contains(query, StringComparison.OrdinalIgnoreCase) 
                || p.Description.Contains(query, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
    }

    private void UpdateProjectCardCount()
    {

    }
}