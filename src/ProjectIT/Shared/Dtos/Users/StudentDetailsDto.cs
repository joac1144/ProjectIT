using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Dtos.Users;

/// <summary>
/// Student details DTO for the <see cref="Student" /> class.
/// </summary>
public record StudentDetailsDto
{
    public int Id { get; set; }

   [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public Programme? Programme { get; set; } = null!;

    public IEnumerable<Project>? AppliedProjects { get; set; }
}