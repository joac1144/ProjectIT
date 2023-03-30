using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Dtos.Topics;

/// <summary>
/// Student details DTO for the <see cref="Student" /> class.
/// </summary>
public record StudentDetailsDto
{
    [Required]
    public string FullName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public Programme? Programme { get; init; } = null!;   
}