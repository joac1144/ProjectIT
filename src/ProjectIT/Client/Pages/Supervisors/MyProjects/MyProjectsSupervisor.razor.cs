using Microsoft.JSInterop;
using ProjectIT.Client.Components.Modal;
using ProjectIT.Client.Constants;
using ProjectIT.Client.Shared.Enums;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Projects;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ProjectIT.Client.Pages.Supervisors.MyProjects;

public partial class MyProjectsSupervisor
{
    private IEnumerable<ProjectDetailsDto>? projects;

    private string? sortValue;
    private readonly IEnumerable<RegularSort> _sortValues = Enum.GetValues<RegularSort>();

    private Modal<ProjectDetailsDto>? modal;
    private ProjectDetailsDto modalData = new();

    private ClaimsPrincipal? authUser;

    protected override async Task OnInitializedAsync()
    {
        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
        string userEmail = authUser?.FindFirst("preferred_username")?.Value!;

        // Fetch supervisor's projects.
        // This might be a security flaw ???
        projects = (await httpClient.GetFromJsonAsync<IEnumerable<ProjectDetailsDto>>(ApiEndpoints.Projects))!.Where(project => project.Supervisor.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase));
    }

    private void OnSort(object value)
    {
        if (projects != null && value.GetType() == typeof(string))
        {
            sortValue = value.ToString();
            switch (value)
            {
                case nameof(RegularSort.Semester):
                    projects = projects.OrderBy(p => p.Semester).ToList();
                    break;
                case nameof(RegularSort.ECTS):
                    projects = projects.OrderBy(p => p.Ects).ToList();
                    break;
            }
        }
    }

    private void OpenDeleteConfirmationModal(ProjectDetailsDto project)
    {
        modalData = project;
        modal?.OpenModal(modalData);
    }

    private void EditProject(int projectId)
    {
        navigationManager.NavigateTo($"/projects/{projectId}/edit");
    }

    private async void DeleteProject(int id)
    {
        var response = await httpClient.DeleteAsync($"{ApiEndpoints.Projects}/{id}");

        if (!response.IsSuccessStatusCode)
        {
            await jsRuntime.InvokeAsync<string>("alert", $"Something went wrong deleting project with id {id}.");
        }

        navigationManager.NavigateTo(PageUrls.MyProjects, true);
    }
}