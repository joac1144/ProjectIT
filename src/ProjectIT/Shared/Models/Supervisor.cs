using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Models;

public class Supervisor : User
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public IEnumerable<Topic> Topics { get; set; } = null!;
}