using Microsoft.JSInterop;
using ProjectIT.Shared.Dtos.Requests;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages.CreateRequest;

public partial class CreateRequestPage
{
    private class EctsWrapper
    {
        public Ects Ects { get; set; }
        public string StringValue => Ects.ToString();
    }
    private class ProgrammeWrapper
    {
        public Programme Programme { get; set; }

        public string StringValue => Programme.ToString();
    }

    private class LanguageWrapper
    {
        public Language Language { get; set; }
        public string StringValue => Language.ToString();
    }

    private readonly IEnumerable<EctsWrapper> ectsWrappers = Enum.GetValues<Ects>().Select(ects => new EctsWrapper { Ects = ects });

    private readonly IEnumerable<ProgrammeWrapper> programmeWrappers = Enum.GetValues<Programme>().Select(prog => new ProgrammeWrapper { Programme = prog });

    private readonly IEnumerable<LanguageWrapper> languageWrappers = Enum.GetValues<Language>().Select(lang => new LanguageWrapper { Language = lang });

    private IEnumerable<Programme>? requestProgrammes;
    private IEnumerable<Language>? requestLanguages;
    private int groupMembers = 1;
    private string? email;
    private IList<Topic> topics = new List<Topic>();
    private string? topicName;
    private IList<Supervisor> supervisors = new List<Supervisor>();
    private string? supervisorName;
    private readonly Request request = new();

    private void AddTopic() => topics.Add(new Topic { Name = topicName! });

    private void DeleteTopic(Topic topic) => topics.Remove(topic);

    private void AddSupervisor()
    {
        var supervisor = new Supervisor
        {
            FullName = supervisorName!,
            Email = "mail@hotmail.com",
            Profession = "Monkey",
            Topics = new Topic[] { }
        };
        supervisors?.Add(supervisor);
    }
    
    private void DeleteSupervisor(Supervisor supervisor) => supervisors?.Remove(supervisor);

    private async void SubmitRequestAsync()
    {
        var requestDto = new RequestCreateDto
        {
            Title = request.Title,
            Description = request.Description,
            Topics = topics,
            Languages = requestLanguages!,
            Programmes = requestProgrammes!,
            Members = new Student[] { },
            Supervisors = supervisors,
            Ects = request.Ects,
            Semester = request.Semester
        };

        var response = await httpClient.Client.PostAsJsonAsync("https://localhost:7094/requests", requestDto);

        if (response.IsSuccessStatusCode)
        {
            navManager.NavigateTo("/");
        }
        else
        {
            //Do something
        }
    }

    private void CancelRequest()
    {
        navManager.NavigateTo("/");
    }
}
