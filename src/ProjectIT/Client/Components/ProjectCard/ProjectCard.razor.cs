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
    public Supervisor Supervisor { get; set; } = null!;

    [Parameter]
    public Supervisor? CoSupervisor { get; set; }

    [Parameter]
    public IEnumerable<Programme> Educations { get; set; } = null!;

    [Parameter]
    public Semester Date { get; set; } = null!;

    [Parameter]
    public Ects? Ects { get; set; }

    private void OnProjectCardClicked()
    {
        // Logic to remove project from current search.
    }
}