using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using ProjectIT.Shared.Dtos.Topics;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Client.Components.RequestCard;

public partial class RequestCard
{
    [Parameter]
    public int Id { get; set; }

    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public IEnumerable<Programme>? Programmes { get; set; } = null!;

    [Parameter]
    public Ects? Ects { get; set; }

    [Parameter]
    public Season Season { get; set; }

    [Parameter]
    public int Year { get; set; }

    [Parameter]

    public int GroupMembers { get; set; }

    [Parameter]
    public string Description { get; set; }
}