using ProjectIT.Client.Constants;

namespace ProjectIT.Client.Shared;

public partial class NavMenu
{
    private string activeTab = null!;

    protected override void OnInitialized()
    {
        Navigation.NavigateTo(PageUrls.LandingPage);
        activeTab = PageUrls.Projects;
    }

    private void SetActiveTab(string val)
    {
        activeTab = val;
    }
}