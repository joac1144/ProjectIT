using Microsoft.AspNetCore.Components;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Client.Components.ProjectCard;

public partial class ProjectCard
{
    [Parameter]
    public string Title { get; set; } = string.Empty;

    [Parameter]
    public string Description { get; set; } = string.Empty;

    [Parameter]
    public IEnumerable<Supervisor> Supervisors { get; set; } = null!;

    [Parameter]
    public IEnumerable<Education> Educations { get; set; } = null!;

    [Parameter]
    public Date Date { get; set; } = null!;

    [Parameter]
    public Ects? Ects { get; set; } = null!;

    private void OnProjectCardClicked()
    {
        // Logic to remove project from current search.
    }
}