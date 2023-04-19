using Microsoft.AspNetCore.Components;
using ProjectIT.Shared.Models;

namespace ProjectIT.Client.Components.SupervisorCard;

public partial class SupervisorCard
{
    [Parameter]
    public int Id { get; set; }

    [Parameter]
    public string Name { get; set; } = string.Empty;

    [Parameter]
    public IEnumerable<Topic> Topics { get; set; } = null!;
}