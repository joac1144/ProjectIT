using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Models;

public class Tag
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;
}