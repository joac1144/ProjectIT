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
                shownProjects = shownProjects?.Where(p => p.Programmes.Where(prog => prog.ToString() == filterTag.Tag).Any()).ToList();
                break;
            case FilterType.ECTS:
                shownProjects = shownProjects?.Where(p => p.Ects.ToString() == filterTag.Tag).ToList();
                break;
            case FilterType.Semester:
                shownProjects = shownProjects?.Where(p => $"{p.Semester?.Season} {p.Semester?.Year}" == filterTag.Tag).ToList();
                break;
            case FilterType.Language:
                shownProjects = shownProjects?.Where(p => p.Languages.Where(lang => lang.ToString() == filterTag.Tag).Any()).ToList();
                break;
        }
    }

    private void OnTagClickedInFilterPanelTopics(FilterTag filterTag)
    {
        Tags.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = filterTag.Selected;
        
        shownProjects = shownProjects?.Where(p => p.Topics.Where(topic => Topics!.Where(ft => ft.Selected).Select(ft => ft.Tag).Contains(topic.Name)).Any()).ToList();
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

    private void ClearFilters()
    {        
        foreach (FilterTag tag in Tags)
        {
            Tags.Where(ft => ft.Tag == tag.Tag).Single().Selected = false;
        }
    }

    private void UpdateProjectCardCount()
    {
        projectCardCount = projectCardCount + 3;
    }
}