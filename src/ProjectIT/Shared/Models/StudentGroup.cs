using ProjectIT.Shared.Enums;

namespace ProjectIT.Shared.Models;

public class StudentGroup
{
    public int Id { get; set; }

    public IEnumerable<Student> Students { get; set; } = null!;

    public StudentGroupStatus Status { get; set; }
}