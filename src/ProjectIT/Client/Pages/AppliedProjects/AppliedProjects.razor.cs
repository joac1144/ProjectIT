using System.Net.Http.Json;
using System.Security.Claims;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Projects;

namespace ProjectIT.Client.Pages.AppliedProjects;

public partial class AppliedProjects
{
    private List<ProjectDetailsDto>? projects = new();
    private List<ProjectDetailsDto>? appliedProjects = new();
    private ClaimsPrincipal? authUser;


    protected override async Task OnInitializedAsync() 
    {
        projects = (await anonymousClient.Client.GetFromJsonAsync<IEnumerable<ProjectDetailsDto>>(ApiEndpoints.Projects))?.ToList()!;

        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;

        string userEmail = authUser?.FindFirst("preferred_username")?.Value!;

        appliedProjects = projects.Where(p => p.Students.Any(s => s.Email == userEmail)).ToList();
    }
}

