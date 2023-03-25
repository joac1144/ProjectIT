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

    public Education? Education { get; init; } = null!;   
}