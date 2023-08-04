using ProjectIT.Shared.Enums;

namespace ProjectIT.Shared.Models;

public class Student : User
{
    public Programme? Programme { get; init; } = null!;

    public IEnumerable<Project>? AppliedProjects { get; init; }

    public IEnumerable<Request>? Requests { get; init; }
}