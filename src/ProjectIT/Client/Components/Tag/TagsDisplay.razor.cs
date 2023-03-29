using Microsoft.AspNetCore.Components;
using ProjectIT.Client.Components.Filter;

namespace ProjectIT.Client.Components.Tag;

public partial class TagsDisplay
{
    [Parameter]
    public IList<FilterTag> Tags { get; set; } = null!;

    [Parameter]
    public EventCallback<IList<FilterTag>> TagsChanged { get; set; }

    private Task TagClicked(FilterTag filterTag)
    {
        Tags.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = false;

        return TagsChanged.InvokeAsync(Tags);
    }

    private Task ClearFilters()
    {        
        foreach (FilterTag tag in Tags)
        {
            Tags.Where(ft => ft.Tag == tag.Tag).Single().Selected = false;
        }
        return TagsChanged.InvokeAsync(Tags);
    }
}