using System.ComponentModel.DataAnnotations;
using ProjectIT.Shared.Enums;

namespace ProjectIT.Shared.Models;

public class Request
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
    public IEnumerable<Programme> Programmes { get; set; } = null!;

    [Required]
    public IEnumerable<Student> Members { get; set; } = null!;

    [Required]
    public Ects? Ects { get; set; }

    [Required]
    public Semester Semester { get; set; } = null!;
    
    [Required]
    public IEnumerable<Supervisor> Supervisors { get; set; } = null!;
}