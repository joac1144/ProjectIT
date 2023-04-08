using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using ProjectIT.Shared.Dtos.Topics;
using ProjectIT.Shared.Models;

namespace ProjectIT.Client.Components.SupervisorCard;

public partial class SupervisorCard
{
    [Parameter]
    public int Id { get; set; }

    [Parameter]
    public string FullName { get; set; } = string.Empty;

    [Parameter]
    public IEnumerable<Topic> Topics { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        Topics = await httpClient.GetFromJsonAsync<IEnumerable<Topic>>($"supervisors/{Id}/topics");
    }
}