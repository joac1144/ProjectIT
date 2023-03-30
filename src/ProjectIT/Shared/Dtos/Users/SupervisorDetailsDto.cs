using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Dtos.Topics;

/// <summary>
/// Supervisor details DTO for the <see cref="Supervisor" /> class.
/// </summary>
public record SupervisorDetailsDto
{
    [Required]
    public string FullName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public IEnumerable<Topic> Topics { get; set; } = null!;

    [Required]
    public string Profession { get; set; } = null!;

    // For Co-supervisors that might be PhD or Master students.
    public Programme? Programme { get; init; } = null!; 
}