using ProjectIT.Shared.Enums;

namespace ProjectIT.Shared.Models;

public class Student : User
{
    public Education? Education { get; init; } = null!;
}