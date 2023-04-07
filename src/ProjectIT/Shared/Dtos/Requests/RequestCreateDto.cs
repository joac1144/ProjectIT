using System.ComponentModel.DataAnnotations;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Shared.Dtos.Requests;

public record RequestCreateDto
{
    [Required]
    [StringLength(50)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(4400)]
    public string Description { get; set; } = null!;

    [Required]
    public IEnumerable<Topic> Topics { get; set; } = null!;

    [Required]
    public IEnumerable<Language> Languages { get; set; } = null!;

    [Required]
    public IEnumerable<Programme> Programmes { get; set; } = null!;

    public IEnumerable<Student>? Members { get; set; }

    [Required]
    public IEnumerable<Supervisor> Supervisors { get; set; } = null!;

    [Required]
    public Ects? Ects { get; set; }

    [Required]
    public Semester? Semester { get; set; }
}