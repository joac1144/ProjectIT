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
    private readonly IEnumerable<Sort> _sortValues = Enum.GetValues<Sort>();

    private Modal<ProjectDetailsDto>? modal;

    private ClaimsPrincipal? authUser;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        modal!.Data = new ProjectDetailsDto();
    }

    protected override async Task OnInitializedAsync()
    {
        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
        string userEmail = authUser?.FindFirst("preferred_username")?.Value!;

        // Fetch supervisor's projects.
        // This might be a security flaw ???
        projects = (await httpClient.GetFromJsonAsync<IEnumerable<ProjectDetailsDto>>(ApiEndpoints.Projects))!.Where(project => project.Supervisor.Email == userEmail);
    }

    private void OnSort(object value)
    {
        if (projects != null && value.GetType() == typeof(string))
        {
            sortValue = value.ToString();
            switch (value)
            {
                case nameof(Sort.Semester):
                    projects = projects.OrderBy(p => p.Semester).ToList();
                    break;
                case nameof(Sort.ECTS):
                    projects = projects.OrderBy(p => p.Ects).ToList();
                    break;
            }
        }
    }

    private void EditProject(int projectId)
    {
        navigationManager.NavigateTo($"/my-projects/{projectId}/edit");
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