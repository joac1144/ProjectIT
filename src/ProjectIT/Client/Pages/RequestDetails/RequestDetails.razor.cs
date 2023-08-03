using Microsoft.AspNetCore.Components;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Requests;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ProjectIT.Client.Pages.RequestDetails;

public partial class RequestDetails
{
    [Parameter]
    public int Id { get; set; }

    private RequestDetailsDto? request;
    private string panelWidth = "250px";
    private ClaimsPrincipal? authUser;
    private string? userEmail;

    protected override async Task OnInitializedAsync()
    {
        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
        userEmail = authUser?.FindFirst("preferred_username")?.Value!;
        request = await httpClient.GetFromJsonAsync<RequestDetailsDto>($"{ApiEndpoints.Requests}/{Id}");
    }
}