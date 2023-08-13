using ProjectIT.Shared.Enums;

namespace ProjectIT.Shared.Models;

public class Student : User
{
    public Programme? Programme { get; set; }

    public IEnumerable<Project>? AppliedProjects { get; set; }

    public IEnumerable<Request>? Requests { get; set; }
}