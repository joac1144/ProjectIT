using ProjectIT.Shared.Enums;

namespace ProjectIT.Client.Components.Filter;

public class FilterTag
{
    public string Tag { get; set; } = null!;

    public bool Selected { get; set; }

    public TopicCategory? Category { get; set; }
}