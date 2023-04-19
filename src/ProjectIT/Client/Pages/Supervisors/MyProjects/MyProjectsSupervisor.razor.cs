using Microsoft.JSInterop;
using ProjectIT.Client.Components.Modal;
using ProjectIT.Client.Shared.Enums;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Projects;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages.Supervisors.MyProjects;

public partial class MyProjectsSupervisor
{
    private IEnumerable<ProjectDetailsDto>? projects;

    private string? sortValue;
    private readonly IEnumerable<Sort> _sortValues = Enum.GetValues<Sort>();

    private Modal<ProjectDetailsDto>? modal;

    protected override async Task OnInitializedAsync()
    {
        // Fetch supervisor's projects.
        projects = await httpClient.GetFromJsonAsync<IEnumerable<ProjectDetailsDto>>(ApiEndpoints.Projects); /*await httpClient.GetFromJsonAsync<IEnumerable<Supervisor>>("supervisors").Where(...).Select(supervisor => supervisor.Projects);*/
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

    private async void DeleteProject(int id)
    {
        var response = httpClient.DeleteAsync($"{ApiEndpoints.Projects}/{id}");

        if (!response.Result.IsSuccessStatusCode)
        {
            await jsRuntime.InvokeAsync<string>("alert", $"Something went wrong deleting project with id {id}.");
        }
    }
}