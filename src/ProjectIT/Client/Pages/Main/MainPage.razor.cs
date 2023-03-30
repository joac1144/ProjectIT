using ProjectIT.Client.Components.Filter;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Models;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages.Main;

public partial class MainPage
{
    private ICollection<ProjectDetailsDto>? projects;

    private ICollection<ProjectDetailsDto>? shownProjects;

    private IList<FilterTag> Tags { get; set; } = new List<FilterTag>();

    private IList<FilterTagTopic> Topics { get; set; }
    private IList<FilterTag> Programmes { get; set; }
    private IList<FilterTag> ECTS { get; set; }
    private IList<FilterTag> Semester { get; set; }
    private IList<FilterTag> Languages { get; set; }

    private int projectCardCount;

    protected async override Task OnInitializedAsync()
    {
        projects = (await anonymousClient.Client.GetFromJsonAsync<IEnumerable<ProjectDetailsDto>>("projects"))?.ToList();
        shownProjects = projects;
    }

    private void FilterPanelInitialized(IList<FilterTag> data)
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