using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using ProjectIT.Client.Http;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Users;
using ProjectIT.Client.Constants;

namespace ProjectIT.Client.Components.Login;

public partial class LoginDisplay
{

    private ClaimsPrincipal? authUser;
    private string? userEmail;

    private SupervisorDetailsDto? supervisor = new();

    private async void DisplaySupervisorProfile()
    {
        authUser = (await authenticationStateProvider.GetAuthenticationStateAsync()).User;

        userEmail = authUser?.FindFirst("preferred_username")?.Value!;

        supervisor = await anonymousClient.Client.GetFromJsonAsync<SupervisorDetailsDto>($"{ApiEndpoints.Supervisors}/{userEmail}");

        Console.WriteLine($"User Id: {supervisor!.Id}");

        navManager.NavigateTo($"{PageUrls.MyProfile}/{supervisor!.Id}");

    }

    public void BeginLogOut()
    {
        Navigation.NavigateToLogout("authentication/logout");
    }

    public void BeginLogIn()
    {
        Navigation.NavigateToLogin("authentication/login");

    }


}
