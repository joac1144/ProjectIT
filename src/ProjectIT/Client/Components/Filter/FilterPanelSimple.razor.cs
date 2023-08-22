using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using ProjectIT.Client.Http;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Extensions;
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
                    Data.Add(new FilterTagSimple { Tag = ((Enum)Enum.Parse(typeof(Programme), programme.ToString())).GetTranslatedString(EnumsLocalizer), FilterType = FilterType.Programme });
                }
                break;
            case FilterType.ECTS:
                foreach (Ects ects in Enum.GetValues<Ects>())
                {
                    Data.Add(new FilterTagSimple { Tag = ((Enum)Enum.Parse(typeof(Ects), ects.ToString())).GetTranslatedString(EnumsLocalizer), FilterType = FilterType.ECTS });
                }
                break;
            case FilterType.Semester:
                foreach (Semester semester in semesters)
                {
                    Data.Add(new FilterTagSimple { Tag = $"{((Enum)Enum.Parse(typeof(Season), semester.Season.ToString())).GetTranslatedString(EnumsLocalizer)} {semester.Year}", FilterType = FilterType.Semester });
                }
                break;
            case FilterType.Language:
                foreach (Language lang in Enum.GetValues<Language>())
                {
                    Data.Add(new FilterTagSimple { Tag = ((Enum)Enum.Parse(typeof(Language), lang.ToString())).GetTranslatedString(EnumsLocalizer), FilterType = FilterType.Language });
                }
                break;
            case FilterType.SupervisorStatus:
                foreach (SupervisorStatus status in Enum.GetValues<SupervisorStatus>())
                {
                    Data.Add(new FilterTagSimple { Tag = ((Enum)Enum.Parse(typeof(SupervisorStatus), status.ToString())).GetTranslatedString(EnumsLocalizer), FilterType = FilterType.SupervisorStatus });
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