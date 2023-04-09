using System.ComponentModel.DataAnnotations;
using ProjectIT.Shared.Enums;

namespace ProjectIT.Shared.Models;

public class Supervisor : User
{
    [Required]
    public IEnumerable<Topic> Topics { get; set; } = null!;

    [Required]
    public string Profession { get; set; } = null!;

    // For Co-supervisors that might be PhD or Master students.
    public Programme? Programme { get; set; } = null!;
}