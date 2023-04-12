using System.ComponentModel.DataAnnotations;
using ProjectIT.Shared.Enums;

namespace ProjectIT.Shared.Models;

public class Supervisor : User
{
    [Required]
    public IEnumerable<Topic> Topics { get; set; } = null!;

    [Required]
    public SupervisorProfession Profession { get; set; }

    [Required]
    public SupervisorStatus Status { get; set; }
}