using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using ProjectIT.Client.Http;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Client.Components.Filter;

public partial class FilterPanelSimple
{
    [Inject]
    public AnonymousClient anonymousClient { get; set; } = null!;

    [Parameter]
    public string Title { get; init; } = null!;

    [Parameter]
    public IList<FilterTagSimple> Data { get; set; } = new List<FilterTagSimple>();

    [Parameter]
    public EventCallback<FilterTagSimple> DataChanged { get; init; }

    [Parameter]
    public EventCallback<IList<FilterTagSimple>> OnInitializedData { get; set; }

    [Parameter]
    public FilterType Type { get; init; }

    protected override async Task OnInitializedAsync()
    {
        var projects = await anonymousClient.Client.GetFromJsonAsync<IEnumerable<ProjectDetailsDto>>(ApiEndpoints.Projects);
        var semesters = projects!.Select(project => project.Semester).Distinct().Order();

        switch (Type)
        {
            case FilterType.Programme:
                foreach (Programme programme in Enum.GetValues<Programme>())
                {
                    Data.Add(new FilterTagSimple { Tag = programme.ToString(), FilterType = FilterType.Programme });
                }
                break;
            case FilterType.ECTS:
                foreach (Ects ects in Enum.GetValues<Ects>())
                {
                    Data.Add(new FilterTagSimple { Tag = ects.ToString(), FilterType = FilterType.ECTS });
                }
                break;
            case FilterType.Semester:
                foreach (Semester semester in semesters)
                {
                    Data.Add(new FilterTagSimple { Tag = semester.ToString(), FilterType = FilterType.Semester });
                }
                break;
            case FilterType.Language:
                foreach (Language lang in Enum.GetValues<Language>())
                {
                    Data.Add(new FilterTagSimple { Tag = lang.ToString(), FilterType = FilterType.Language });
                }
                break;
            case FilterType.SupervisorStatus:
                foreach (SupervisorStatus st in Enum.GetValues<SupervisorStatus>())
                {
                    Data.Add(new FilterTagSimple { Tag = st.ToString(), FilterType = FilterType.SupervisorStatus });
                }
                break;
        }

        if (OnInitializedData.HasDelegate)
        {
            await OnInitializedData.InvokeAsync(Data);
        }
    }

    private Task CheckboxChecked(ChangeEventArgs e, FilterTagSimple filterTag)
    {
        Data.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = (bool)e.Value!;

        return DataChanged.InvokeAsync(filterTag);
    }
}