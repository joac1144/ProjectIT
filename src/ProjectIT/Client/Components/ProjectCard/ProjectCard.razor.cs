using Microsoft.AspNetCore.Components;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;
using System.Text.RegularExpressions;

namespace ProjectIT.Client.Components.ProjectCard;

public partial class ProjectCard
{
    [Parameter]
    public int Id { get; set; }

    [Parameter]
    public string Title { get; set; } = string.Empty;

    [Parameter]
    public string DescriptionHtml { get; set; } = string.Empty;

    [Parameter]
    public Supervisor Supervisor { get; set; } = null!;

    [Parameter]
    public Supervisor? CoSupervisor { get; set; }

    [Parameter]
    public IEnumerable<Programme> Programmes { get; set; } = null!;

    [Parameter]
    public Semester Semester { get; set; } = null!;

    [Parameter]
    public Ects Ects { get; set; }

    [Parameter]
    public string? CssClasses { get; set; }

    private string Description
    {
        get
        {
            var strippedString = Regex.Replace(DescriptionHtml, "<[^>]*>", " ");
            foreach (var (key, val) in _htmlEntitiesTable)
            {
                strippedString = strippedString.Replace(key, val);
            }
            return strippedString;
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