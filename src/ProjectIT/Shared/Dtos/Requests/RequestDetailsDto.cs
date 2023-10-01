using System.ComponentModel.DataAnnotations;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Shared.Dtos.Requests;

/// <summary>
/// Request details DTO for the <see cref="request" /> class.
/// </summary>
public record RequestDetailsDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(4800)]
    public string DescriptionHtml { get; set; } = null!;

    public IEnumerable<Topic>? Topics { get; set; }

    [Required]
    public IEnumerable<Language> Languages { get; set; } = null!;

    [Required]
    public IEnumerable<Programme> Programmes { get; set; } = null!;

    [Required]
    public StudentGroup StudentGroup { get; set; } = null!;

    [Required]
    public IEnumerable<Supervisor> Supervisors { get; set; } = null!;

    [Required]
    public Ects Ects { get; set; }

    [Required]
    public Semester? Semester { get; set; }

    [Required]
    public RequestStatus? Status { get; set; }
}