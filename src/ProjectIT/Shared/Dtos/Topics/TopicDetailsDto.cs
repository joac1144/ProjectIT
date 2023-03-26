using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Dtos.Topics;

/// <summary>
/// Topic details DTO for the <see cref="Topic" /> class.
/// </summary>
public record TopicDetailsDto
{
    [Required]
    public string Name { get; set; } = null!;

    public TopicCategory Category { get; set; }    
}