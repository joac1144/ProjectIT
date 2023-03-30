using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Models;

public class Supervisor : User
{
    [Required]
    public IEnumerable<Topic> Topics { get; set; } = null!;

    [Required]
    public string Profession { get; set; } = null!;
}