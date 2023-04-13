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
        /// Should also pre-select the given supervisor in the create request page.
        // navigationManager.NavigateTo("create-request");
    }

    protected override async Task OnInitializedAsync()
    {
        supervisor = await httpClient.GetFromJsonAsync<SupervisorDetailsDto>($"supervisors/{Id}");
    }
}