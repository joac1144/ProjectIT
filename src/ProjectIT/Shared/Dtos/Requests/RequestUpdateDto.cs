using System.ComponentModel.DataAnnotations;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Shared.Dtos.Requests;

public record RequestUpdateDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(4400)]
    public string DescriptionHtml { get; set; } = null!;

    public IEnumerable<Topic>? Topics { get; set; }

    [Required]
    public IEnumerable<Language> Languages { get; set; } = null!;

    [Required]
    public IEnumerable<Programme> Programmes { get; set; } = null!;

    [Required]
    public Student Student { get; set;} = null!;

    public IEnumerable<Student>? ExtraMembers { get; set; }

    [Required]
    public IEnumerable<Supervisor> Supervisors { get; set; } = null!;

    [Required]
    public Ects Ects { get; set; }

    [Required]
    public Semester? Semester { get; set; }

    [Required]
    public RequestStatus? Status { get; set; }
}