using ProjectIT.Client.Components.Filter;

namespace ProjectIT.Client.Pages;

public partial class TestPage
{
    public IList<FilterTag> Tags { get; set; } = new List<FilterTag>();

    private void FilterPanelsInitialized(IList<FilterTag> data)
    {
        Tags = Tags.Concat(data).ToList();
    }

    private void OnTagClickedInFilterPanel(FilterTag filterTag)
    {
        Tags.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = filterTag.Selected;
    }
}