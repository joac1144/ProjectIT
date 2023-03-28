using ProjectIT.Client.Components.Filter;
using ProjectIT.Client.Utilities;

namespace ProjectIT.Client.Pages;

public partial class TestPage
{
    public IList<FilterTag> Tags { get; set; } = new List<FilterTag>();

    private string? GptResult;

    private readonly GptClient _gptClient;

    public TestPage()
    {
    }    

    public TestPage(GptClient gptClient)
    {
        _gptClient = gptClient;
    }

    private void FilterPanelsInitialized(IList<FilterTag> data)
    {
        Tags = Tags.Concat(data).ToList();
    }

    private void OnTagClickedInFilterPanel(FilterTag filterTag)
    {
        Tags.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = filterTag.Selected;

        var response = _gptClient.GenerateText(new (string, string)[] { ("user", "What are class diagrams?") });
    }
}