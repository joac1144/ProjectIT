using System.ComponentModel.DataAnnotations;
using ProjectIT.Shared.Constants;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Shared.Dtos.Requests;

public record RequestCreateDto
{
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
    public IEnumerable<string> StudentEmails { get; set; } = null!;

    [Required]
    public IEnumerable<string> SupervisorEmails { get; set; } = null!;

    [Required]
    public Ects Ects { get; set; }

    [Required]
    public Semester Semester { get; set; } = null!;

    [Required]
    public RequestStatus? Status { get; set; }
}