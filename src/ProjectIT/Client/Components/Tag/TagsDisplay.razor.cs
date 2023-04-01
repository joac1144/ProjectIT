using Microsoft.AspNetCore.Components;
using ProjectIT.Client.Components.Filter;

namespace ProjectIT.Client.Components.Tag;

public partial class TagsDisplay
{
    [Parameter]
    public List<FilterTag> Tags { get; set; } = null!;

    [Parameter]
    public EventCallback<List<FilterTag>> TagsChanged { get; set; }

    [Parameter]
    public bool TagsAreClickable { get; set; }

    private Task TagClicked(FilterTag filterTag)
    {
        Tags.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = false;

        return TagsChanged.InvokeAsync(Tags);
    }
}