using ProjectIT.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectIT.Shared.Models;

public class Student : User
{
    public Programme? Programme { get; set; }

    public IEnumerable<Project>? AppliedProjects { get; set; }

    [NotMapped]
    public IEnumerable<Request>? Requests { get; set; }
}