using System.ComponentModel.DataAnnotations;
using ProjectIT.Shared.Enums;

namespace ProjectIT.Shared.Models;

public class Semester
{
    public int Id { get; set; }

    [Required]
    public Season Season { get; set; }

    [Required]
    public int Year { get; set; }

    public override string ToString()
    {
        return $"{Season} {Year}";
    }
}