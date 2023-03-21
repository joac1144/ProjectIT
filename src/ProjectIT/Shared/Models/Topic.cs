using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Models;

public class Topic
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string Category { get; set; } = null!;
}