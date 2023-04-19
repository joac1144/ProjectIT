using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; } = null!;
    
    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}