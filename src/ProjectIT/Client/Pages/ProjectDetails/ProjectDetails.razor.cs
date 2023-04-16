using Microsoft.AspNetCore.Components;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Enums;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages.ProjectDetails;

public partial class ProjectDetails
{
    [Parameter]
    public int Id { get; set; }

    private ProjectDetailsDto? project;
    
    private string panelWidth = "250px";
    private string statusSupervisor = null!;
    private string statusCoSupervisor = null!;

    private void ApplyProject(NavigationManager navigationManager)
    {
        navigationManager.NavigateTo("/");
    }

    protected override async Task OnInitializedAsync()
    {
        project = await httpClient.GetFromJsonAsync<ProjectDetailsDto>($"{ApiEndpoints.Projects}/{Id}");
        SetSupervisorStatus(project!.Supervisor.Status);
        SetCoSupervisorStatus(project.CoSupervisor?.Status);
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

    private void SetCoSupervisorStatus(SupervisorStatus? coSupervisorStatus)
    {
        switch (coSupervisorStatus)
        {
            case SupervisorStatus.Available:
                statusCoSupervisor = "bg-success";
                break;
            case SupervisorStatus.LimitedSupervision:
                statusCoSupervisor = "bg-warning";
                break;
            case SupervisorStatus.Inactive:
                statusCoSupervisor = "bg-danger";
                break;
        }
    }
}