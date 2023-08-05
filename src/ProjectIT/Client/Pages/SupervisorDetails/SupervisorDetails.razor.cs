using Microsoft.AspNetCore.Components;
using ProjectIT.Client.Constants;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Dtos.Users;
using ProjectIT.Shared.Enums;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages.SupervisorDetails;

public partial class SupervisorDetails
{
    [Parameter]
    public int Id { get; set; }

    private SupervisorDetailsDto? supervisor;
    private IEnumerable<ProjectDetailsDto>? supervisorProjects;

    private string panelWidth = "250px";
    private string statusSupervisor = null!;

    protected override async Task OnInitializedAsync()
    {
        supervisor = await anonymousClient.Client.GetFromJsonAsync<SupervisorDetailsDto>($"{ApiEndpoints.Supervisors}/{Id}");
        supervisorProjects = (await anonymousClient.Client.GetFromJsonAsync<IEnumerable<ProjectDetailsDto>>(ApiEndpoints.Projects))!.Where(project => project.Supervisor.Email.Equals(supervisor!.Email, StringComparison.OrdinalIgnoreCase));

        SetSupervisorStatus(supervisor!.Status);
    }

    private void SetSupervisorStatus(SupervisorStatus supervisorStatus)
    {
        switch (supervisorStatus)
        {
            case SupervisorStatus.Available:
                statusSupervisor = "bg-success";
                break;
            case SupervisorStatus.LimitedSupervision:
                statusSupervisor = "bg-warning";
                break;
            case SupervisorStatus.Inactive:
                statusSupervisor = "bg-danger";
                break;
        }
    }

    private void RequestSupervision(NavigationManager navigationManager) => navigationManager.NavigateTo(PageUrls.CreateRequest);
}