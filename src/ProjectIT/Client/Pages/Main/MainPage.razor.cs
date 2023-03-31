using ProjectIT.Client.Components.Filter;
using ProjectIT.Shared.Dtos.Projects;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages.Main;

public partial class MainPage
{
    private ICollection<ProjectDetailsDto>? projects;

    private ICollection<ProjectDetailsDto>? shownProjects;

    private IList<FilterTag> Tags { get; set; } = new List<FilterTag>();

    private IList<FilterTagTopic>? Topics { get; set; }
    private IList<FilterTag>? Programmes { get; set; }
    private IList<FilterTag>? ECTSs { get; set; }
    private IList<FilterTag>? Semesters { get; set; }
    private IList<FilterTag>? Languages { get; set; }

    private int projectCardCount;

    protected async override Task OnInitializedAsync()
    {
        projects = (await anonymousClient.Client.GetFromJsonAsync<IEnumerable<ProjectDetailsDto>>("projects"))?.ToList();
        shownProjects = projects;
    }

    private void FilterPanelInitialized(FilterType type, IList<FilterTag> data)
    {
        switch (type)
        {
            case FilterType.Programme:
                Programmes = data;
                break;
            case FilterType.ECTS:
                ECTSs = data;
                break;
            case FilterType.Semester:
                Semesters = data;
                break;
            case FilterType.Language:
                Languages = data;
                break;
        }
        Tags = Tags.Concat(data).ToList();
    }

    private void FilterPanelTopicsInitialized(IList<FilterTagTopic> data)
    {
        Topics = data;
        Tags = Tags.Concat(data).ToList();
    }

    private void OnTagClickedInFilterPanel(FilterType type, FilterTag filterTag)
    {
        Tags.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = filterTag.Selected;

        switch (type)
        {
            case FilterType.Programme:
                //shownProjects
                break;
            case FilterType.ECTS:
                break;
            case FilterType.Semester:
                break;
            case FilterType.Language:
                break;
        }

        // Filter by topics


        shownProjects = shownProjects?.Where(
            p => p.Topics.Where(t => Topics!.Where(ft => ft.Selected).Select(ft => ft.Tag).Contains(t.Name)).Any()
        ).ToList();
    }

    private void OnTagClickedInFilterPanelTopics(FilterTag filterTag)
    {

    }

    private void FilterProjectsByTopic()
    {
        shownProjects = projects?.Where(
            p => p.Topics.Where(t => Topics!.Where(ft => ft.Selected).Select(ft => ft.Tag).Contains(t.Name)).Any()
        ).ToList();
    }

    private void FilterProjectsBySearch(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            shownProjects = projects;
        }
        else
        {
            shownProjects = shownProjects?.Where(
                p => p.Topics.Where(t => t.Name == query).Any() 
                || p.Title.Contains(query, StringComparison.OrdinalIgnoreCase) 
                || p.Description.Contains(query, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
    }

    private void UpdateProjectCardCount()
    {
        projectCardCount = projectCardCount + 3;
    }
}