using Microsoft.JSInterop;
using ProjectIT.Client.Components.Modal;
using ProjectIT.Client.Constants;
using ProjectIT.Client.Shared.Enums;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Requests;
using ProjectIT.Shared.Models;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ProjectIT.Client.Pages.Students.MyRequestsStudent;

public partial class MyRequestsStudent
{
    private IEnumerable<RequestDetailsDto>? requests;

    private string? sortValue;
    private readonly IEnumerable<Sort> _sortValues = Enum.GetValues<Sort>();

    private ClaimsPrincipal? authUser;

    protected override async Task OnInitializedAsync()
    {
        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
        string userEmail = authUser?.FindFirst("preferred_username")?.Value!;

        requests = (await httpClient.GetFromJsonAsync<IEnumerable<RequestDetailsDto>>(ApiEndpoints.Requests))!.Where(request => request.Student.Email == userEmail);
    }

    private void OnSort(object value)
    {
        if (requests != null && value.GetType() == typeof(string))
        {
            sortValue = value.ToString();
            switch (value)
            {
                case nameof(Sort.Status):
                    requests = requests.OrderBy(r => r.Status).ToList();
                    break;
                case nameof(Sort.Semester):
                    requests = requests.OrderBy(r => r.Semester).ToList();
                    break;
                case nameof(Sort.ECTS):
                    requests = requests.OrderBy(r => r.Ects).ToList();
                    break;
            }
        }
    }
}