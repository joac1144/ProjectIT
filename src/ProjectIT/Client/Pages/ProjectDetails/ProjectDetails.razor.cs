using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using ProjectIT.Client.Components.Modal;
using ProjectIT.Client.Constants;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Dtos.Users;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace ProjectIT.Client.Pages.ProjectDetails;

public partial class ProjectDetails
{
    [Parameter]
    public int Id { get; set; }

    private ProjectDetailsDto? project;
    private IEnumerable<StudentDetailsDto> students = null!;
    
    private string panelWidth = "250px";
    private string statusSupervisor = null!;
    private string statusCoSupervisor = null!;

    private Modal<StudentGroup>? modal;
    private int groupMembers = 1;
    private string? memberMail;
    private readonly List<StudentDetailsDto> extraMembers = new();

    private ClaimsPrincipal? authUser;
    private string? userEmail;
    private StudentDetailsDto? studentDetails;

    private bool? hasApplied;
    private StudentGroup? appliedStudentGroup;

    protected override async Task OnInitializedAsync()
    {
        project = await anonymousClient.Client.GetFromJsonAsync<ProjectDetailsDto>($"{ApiEndpoints.Projects}/{Id}");
        SetSupervisorStatus(project!.Supervisor.Status);
        SetCoSupervisorStatus(project.CoSupervisor?.Status);

        students = (await anonymousClient.Client.GetFromJsonAsync<IEnumerable<StudentDetailsDto>>(ApiEndpoints.Students))!;
        if (students == null)
            throw new Exception("Could not load students");

        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
        userEmail = authUser?.FindFirst("preferred_username")?.Value!;

        if (userEmail is not null && students.Any(student => student.Email == userEmail))
            studentDetails = students.Where(student => student.Email == userEmail).Single();

        hasApplied = project.AppliedStudentGroups?.Any(sg => sg.Students.Select(student => student.Email).Contains(userEmail));
        if (hasApplied.GetValueOrDefault())
        {
            appliedStudentGroup = project.AppliedStudentGroups!.First(sg => sg.Students.Select(student => student.Email).Contains(userEmail));
        }
    }

    private async Task OnAddNewMemberFromSearchClicked()
    {
        if (!string.IsNullOrWhiteSpace(memberMail) && Regex.IsMatch(memberMail, @"^([\w.\-]+)@([\w\-]+)((\.(\w{2,}))+)$"))
        {
            if (!extraMembers!.Select(member => member.Email).Contains(memberMail, StringComparer.OrdinalIgnoreCase) && students.Select(student => student.Email).Contains(memberMail, StringComparer.OrdinalIgnoreCase))
            {
                var newMember = students.Single(student => student.Email.Equals(memberMail, StringComparison.OrdinalIgnoreCase));
                extraMembers.Add(newMember);
                students = students.Where(student => student.Email != newMember.Email);
            }
            else
            {
                await JSRuntime.InvokeAsync<string>("alert", "The student is already added or does not exist.");
            }
        }
        else
        {
            await JSRuntime.InvokeAsync<string>("alert", "Cannot find a student with this email, please try again.");
        }
        memberMail = string.Empty;
    }

    private void OnSelectedMemberClicked(StudentDetailsDto student)
    {
        extraMembers.Remove(student);
        students = students.Append(student);
    }

    private async Task ApplyProject()
    {
        var studentsToGroup = extraMembers.Prepend(studentDetails);

        var response = await anonymousClient.Client.PutAsJsonAsync($"{ApiEndpoints.Projects}/{Id}/apply", studentsToGroup);

        if (response.IsSuccessStatusCode)
        {
            await JSRuntime.InvokeAsync<string>("alert", "Project applied successfully!");
            navigationManager.NavigateTo(PageUrls.Projects);
        }
        else
        {
            await JSRuntime.InvokeAsync<string>("alert", "Something went wrong!");
        }
    }

    private void EditProject(int projectId)
    {
        navigationManager.NavigateTo($"/projects/{projectId}/edit");
    }

    private void SetSupervisorStatus(SupervisorStatus supervisorStatus)
    {
        switch (supervisorStatus)
        {
            case SupervisorStatus.Available:
                statusSupervisor = "bg-success";
                break;
            case SupervisorStatus.LimitedSupervision:
                statusSupervisor = "bg-warning";
                break;
            case SupervisorStatus.Inactive:
                statusSupervisor = "bg-danger";
                break;
        }
    }

    private void SetCoSupervisorStatus(SupervisorStatus? coSupervisorStatus)
    {
        switch (coSupervisorStatus)
        {
            case SupervisorStatus.Available:
                statusCoSupervisor = "bg-success";
                break;
            case SupervisorStatus.LimitedSupervision:
                statusCoSupervisor = "bg-warning";
                break;
            case SupervisorStatus.Inactive:
                statusCoSupervisor = "bg-danger";
                break;
        }
    }
}