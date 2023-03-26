using Microsoft.AspNetCore.Components;
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

    private void ApplyProject(NavigationManager navigationManager)
    {
        navigationManager.NavigateTo("/");
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        for (int i = 0; i < 5; i++)
        {
            Tags.Add
            (
                new FilterTag
                {
                    Tag = "Tags " + i
                }
            );
        }

    }

}