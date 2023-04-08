using Microsoft.AspNetCore.Components;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;
using System.Text.RegularExpressions;

namespace ProjectIT.Client.Components.ProjectCard;

public partial class ProjectCard
{
    [Parameter]
    public int Id { get; set; }

    [Parameter]
    public string Title { get; set; } = string.Empty;

    [Parameter]
    public string DescriptionHtml { get; set; } = string.Empty;

    [Parameter]
    public Supervisor Supervisor { get; set; } = null!;

    [Parameter]
    public Supervisor? CoSupervisor { get; set; }

    [Parameter]
    public IEnumerable<Programme> Programmes { get; set; } = null!;

    [Parameter]
    public Semester Semester { get; set; } = null!;

    [Parameter]
    public Ects? Ects { get; set; }

    public string Description => Regex.Replace(DescriptionHtml, "<[^>]*>", "");
}