using Microsoft.AspNetCore.Components;
using ProjectIT.Shared.Enums;

namespace ProjectIT.Client.Components.Filter;

public partial class FilterPanelSimple
{
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

    protected override void OnInitialized()
    {
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
                DateTime date = DateTime.Now;
                foreach (Season season in Enum.GetValues<Season>())
                {
                    Data.Add(new FilterTagSimple { Tag = $"{season} {date.Year}", FilterType = FilterType.Semester });
                    Data.Add(new FilterTagSimple { Tag = $"{season} {date.Year + 1}", FilterType = FilterType.Semester });
                }
                break;
            case FilterType.Language:
                foreach (Language lang in Enum.GetValues<Language>())
                {
                    Data.Add(new FilterTagSimple { Tag = lang.ToString(), FilterType = FilterType.Language });
                }
                break;
        }

        if (OnInitializedData.HasDelegate)
        {
            OnInitializedData.InvokeAsync(Data);
        }
    }

    private Task CheckboxChecked(ChangeEventArgs e, FilterTagSimple filterTag)
    {
        Data.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = (bool)e.Value!;

        return DataChanged.InvokeAsync(filterTag);
    }
}