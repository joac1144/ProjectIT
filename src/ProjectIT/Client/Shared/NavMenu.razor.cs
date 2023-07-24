using ProjectIT.Client.Constants;

namespace ProjectIT.Client.Shared;

public partial class NavMenu
{
    private string? activeTab;

    protected override void OnInitialized()
    {
        activeTab = GetActiveTabFromUrl(Navigation.Uri);
        base.OnInitialized();
    }

    private void SetActiveTab(string val) => activeTab = val;

    private string GetActiveTabFromUrl(string url)
    {
        if (url.Contains(PageUrls.Projects))
            return PageUrls.Projects;
        else if (url.Contains(PageUrls.Supervisors))
            return PageUrls.Supervisors;
        else if (url.Contains(PageUrls.CreateRequest))
            return PageUrls.CreateRequest;
        else if (url.Contains(PageUrls.CreateProject))
            return PageUrls.CreateProject;

        return PageUrls.Projects;
    }
}