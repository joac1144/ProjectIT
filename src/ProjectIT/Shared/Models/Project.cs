using System.ComponentModel.DataAnnotations;

namespace ProjectIT.Shared.Models;

public class Project
{
    public int Id { get; set; }

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
    public IEnumerable<Education> Educations { get; set; } = null!;

    [Required]
    public Ects? Ects { get; set; }

    [Required]
    public Date? Date { get; set; } = null!;

    [Required]
    public Supervisor Supervisor { get; set; } = null!;

    public Supervisor? CoSupervisor { get; set; }

    [Required]
    public IEnumerable<Student> Students { get; set; } = null!;
}