using ProjectIT.Client.Constants;

namespace ProjectIT.Client.Shared;

public partial class NavMenu
{
    private string activeTab = PageUrls.Projects;

    private void SetActiveTab(string val) => activeTab = val;
}