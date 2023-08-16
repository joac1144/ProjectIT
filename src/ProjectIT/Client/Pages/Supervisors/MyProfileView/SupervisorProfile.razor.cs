using ProjectIT.Shared.Enums;
using Radzen.Blazor;
using ProjectIT.Shared.Extensions;
using ProjectIT.Shared.Dtos.Topics;
using System.Net.Http.Json;
using ProjectIT.Shared;
using ProjectIT.Shared.Models;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Security.Claims;
using ProjectIT.Shared.Dtos.Users;

namespace ProjectIT.Client.Pages.Supervisors.MyProfileView;

public partial class SupervisorProfile
{

    [Parameter]
    public int Id { get; set; }


    private SupervisorProfession profession = new();

    private IEnumerable<string> professions = new List<string>();

    private SupervisorProfession sp = new();


    private SupervisorStatus status = new();

    private IEnumerable<string> statuses = new List<string>();

    private SupervisorStatus ss = new();


    private readonly List<Topic> supervisorTopics = new();

    private RadzenDropDown<Topic>? topicSelector;

    private IEnumerable<Topic> topics = null!;

    private IEnumerable<Topic> topicsInDropdownList = null!;

    private string topicName = string.Empty;

    private SupervisorDetailsDto? supervisor = new();


    protected override async Task OnInitializedAsync()
    {
        

        // userEmail = "testsupervisor @projectititu.onmicrosoft.com";

        supervisor = await anonymousClient.Client.GetFromJsonAsync<SupervisorDetailsDto>($"{ApiEndpoints.Supervisors}/{Id}");

        professions = Enum.GetValues<SupervisorProfession>().ToList().Select(sp => sp.GetTranslatedString(EnumsLocalizer)).ToList();

        statuses = Enum.GetValues<SupervisorStatus>().ToList().Select(ss => ss.GetTranslatedString(EnumsLocalizer)).ToList();

        topics = (await anonymousClient.Client.GetFromJsonAsync<IEnumerable<Topic>>(ApiEndpoints.Topics))!;

        topicsInDropdownList = topics;

    }

    private void OnProfessionsSelectedInList(object args)
    {
        string professionAsString = args.ToString()!;

        Dictionary<string, SupervisorProfession> mappings = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Assistant professor", SupervisorProfession.AssistantProfessor },
            { "Associate professor", SupervisorProfession.AssociateProfessor },
            { "Full professor", SupervisorProfession.FullProfessor },
            { "Adjunct professor", SupervisorProfession.AdjunctProfessor },
            { "External professor", SupervisorProfession.ExternalProfessor },
            { "Research professor", SupervisorProfession.ResearchProfessor },
            { "Phd student", SupervisorProfession.PhdStudent },
            { "Lecturer", SupervisorProfession.Lecturer },
        };

        if (mappings.TryGetValue(professionAsString, out SupervisorProfession supervisorProfession))
        {
            Console.WriteLine("Done!");
            profession = supervisorProfession;
        }
        else
        {
            Console.WriteLine("Invalid input string.");
        }
    }

    private void OnStatusesSelectedInList(object args)
    {
        string statusAsString = args.ToString()!;

        Dictionary<string, SupervisorStatus> mappings = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Limited supervision", SupervisorStatus.LimitedSupervision},
            { "Available", SupervisorStatus.Available},
            {"Inactive",SupervisorStatus.Inactive}
        };

        if (mappings.TryGetValue(statusAsString, out SupervisorStatus supervisorStatus))
        {
            Console.WriteLine("Done!");
            status = supervisorStatus;
        }
        else
        {
            Console.WriteLine("Invalid input string.");
        }
    }

    private void OnTopicSelectedInList(object value)
    {
        if (value is not null)
        {
            string val = (string)value;
            supervisorTopics.Add(topicsInDropdownList.Single(t => t.Name == val));
            topicsInDropdownList = topicsInDropdownList.Where(t => t.Name != val);
            topicSelector?.Reset();
        }
    }

    private void AddTopic(string topicName)
    {
        supervisorTopics.Add(new Topic { Name = topicName });
    }

    private void OnSelectedTopicClicked(Topic topic)
    {
        supervisorTopics.Remove(topic);
        topicsInDropdownList = topicsInDropdownList.Append(topic);
        SortTopics();
    }

    private void SortTopics() => topicsInDropdownList = topicsInDropdownList.OrderBy(t => t.Category.ToString()).ThenBy(t => t.Name);

    private async Task SubmitAsync()
    {
        try
        {
            
            var supervisorDetailsDto = new SupervisorDetailsDto()
            {
                Id = Id,
                FirstName = supervisor.FirstName,
                LastName = supervisor.LastName,
                Email = supervisor.Email,
                Status = status,
                Profession = profession,
                Topics = supervisorTopics
            };

            IEnumerable<Topic> newTopics = supervisorTopics.Where(topic => topic.Category is null);

            if (newTopics.Any())
            {
                // A new topic was added, open dialog to confirm and to add category.
                var result = await SelectTopicCategoryDialog(newTopics);

                // Check if the dialog was confirmed (Save button clicked)
                // and update the project's topics with the modified newProject topics.
                if (result == true)
                {
                    Console.WriteLine("Categories added to topic(s)");
                }
                if (result == null)
                {
                    return;
                }

                else
                {
                    // If the dialog was canceled (Cancel button clicked), do not post the project.
                    return;
                }
            }

            var response = await anonymousClient.Client.PutAsJsonAsync($"{ApiEndpoints.Supervisors}", supervisorDetailsDto);

            if (response.IsSuccessStatusCode)
            {
                await JSRuntime.InvokeAsync<string>("alert", "Supervisor profile updated successfully!");
                navigationManager.NavigateTo("/");
            }
            else
            {
                await JSRuntime.InvokeAsync<string>("alert", "Something went wrong! Please make sure to fill out all required fields and try again.");
            }
        }
        catch
        {
            await JSRuntime.InvokeAsync<string>("alert", "Something went wrong! Please make sure to fill out all required fields and try again.");
        }
    }

    private void DiscardChanges()
    {
        navigationManager.NavigateTo("/");
    }

}