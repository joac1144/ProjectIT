using Microsoft.AspNetCore.Components;
using ProjectIT.Client.Components.Filter;
using ProjectIT.Client.Components.Search;
using ProjectIT.Client.Pages.Main;
using ProjectIT.Shared.Dtos.Requests;
using ProjectIT.Shared.Dtos.Topics;
using ProjectIT.Shared.Dtos.Users;
using ProjectIT.Shared.Models;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages.ViewStudentRequests;

public partial class ViewRequestsPage
{
    private List<RequestDetailsDto> requests = new();

    private List<RequestDetailsDto> filteredRequests = new();
    private List<RequestDetailsDto> shownRequests = new();

    private List<Sort> sortValues = Enum.GetValues<Sort>().ToList();

    private string? sortValue;

    private int pageSize = 10;
    private int totalPages;
    private int currentPage;

    protected async override Task OnInitializedAsync()
    {
        requests = (await anonymousClient.Client.GetFromJsonAsync<IEnumerable<RequestDetailsDto>>("https://localhost:7094/requests"))?.ToList()!;
        filteredRequests = requests;
        UpdateRequests(0);
    }

    private void UpdateRequests(int pageNumber)
    {
        shownRequests = filteredRequests.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        totalPages = (int)Math.Ceiling(filteredRequests.Count() / (decimal)pageSize);
        currentPage = pageNumber;
    }

    private void NewPage(string buttonType)
    {
        if (buttonType == "next" && currentPage != totalPages - 1) currentPage++;

        if (buttonType == "prev" && currentPage != 0) currentPage--;

        UpdateRequests(currentPage);
    }


    private void NavigateToCreateRequest()
    {
        navManager.NavigateTo("create-request");
    }
}