using ProjectIT.Client.Components.Filter;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Client.Pages;

public partial class MainPage
{
    private Semester date = new Semester
    {
        Season = Season.Spring,
        Year = 2023,
    };

    private Ects ects = Ects.Bachelor;

    private Supervisor supervisor = new Supervisor { FullName = "Jannick Vestergaard" };

    private Supervisor coSupervisor = new Supervisor { FullName = "Morten Østervortesen" };

    private IEnumerable<Programme> educations = new[]
    {
        Programme.BSWU,
        Programme.BDS
    };

    private string description = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga.";

    private string title = "ProjectIT - A very beautiful platform solving ITU's problems";

    public IList<FilterTag> Tags { get; set; } = new List<FilterTag>();
    
    public IList<Project> Projects { get; set; } = new List<Project>();

    protected async override Task OnInitializedAsync()
    {
        //TODO Fetch projects from database.
        await Task.CompletedTask;
    }

    private void FilterPanelsInitialized(IList<FilterTag> data)
    {
        Tags = Tags.Concat(data).ToList();
    }

    private void OnTagClickedInFilterPanel(FilterTag filterTag)
    {
        Tags.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = filterTag.Selected;
    }
}