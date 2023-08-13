using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ProjectIT.Client.Pages.Students.MyProjects;

public partial class MyProjectsStudent
{
    private ICollection<ProjectDetailsDto>? appliedProjects;

    private ClaimsPrincipal? authUser;

    protected override async Task OnInitializedAsync()
    {
        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
        string userEmail = authUser?.FindFirst("preferred_username")?.Value!;

        // Fetch student's applied projects.
        appliedProjects = (await httpClient.GetFromJsonAsync<IEnumerable<ProjectDetailsDto>>(ApiEndpoints.Projects))!
            .Where(
                project => project.AppliedStudentGroups is not null && project.AppliedStudentGroups.Any(
                    group => group.Students is not null && group.Students.Select(student => student.Email)
                    .Contains(userEmail)))
            .ToList();

        /* TODO: Do this instead when it works at some point...
        var student = (await httpClient.GetFromJsonAsync<IEnumerable<StudentDetailsDto>>(ApiEndpoints.Students))!.Where(student => student.Email == userEmail).Single();

        var appPro = student.AppliedProjects;
        */
    }
}