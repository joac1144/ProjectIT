using ProjectIT.Client.Components.Filter;
using ProjectIT.Client.Components.Search;
using ProjectIT.Shared.Dtos.Projects;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages.Main;

public partial class MainPage
{
    private List<ProjectDetailsDto> projects = new();

    private List<ProjectDetailsDto> shownProjects = new();

    private List<FilterTag> tags = new();

    private readonly List<FilterTag>? _activeTopics = new();
    private readonly List<FilterTag>? _activeProgrammes = new();
    private readonly List<FilterTag>? _activeECTSs = new();
    private readonly List<FilterTag>? _activeSemesters = new();
    private readonly List<FilterTag>? _activeLanguages = new();

    private SearchField? searchField;

    protected async override Task OnInitializedAsync()
    {
        projects = (await anonymousClient.Client.GetFromJsonAsync<IEnumerable<ProjectDetailsDto>>("projects"))?.ToList()!;
        shownProjects = projects;
    }

    private void FilterPanelInitialized(IList<FilterTag> data)
    {
        tags = tags.Concat(data).ToList();
    }

    private void FilterPanelTopicsInitialized(IList<FilterTagTopic> data)
    {
        tags = tags.Concat(data).ToList();
    }

    private void OnTagClickedInFilterPanel(FilterType type, FilterTag filterTag)
    {
        tags.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = filterTag.Selected;

        if (filterTag.Selected)
        {
            switch (type)
            {
                case FilterType.Programme:
                    _activeProgrammes?.Add(filterTag);
                    break;
                case FilterType.ECTS:
                    _activeECTSs?.Add(filterTag);
                    break;
                case FilterType.Semester:
                    _activeSemesters?.Add(filterTag);
                    break;
                case FilterType.Language:
                    _activeLanguages?.Add(filterTag);
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case FilterType.Programme:
                    _activeProgrammes?.Remove(filterTag);
                    break;
                case FilterType.ECTS:
                    _activeECTSs?.Remove(filterTag);
                    break;
                case FilterType.Semester:
                    _activeSemesters?.Remove(filterTag);
                    break;
                case FilterType.Language:
                    _activeLanguages?.Remove(filterTag);
                    break;
            }
        }

        FilterProjects(searchField?.SearchString);
    }

    private void OnTagClickedInFilterPanelTopics(FilterTagTopic filterTag)
    {
        tags.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = filterTag.Selected;

        if (filterTag.Selected)
        {
            _activeTopics?.Add(filterTag);
        }
        else
        {
            _activeTopics?.Remove(filterTag);
        }
    }

    private void FilterProjects(string? searchFieldQuery)
    {
        List<ProjectDetailsDto> filteredByLanguages = new();
        List<ProjectDetailsDto> filteredBySemesters = new();
        List<ProjectDetailsDto> filteredByEcts = new();
        List<ProjectDetailsDto> filteredByProgrammes = new();
        List<ProjectDetailsDto> filteredByTopics = new();
        List<ProjectDetailsDto>? filteredBySearch = new();

        foreach (FilterTag filterTag in _activeLanguages!)
        {
            filteredByLanguages = projects!
                .Intersect(projects?.Where(project => project.Languages.Select(lang => lang.ToString()).Contains(filterTag.Tag))!)
                .Union(filteredByLanguages)
                .ToList();
        }
        foreach (FilterTag filterTag in _activeSemesters!)
        {
            filteredBySemesters = projects!
                .Intersect(projects?.Where(project => project.Semester?.ToString() == filterTag.Tag)!)
                .Union(filteredBySemesters)
                .ToList();
        }
        foreach (FilterTag filterTag in _activeECTSs!)
        {
            filteredByEcts = projects!
                .Intersect(projects?.Where(project => project.Ects.ToString() == filterTag.Tag)!)
                .Union(filteredByEcts)
                .ToList();
        }
        foreach (FilterTag filterTag in _activeProgrammes!)
        {
            filteredByProgrammes = projects!
                .Intersect(projects?.Where(project => project.Programmes.Select(prog => prog.ToString()).Contains(filterTag.Tag))!)
                .Union(filteredByProgrammes)
                .ToList();
        }
        foreach (FilterTag filterTag in _activeTopics!)
        {
            filteredByTopics = projects!
                .Intersect(projects?.Where(project => project.Topics.Select(topic => topic.Name).Contains(filterTag.Tag))!)
                .Union(filteredByTopics)
                .ToList();
        }

        filteredBySearch = FilterProjectsBySearch(searchFieldQuery);

        filteredByLanguages = filteredByLanguages.Any() ? filteredByLanguages : projects;
        filteredBySemesters = filteredBySemesters.Any() ? filteredBySemesters : projects;
        filteredByEcts = filteredByEcts.Any() ? filteredByEcts : projects;
        filteredByProgrammes = filteredByProgrammes.Any() ? filteredByProgrammes : projects;
        filteredByTopics = filteredByTopics.Any() ? filteredByTopics : projects;

        shownProjects = projects
            .Intersect(filteredByLanguages)
            .Intersect(filteredBySemesters)
            .Intersect(filteredByEcts)
            .Intersect(filteredByProgrammes)
            .Intersect(filteredByTopics)
            .Intersect(filteredBySearch!)
            .ToList();
    }

    private List<ProjectDetailsDto>? FilterProjectsBySearch(string? query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return projects;
        }
        else
        {
            return projects.Where(
                p => p.Topics.Where(t => t.Name == query).Any()
                || p.Title.Contains(query, StringComparison.OrdinalIgnoreCase)
                || p.Description.Contains(query, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
    }

    private void ClearFilters()
    {        
        tags.ForEach(ft => ft.Selected = false);
        _activeLanguages?.Clear();
        _activeSemesters?.Clear();
        _activeECTSs?.Clear();
        _activeProgrammes?.Clear();
        _activeTopics?.Clear();
        FilterProjects(searchField?.SearchString);
    }
}