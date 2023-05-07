using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Dtos.Projects;

/// <summary>
/// Project details DTO for the <see cref="project" /> class.
/// </summary>
public record ProjectDetailsDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(4400)]
    public string DescriptionHtml { get; set; } = null!;

    [Required]
    public IEnumerable<Topic> Topics { get; set; } = null!;

    [Required]
    public IEnumerable<Language> Languages { get; set; } = null!;

    [Required]
    public IEnumerable<Programme> Programmes { get; set; } = null!;

    [Required]
    public Ects Ects { get; set; }

    [Required]
    public Semester Semester { get; set; } = null!;

    [Required]
    public Supervisor Supervisor { get; set; } = null!;

    public Supervisor? CoSupervisor { get; set; }

    public IEnumerable<Student>? Students { get; set; }
}