using ProjectIT.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Models;

public class Topic
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public TopicCategory? Category { get; set; }
}