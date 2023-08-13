using Microsoft.JSInterop;
using ProjectIT.Client.Components.Modal;
using ProjectIT.Client.Constants;
using ProjectIT.Client.Shared.Enums;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Requests;
using ProjectIT.Shared.Models;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ProjectIT.Client.Pages.MyRequests;

public partial class MyRequests
{
    private IEnumerable<RequestDetailsDto>? studentRequests;
    private IEnumerable<RequestDetailsDto>? supervisorRequests;

    private bool isLoading = false;
    private string? sortValue;
    private readonly IEnumerable<SortForStudents> _sortValuesForStudents = Enum.GetValues<SortForStudents>();
    private readonly IEnumerable<RegularSort> _sortValues = Enum.GetValues<RegularSort>();

    private ClaimsPrincipal? authUser;

    protected override async Task OnInitializedAsync()
    {
        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
        string userEmail = authUser?.FindFirst("preferred_username")?.Value!;

        isLoading = true;
        if (authUser!.IsInRole(AppRoles.Student))
        {
            studentRequests = (await httpClient.GetFromJsonAsync<IEnumerable<RequestDetailsDto>>(ApiEndpoints.Requests))!
                .Where(request => request.Student.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase));
        }
        else if (authUser!.IsInRole(AppRoles.Supervisor))
        {
            supervisorRequests = (await httpClient.GetFromJsonAsync<IEnumerable<RequestDetailsDto>>(ApiEndpoints.Requests))!
                .Where(request => request.Supervisors.Any(supervisor => supervisor.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase)));
        }
        
        isLoading = false;
    }

    private void OnSortForStudents(object value)
    {
        if (studentRequests != null && value.GetType() == typeof(string))
        {
            sortValue = value.ToString();
            switch (value)
            {
                case nameof(SortForStudents.Status):
                    studentRequests = studentRequests.OrderBy(r => r.Status).ToList();
                    break;
                case nameof(SortForStudents.Semester):
                    studentRequests = studentRequests.OrderBy(r => r.Semester).ToList();
                    break;
                case nameof(SortForStudents.ECTS):
                    studentRequests = studentRequests.OrderBy(r => r.Ects).ToList();
                    break;
            }
        }
    }

    private void OnSortForSupervisors(object value)
    {
        if (supervisorRequests != null && value.GetType() == typeof(string))
        {
            sortValue = value.ToString();
            switch (value)
            {
                case nameof(RegularSort.Semester):
                    supervisorRequests = supervisorRequests.OrderBy(r => r.Semester).ToList();
                    break;
                case nameof(RegularSort.ECTS):
                    supervisorRequests = supervisorRequests.OrderBy(r => r.Ects).ToList();
                    break;
            }
        }
    }
}