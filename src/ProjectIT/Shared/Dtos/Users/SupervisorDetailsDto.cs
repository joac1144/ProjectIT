using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Dtos.Users;

/// <summary>
/// Supervisor details DTO for the <see cref="Supervisor" /> class.
/// </summary>
public record SupervisorDetailsDto
{
    public int Id { get; set; }

    [Required]
    public string FullName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public IEnumerable<Topic> Topics { get; set; } = null!;

    [Required]
    public SupervisorProfession Profession { get; set; }

    [Required]
    public SupervisorStatus Status { get; set; }
}