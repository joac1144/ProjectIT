using Microsoft.AspNetCore.Components;
using ProjectIT.Client.Components.Filter;
using ProjectIT.Client.Components.Search;
using ProjectIT.Shared.Dtos.Topics;
using ProjectIT.Shared.Dtos.Users;
using ProjectIT.Shared.Models;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages.SupervisorsList;

public partial class SupervisorList
{
    private List<SupervisorDetailsDto> supervisors = new();

    private List<SupervisorDetailsDto> filteredSupervisors = new();
    private List<SupervisorDetailsDto> shownSupervisors = new();

    private List<FilterTag> tags = new();
    private List<FilterTagTopic> activeTopics = new();

    private SearchField? searchField;

    private int pageSize = 10;
    private int totalPages;
    private int currentPage;

    protected async override Task OnInitializedAsync()
    {
        supervisors = (await anonymousClient.Client.GetFromJsonAsync<IEnumerable<SupervisorDetailsDto>>("supervisors"))?.ToList()!;
        filteredSupervisors = supervisors;
        UpdateSupervisors(0);
    }

    private void UpdateSupervisors(int pageNumber)
    {
        shownSupervisors = filteredSupervisors.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        totalPages = (int)Math.Ceiling(filteredSupervisors.Count() / (decimal)pageSize);
        currentPage = pageNumber;
    }

    private void NewPage(string buttonType)
    {
        if (buttonType == "next" && currentPage != totalPages - 1) currentPage++;

        if (buttonType == "prev" && currentPage != 0) currentPage--;

        UpdateSupervisors(currentPage);
    }

    private void FilterPanelTopicsInitialized(IList<FilterTagTopic> data)
    {
        tags = tags.Concat(data).ToList();
    }

    private void OnTagClickedInTagsDisplay(FilterTag filterTag)
    {
        RemoveTagFromActives(filterTag);

        FilterSupervisors();
    }

    private void RemoveTagFromActives(FilterTag filterTag)
    {
        activeTopics = activeTopics.Where(ft => ft.Tag != filterTag.Tag).ToList();
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

        FilterSupervisors();
    }

    private void FilterSupervisors()
    {
        List<SupervisorDetailsDto> filteredByTopics = supervisors;
        List<SupervisorDetailsDto>? filteredBySearch = supervisors;

        if (activeTopics.Any())
        {
            filteredByTopics = new List<SupervisorDetailsDto>();
            foreach (FilterTag filterTag in activeTopics)
            {
                filteredByTopics = supervisors!
                    .Intersect(supervisors?.Where(supervisor => supervisor.Topics.Select(topic => topic.Name).Contains(filterTag.Tag))!)
                    .Union(filteredByTopics)
                    .ToList();
            }
        }

        filteredBySearch = FilterSupervisorsBySearch(searchField?.SearchString);

        filteredSupervisors = supervisors
            .Intersect(filteredByTopics)
            .Intersect(filteredBySearch!)
            .ToList();

        UpdateSupervisors(0);
    }

    private List<SupervisorDetailsDto>? FilterSupervisorsBySearch(string? query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return supervisors;
        }
        else
        {
            return supervisors.Where(
                p => p.Topics.Any(topic => topic.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                || p.FullName.Contains(query, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
    }

    private void ClearFilters()
    {
        tags.ForEach(ft => ft.Selected = false);
        activeTopics.Clear();
        FilterSupervisors();
    }
}