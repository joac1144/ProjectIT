using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using ProjectIT.Client.Constants;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Dtos.Users;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ProjectIT.Client.Pages.ProjectDetails;

public partial class ProjectDetails
{
    [Parameter]
    public int Id { get; set; }

    private ProjectDetailsDto? project;
    private StudentDetailsDto? student;

    private string panelWidth = "250px";
    private string statusSupervisor = null!;
    private string statusCoSupervisor = null!;

    private ClaimsPrincipal? authUser;


    private async Task ApplyProject(NavigationManager navigationManager)
    {
        string userEmail = authUser?.FindFirst("preferred_username")?.Value!;

        var projectUpdateByApplicantDto = new ProjectUpdateByApplicantsDto
        {
            Id = project.Id,
            Email = userEmail
        };
        

        var response = await anonymousClient.Client.PutAsJsonAsync(ApiEndpoints.Projects, projectUpdateByApplicantDto);

        if (response.IsSuccessStatusCode) 
        {
            await JSRuntime.InvokeAsync<string>("alert", "Project applied successfully!");
            navigationManager.NavigateTo(PageUrls.Projects);
        }

        else 
        {
            await JSRuntime.InvokeAsync<string>("alert", "Something went wrong, check your input and try again!");

        }
    }

    protected override async Task OnInitializedAsync()
    {
        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
        project = await httpClient.GetFromJsonAsync<ProjectDetailsDto>($"{ApiEndpoints.Projects}/{Id}");
        SetSupervisorStatus(project!.Supervisor.Status);
        SetCoSupervisorStatus(project.CoSupervisor?.Status);
    }

    private void EditProject(int projectId)
    {
        navigationManager.NavigateTo($"/projects/{projectId}/edit");
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