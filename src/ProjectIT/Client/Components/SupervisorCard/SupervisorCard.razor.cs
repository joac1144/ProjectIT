using Microsoft.AspNetCore.Components;
using ProjectIT.Shared.Enums;
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

    [Parameter]
    public SupervisorStatus Status { get; set; }

    private string StatusBackgroundColor
    {
        get
        {
            return Status switch
            {
                SupervisorStatus.Available => "bg-success",
                SupervisorStatus.LimitedSupervision => "bg-warning",
                SupervisorStatus.Inactive => "bg-danger",
                _ => "bg-secondary"
            };
        }
    }
}