using System.ComponentModel.DataAnnotations;
using ProjectIT.Shared.Constants;
using ProjectIT.Shared.Enums;

namespace ProjectIT.Shared.Models;

public class Request
{
    public int Id { get; set; }

    [Required]
    [StringLength(ModelRestrictions.RequestTitleCap)]
    public string Title { get; set; } = null!;

    [Required]
    public string DescriptionHtml { get; set; } = null!;

    public IEnumerable<Topic>? Topics { get; set; }

    [Required]
    public IEnumerable<Language> Languages { get; set; } = null!;

    [Required]
    public IEnumerable<Programme> Programmes { get; set; } = null!;

    [Required]
    public StudentGroup StudentGroup { get; set; } = null!;

    [Required]
    public Ects Ects { get; set; }

    [Required]
    public Semester Semester { get; set; } = null!;
    
    [Required]
    public IEnumerable<Supervisor> Supervisors { get; set; } = null!;

    [Required]
    public RequestStatus? Status { get; set; }
}