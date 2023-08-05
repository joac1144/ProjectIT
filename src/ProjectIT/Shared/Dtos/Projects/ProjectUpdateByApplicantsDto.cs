using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Dtos.Projects;

public record ProjectUpdateByApplicantsDto
{
    public int Id { get; set; }

    [Required]
    public string Email { get; set; } = null!;
}