using ProjectIT.Shared.Constants;
using ProjectIT.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Models;

public class Topic : IEquatable<Topic>
{
    public int Id { get; set; }

    [Required]
    [StringLength(ModelRestrictions.TopicNameCap)]
    public string Name { get; set; } = null!;

    [Required]
    public TopicCategory? Category { get; set; }

    public bool Equals(Topic? other)
    {
        if (this is null || other is null) return false;

        return Name == other.Name;
    }

    public override bool Equals(object? obj) => Equals(obj as Topic);

    public override int GetHashCode() => (Name, Category).GetHashCode();
}