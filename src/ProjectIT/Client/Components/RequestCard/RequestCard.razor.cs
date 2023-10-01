using Microsoft.AspNetCore.Components;
using ProjectIT.Client.Shared.Helpers;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;
using System.Text.RegularExpressions;

namespace ProjectIT.Client.Components.RequestCard;

public partial class RequestCard
{
    [Parameter]
    public int Id { get; set; }

    [Parameter]
    public string Title { get; set; } = string.Empty;

    [Parameter]
    public string DescriptionHtml { get; set; } = string.Empty;

    [Parameter]
    public StudentGroup StudentGroup { get; set; } = null!;

    [Parameter]
    public IEnumerable<Programme> Programmes { get; set; } = null!;

    [Parameter]
    public Semester Semester { get; set; } = null!;

    [Parameter]
    public Ects Ects { get; set; }

    [Parameter]
    public RequestStatus? Status { get; set; }

    [Parameter]
    public string? CssClasses { get; set; }

    private string Description
    {
        get
        {
            var strippedString = HTMLHelper.RemoveFromText(DescriptionHtml);
            return strippedString;
        }
    }

    private string StatusBackgroundColor
    {
        get
        {
            return Status switch
            {
                RequestStatus.Accepted => "bg-success",
                RequestStatus.Pending => "bg-warning",
                RequestStatus.Declined => "bg-danger",
                _ => "bg-secondary"
            };
        }
    }
    
    private readonly Dictionary<string, string> _htmlEntitiesTable = new()
    {
        { "&nbsp;", " " },
        { "&amp;", "&" },
        { "&quot;", "\"" },
        { "&apos;", "'" },
        { "&lt;", "<" },
        { "&gt;", ">" },
        { "&cent;", "¢" },
        { "&pound;", "£" },
        { "&yen;", "¥" },
        { "&euro;", "€" },
        { "&copy;", "©" },
        { "&reg;", "®" },
        { "&trade;", "™" },
        { "&times;", "×" },
        { "&divide;", "÷" }
    };
}