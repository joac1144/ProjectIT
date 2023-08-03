using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Dtos.Projects;

/// <summary>
/// Add project arguments.
/// </summary>
public record ProjectCreateDto
{
    [Required]
    [StringLength(50)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(4400)]
    public string DescriptionHtml { get; set; } = null!;

    public IEnumerable<Topic>? Topics { get; set; }

    [Required]
    public IEnumerable<Language> Languages { get; set; } = null!;

    [Required]
    public IEnumerable<Programme> Programmes { get; set; } = null!;

    [Required]
    public Ects Ects { get; set; }

    [Required]
    public Semester Semester { get; set; } = null!;

    [Required]
    public string SupervisorEmail { get; set; } = null!;
    
    public string? CoSupervisorEmail { get; set; }
}