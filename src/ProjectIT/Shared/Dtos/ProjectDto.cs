using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Dtos.ProjectDto;

public record ProjectCreateDto
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

    [Required]
    public Ects? Ects { get; set; } = null!;

    [Required]
    public Semester? Semester { get; set; } = null!;

    [Required]
    public Supervisor Supervisor { get; set; } = null!;

    [Required]
    public IEnumerable<Student> Students { get; set; } = null!;
}

public record ProjectUpdateDto : ProjectCreateDto
{
    public int Id { get; set; }
}