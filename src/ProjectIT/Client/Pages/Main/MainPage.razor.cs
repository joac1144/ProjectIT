using ProjectIT.Client.Components.Filter;
using ProjectIT.Client.Components.Search;
using ProjectIT.Shared.Dtos.Projects;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages.Main;

public partial class MainPage
{
    private List<ProjectDetailsDto> projects = new();
    private List<Sort> sortValues = Enum.GetValues<Sort>().ToList();
    private string? sortValue;
    private string sortSemester = Sort.Semester.ToString();

    private List<ProjectDetailsDto> filteredProjects = new();
    private List<ProjectDetailsDto> shownProjects = new();

    private List<FilterTag> tags = new();

    private List<FilterTagTopic> activeTopics = new();
    private readonly List<FilterTagSimple> _activeProgrammes = new();
    private readonly List<FilterTagSimple> _activeECTSs = new();
    private readonly List<FilterTagSimple> _activeSemesters = new();
    private readonly List<FilterTagSimple> _activeLanguages = new();

    private SearchField? searchField;

    private int pageSize = 10;
    private int totalPages;
    private int currentPage;

    protected async override Task OnInitializedAsync()
    {
        projects = (await anonymousClient.Client.GetFromJsonAsync<IEnumerable<ProjectDetailsDto>>("https://localhost:7094/projects"))?.ToList()!;
        filteredProjects = projects;
        OnSort(sortSemester);
    }

    private void UpdateProjects(int pageNumber)
    {
        shownProjects = filteredProjects.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        totalPages = (int)Math.Ceiling(filteredProjects.Count() / (decimal)pageSize);
        currentPage = pageNumber;
    }

    private void NewPage(string buttonType)
    {
        if (buttonType == "next" && currentPage != totalPages - 1) currentPage++;

        if (buttonType == "prev" && currentPage != 0) currentPage--;

        UpdateProjects(currentPage);
    }

    private void FilterPanelInitialized(IList<FilterTagSimple> data)
    {
        tags = tags.Concat(data).ToList();
    }

    private void FilterPanelTopicsInitialized(IList<FilterTagTopic> data)
    {
        tags = tags.Concat(data).ToList();
    }

    private void OnTagClickedInTagsDisplay(FilterTag filterTag)
    {
        RemoveTagFromActives(filterTag);

        FilterProjects();
    }

    private void AddTagToActives(FilterTagSimple filterTag)
    {
        switch (filterTag.FilterType)
        {
            case FilterType.Programme:
                _activeProgrammes.Add(filterTag);
                break;
            case FilterType.ECTS:
                _activeECTSs.Add(filterTag);
                break;
            case FilterType.Semester:
                _activeSemesters.Add(filterTag);
                break;
            case FilterType.Language:
                _activeLanguages.Add(filterTag);
                break;
        }
    }

    private void RemoveTagFromActives(FilterTag filterTag)
    {
        if (filterTag.GetType() == typeof(FilterTagTopic))
        {
            activeTopics = activeTopics.Where(ft => ft.Tag != filterTag.Tag).ToList();
        }
        else
        {
            switch (((FilterTagSimple)filterTag).FilterType)
            {
                case FilterType.Programme:
                    _activeProgrammes.Remove((FilterTagSimple)filterTag);
                    break;
                case FilterType.ECTS:
                    _activeECTSs.Remove((FilterTagSimple)filterTag);
                    break;
                case FilterType.Semester:
                    _activeSemesters.Remove((FilterTagSimple)filterTag);
                    break;
                case FilterType.Language:
                    _activeLanguages.Remove((FilterTagSimple)filterTag);
                    break;
            }
        }
    }

    private void OnFilterPanelTagChanged(FilterTagSimple filterTag)
    {
        tags.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = filterTag.Selected;

        if (filterTag.Selected)
        {
            AddTagToActives(filterTag);
        }
        else
        {
            RemoveTagFromActives(filterTag);
        }

        FilterProjects();
    }

    private void OnFilterPanelTopicsTagChanged(FilterTagTopic filterTag)
    {
        tags.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = filterTag.Selected;
        
        if (filterTag.Selected)
        {
            activeTopics.Add(filterTag);
        }
        else
        {
            activeTopics.Remove(filterTag);
        }

        FilterProjects();
    }

    private void FilterProjects()
    {
        List<ProjectDetailsDto> filteredByLanguages = projects;
        List<ProjectDetailsDto> filteredBySemesters = projects;
        List<ProjectDetailsDto> filteredByEcts = projects;
        List<ProjectDetailsDto> filteredByProgrammes = projects;
        List<ProjectDetailsDto> filteredByTopics = projects;
        List<ProjectDetailsDto>? filteredBySearch = projects;

        if (_activeLanguages.Any())
        {
            filteredByLanguages = new List<ProjectDetailsDto>();
            foreach (FilterTag filterTag in _activeLanguages)
            {
                filteredByLanguages = projects!
                    .Intersect(projects?.Where(project => project.Languages.Select(lang => lang.ToString()).Contains(filterTag.Tag))!)
                    .Union(filteredByLanguages)
                    .ToList();
            }
        }
        if (_activeSemesters.Any())
        {
            filteredBySemesters = new List<ProjectDetailsDto>();
            foreach (FilterTag filterTag in _activeSemesters)
            {
                filteredBySemesters = projects!
                    .Intersect(projects?.Where(project => project.Semester?.ToString() == filterTag.Tag)!)
                    .Union(filteredBySemesters)
                    .ToList();
            }
        }
        if (_activeECTSs.Any())
        {
            filteredByEcts = new List<ProjectDetailsDto>();
            foreach (FilterTag filterTag in _activeECTSs)
            {
                filteredByEcts = projects!
                    .Intersect(projects?.Where(project => project.Ects.ToString() == filterTag.Tag)!)
                    .Union(filteredByEcts)
                    .ToList();
            }
        }
        if (_activeProgrammes.Any())
        {
            filteredByProgrammes = new List<ProjectDetailsDto>();
            foreach (FilterTag filterTag in _activeProgrammes)
            {
                filteredByProgrammes = projects!
                    .Intersect(projects?.Where(project => project.Programmes.Select(prog => prog.ToString()).Contains(filterTag.Tag))!)
                    .Union(filteredByProgrammes)
                    .ToList();
            }
        }
        if (activeTopics.Any())
        {
            filteredByTopics = new List<ProjectDetailsDto>();
            foreach (FilterTag filterTag in activeTopics)
            {
                filteredByTopics = projects!
                    .Intersect(projects?.Where(project => project.Topics.Select(topic => topic.Name).Contains(filterTag.Tag))!)
                    .Union(filteredByTopics)
                    .ToList();
            }
        }

        filteredBySearch = FilterProjectsBySearch(searchField?.SearchString);

        filteredProjects = projects
            .Intersect(filteredByLanguages)
            .Intersect(filteredBySemesters)
            .Intersect(filteredByEcts)
            .Intersect(filteredByProgrammes)
            .Intersect(filteredByTopics)
            .Intersect(filteredBySearch!)
            .ToList();

        UpdateProjects(0);
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
                p => p.Topics.Any(topic => topic.Name.Equals(query, StringComparison.OrdinalIgnoreCase))
                || p.Title.Contains(query, StringComparison.OrdinalIgnoreCase)
                || p.DescriptionHtml.Contains(query, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
    }

    private void ClearFilters()
    {
        tags.ForEach(ft => ft.Selected = false);
        _activeLanguages.Clear();
        _activeSemesters.Clear();
        _activeECTSs.Clear();
        _activeProgrammes.Clear();
        activeTopics.Clear();
        FilterProjects();
        OnSort(sortValue ?? nameof(Sort.Semester));
    }


    private void OnSort(object value) 
    {
        if (filteredProjects != null && value.GetType() == typeof(string))
        {
            switch (value)
            {
                case nameof(Sort.Semester):
                    filteredProjects = filteredProjects.OrderBy(p => p.Semester).ToList();
                    break;
                case nameof(Sort.ECTS):
                    filteredProjects = filteredProjects.OrderBy(p => p.Ects).ToList();
                    break;
            }
            UpdateProjects(currentPage);
        }
    }
}