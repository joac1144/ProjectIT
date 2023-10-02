using System.Net.Http.Json;
using System.Security.Claims;
using ProjectIT.Client.Constants;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Users;

namespace ProjectIT.Client.Shared;

public partial class NavMenu
{
    private string? activeTab;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Task.Delay(1000); // simulate authorization process
            activeTab = GetActiveTabFromUrl(Navigation.Uri);
            StateHasChanged(); // re-render the component
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    private void SetActiveTab(string val)
    {
        activeTab = val;
        StateHasChanged(); // re-render the component
    }

    private string GetActiveTabFromUrl(string url)
    {
        if (url.Contains(PageUrls.Projects))
            return PageUrls.Projects;
        else if (url.Contains(PageUrls.Supervisors))
            return PageUrls.Supervisors;
        else if (url.Contains(PageUrls.MyRequests) || url.Contains(PageUrls.CreateRequest))
            return PageUrls.MyRequests;
        else if (url.Contains(PageUrls.MyProjects))
            return PageUrls.MyProjects;
        else if (url.Contains(PageUrls.AppliedProjects))
            return PageUrls.AppliedProjects;
        else if (url.Contains(PageUrls.MyProfile))
            return PageUrls.MyProfile;

        return string.Empty;
    }
}