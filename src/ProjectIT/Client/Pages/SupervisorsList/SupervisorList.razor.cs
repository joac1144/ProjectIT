using Microsoft.AspNetCore.Components;
using ProjectIT.Shared.Dtos.Topics;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages.Supervisors_list;

public partial class SupervisorList
{
    [Parameter]
    public int Id { get; set; }
}