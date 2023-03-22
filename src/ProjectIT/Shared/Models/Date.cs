using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Models;

public class Date
{
    public int Id { get; set; }

    [Required]
    public Season? Season { get; set; } = null!;

    [Required]
    public int? Year { get; set; } = null!;
}