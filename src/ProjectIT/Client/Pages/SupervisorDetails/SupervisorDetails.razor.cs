using Microsoft.AspNetCore.Components;
using ProjectIT.Shared.Dtos.Users;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages.SupervisorDetails;

public partial class SupervisorDetails
{
    [Parameter]
    public int Id { get; set; }

    private SupervisorDetailsDto? supervisor;

    private string panelWidth = "250px";

    private void RequestSupervision(NavigationManager navigationManager)
    {
        // should go to create request page and pre-select the given supervisor.
        navigationManager.NavigateTo("/");
    }

    protected override async Task OnInitializedAsync()
    {
        supervisor = await httpClient.GetFromJsonAsync<SupervisorDetailsDto>($"supervisors/{Id}");
    }
}