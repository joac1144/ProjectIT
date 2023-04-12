using ProjectIT.Client.Shared.Enums;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Client.Pages.Supervisor.MyProjects;

public partial class MyProjectsSupervisor
{
    private IEnumerable<ProjectDetailsDto> projects; // List of supervisor's projects

    private string? sortValue;
    private readonly IEnumerable<Sort> _sortValues = Enum.GetValues<Sort>();

    protected override async Task OnInitializedAsync()
    {
        // Fetch supervisor's projects.
        projects = new List<ProjectDetailsDto>() 
        {
            new ProjectDetailsDto 
            {
                Id = 777, 
                Title = "Test", 
                DescriptionHtml = "Some description",
                Programmes = new[] { Programme.BDS },
                Ects = Ects.Bachelor,
                Languages = new[] { Language.English },
                Semester = new() { Season = Season.Autumn, Year = 2024 },
                Topics = new[] { new Topic { Name = "Topic for testing", Category = TopicCategory.Security } },
                Supervisor = new() { FullName = "Test Supervisor name", Email = "somemail@itu.dk", Profession = "mmm", Topics = Array.Empty<Topic>() },
                Students = new[] { new Student { FullName = "Test Student name", Email = "teststudent@itu.dk", Programme = Programme.MGAMES } }
            },
            new ProjectDetailsDto
            {
                Id = 666,
                Title = "Test 2",
                DescriptionHtml = "Some description 2",
                Programmes = new[] { Programme.BSWU },
                Ects = Ects.Master,
                Languages = new[] { Language.English },
                Semester = new() { Season = Season.Autumn, Year = 2024 },
                Topics = new[] { new Topic { Name = "Topic for testing", Category = TopicCategory.Security } },
                Supervisor = new() { FullName = "Test Supervisor name", Email = "somemail@itu.dk", Profession = "mmm", Topics = Array.Empty<Topic>() },
                Students = Array.Empty<Student>()
            }
        }; /*await httpClient.GetFromJsonAsync<IEnumerable<Supervisor>>("supervisors").Select(supervisor => supervisor.Projects);*/
    }

    private void OnSort(object value)
    {
        if (projects != null && value.GetType() == typeof(string))
        {
            sortValue = value.ToString();
            switch (value)
            {
                case nameof(Sort.Semester):
                    projects = projects.OrderBy(p => p.Semester).ToList();
                    break;
                case nameof(Sort.ECTS):
                    projects = projects.OrderBy(p => p.Ects).ToList();
                    break;
            }
        }
    }

    private void DeleteProject(int id)
    {

    }
}