using Microsoft.AspNetCore.Components;
using ProjectIT.Shared.Enums;

namespace ProjectIT.Client.Components.Filter;

public partial class FilterPanelSimple
{
    [Parameter]
    public string Title { get; init; } = null!;

    [Parameter]
    public IList<FilterTag> Data { get; set; } = new List<FilterTag>();

    [Parameter]
    public EventCallback<FilterTag> DataChanged { get; init; }

    [Parameter]
    public EventCallback<IList<FilterTag>> OnInitializedData { get; set; }

    [Parameter]
    public FilterType Type { get; init; }

    protected override void OnInitialized()
    {
        switch (Type)
        {
            case FilterType.Programme:
                foreach (Programme programme in Enum.GetValues<Programme>())
                {
                    Data.Add(new FilterTag { Tag = programme.ToString() });
                }
                break;
            case FilterType.ECTS:
                foreach (Ects ects in Enum.GetValues<Ects>())
                {
                    Data.Add(new FilterTag { Tag = ects.ToString() });
                }
                break;
            case FilterType.Semester:
                DateTime date = DateTime.Now;
                foreach (Season season in Enum.GetValues<Season>())
                {
                    Data.Add(new FilterTag { Tag = $"{season} {date.Year}" });
                    Data.Add(new FilterTag { Tag = $"{season} {date.Year + 1}" });
                }
                break;
            case FilterType.Language:
                foreach (Language lang in Enum.GetValues<Language>())
                {
                    Data.Add(new FilterTag { Tag = lang.ToString() });
                }
                break;
        }

        if (OnInitializedData.HasDelegate)
        {
            OnInitializedData.InvokeAsync(Data);
        }
    }

    private Task CheckboxChecked(ChangeEventArgs e, FilterTag filterTag)
    {
        Data.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = (bool)e.Value!;

        return DataChanged.InvokeAsync(filterTag);
    }
}