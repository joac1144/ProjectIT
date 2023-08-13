using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ProjectIT.Server.Database;
using ProjectIT.Server.Repositories.Implementations;
using ProjectIT.Server.Repositories.Interfaces;
using ProjectIT.Shared.Dtos.Requests;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace Repository.Tests;

public class RequestsRepositoryTests : IDisposable
{
    private readonly IProjectITDbContext _context;
    private readonly IRequestsRepository _requestsRepository;

    public RequestsRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ProjectITDbContext>();
        builder.UseSqlite(connection);
        var context = new ProjectITDbContext(builder.Options);
        context.Database.EnsureCreated();

        var student2 = new Student { FirstName = "Current", LastName = "student2", Email = "klfwe@krld.dk" };
        var student3 = new Student { FirstName = "Current", LastName = "student3", Email = "efw@rldæ.dk" };

        context.AddRange(
            new Request
            {
                Title = "RequestTitle",
                DescriptionHtml = "RequestDescription",
                Topics = new Topic[] { },
                Languages = new[] { Language.Danish },
                Programmes = new[] { Programme.BSWU },
                Student = student2,
                ExtraMembers = new Student[] { student2 },
                Supervisors = new Supervisor[]
                {
                    new Supervisor
                    {
                        FirstName = "RequestSupervisor",
                        LastName = "",
                        Email = "RequestSupervisor@mail.dk",
                        Topics = new Topic[] { },
                        Profession = SupervisorProfession.AssistantProfessor,
                        Status = SupervisorStatus.Available
                    }
                },
                Ects = Ects.Bachelor,
                Semester = new() { Season = Season.Autumn, Year = 2025 },
                Status = RequestStatus.Pending
            },
            new Request
            {
                Title = "RequestTitle2",
                DescriptionHtml = "RequestDescription2",
                Topics = new[]
                {
                    new Topic
                    {
                        Name = "Request Topic",
                        Category = TopicCategory.ArtificialIntelligence
                    },
                    new Topic
                    {
                        Name = "Request Topic 2",
                        Category = TopicCategory.SoftwareEngineering
                    }
                },
                Languages = new[]
                {
                    Language.English
                },
                Programmes = new[]
                {
                    Programme.BSWU
                },
                Student = student3,
                ExtraMembers = new Student[] {student3},
                Supervisors = new Supervisor[]
                {
                    new Supervisor
                    {
                        FirstName = "RequestSupervisor2",
                        LastName = "",
                        Email = "RequestSupervisor2@mail.dk",
                        Topics = new Topic[] { },
                        Profession = SupervisorProfession.ExternalProfessor,
                        Status = SupervisorStatus.Inactive
                    }
                },
                Ects = Ects.Bachelor,
                Semester = new()
                {
                    Season = Season.Spring,
                    Year = 2023
                },
                Status = RequestStatus.Pending
            }
        );

        context.Students.Add(new Student
        {
            Email = "hello@itu.dk",
            FirstName = "Ulrich",
            LastName = "Kenneth"
        });
        
        context.Supervisors.Add(new Supervisor
        {
            Email = "henk@itu.dk",
            FirstName = "Banana",
            LastName = "Jakob"
        });

        context.Topics.AddRange(
            new Topic
            {
                Name = "Request Test Topic",
                Category = TopicCategory.ArtificialIntelligence
            },
            new Topic
            {
                Name = "Request Test Topic 2",
                Category = TopicCategory.SoftwareEngineering
            }
        );

        context.SaveChanges();

        _context = context;
        _requestsRepository = new RequestRepository(context);
    }

    [Fact]
    public async void CreateAsync_CreatesRequestSuccessfully()
    {
        var request = new RequestCreateDto
        {
            Title = "RequestTest",
            DescriptionHtml = "Request Test Desc",
            Topics = new[]
            {
                new Topic
                {
                    Name = "Request Test Topic",
                    Category = TopicCategory.ArtificialIntelligence
                },
                new Topic
                {
                    Name = "Request Test Topic 2",
                    Category = TopicCategory.SoftwareEngineering
                }
            },
            Languages = new[]
            {
                Language.English
            },
            Programmes = new[]
            {
                Programme.BSWU
            },
            ExtraMembersEmails = new string[] {"jlds@itu.dk"},
            SupervisorEmails = new string[] {"henk@itu.dk"}, 
            Ects = Ects.Bachelor,
            Semester = new()
            {
                Season = Season.Spring,
                Year = 2023
            },
            Status = RequestStatus.Pending,
            StudentEmail = "hello@itu.dk"
        };

        var resultId = await _requestsRepository.CreateAsync(request);
        var actualResult = await _context.Requests.FindAsync(resultId);

        actualResult.Should().NotBeNull().And.Match<Request>(p => p.Title == request.Title && p.DescriptionHtml == request.DescriptionHtml);
        actualResult?.Id.Should().Be(resultId);
    }

    [Fact]
    public async Task CreateAsync_MandatoryFieldsNotFilledOut_ThrowsException()
    {
        var request = new RequestCreateDto
        {
            Title = String.Empty,
            DescriptionHtml = "Test desc",
            Topics = Array.Empty<Topic>(),
            Languages = Array.Empty<Language>(),
            Programmes = Array.Empty<Programme>(),
            ExtraMembersEmails = Array.Empty<string>(),
            SupervisorEmails = Array.Empty<string>(),
            Ects = Ects.Bachelor,
            Semester = new()
            {
                Season = Season.Spring,
                Year = 2023
            },
            Status = RequestStatus.Pending,
            StudentEmail = "hello@itu.dk"
        };

        await Assert.ThrowsAsync<ArgumentNullException>(() => _requestsRepository.CreateAsync(request));
    }

    [Fact]
    public async void ReadAllAsync_ReturnsAllRequests()
    {
        IEnumerable<RequestDetailsDto> result = await _requestsRepository.ReadAllAsync();

        result.Should().NotBeNull().And.Match<IEnumerable<RequestDetailsDto>>(requests => requests.First().Title == "RequestTitle" && requests.Last().Title == "RequestTitle2");
    }

    [Fact]
    public async void ReadByIdAsync_NonExistingId_ReturnsNull()
    {
        RequestDetailsDto? result = await _requestsRepository.ReadByIdAsync(420);

        result.Should().BeNull();
    }

    [Fact]
    public async void ReadByIdAsync_ExistingId_ReturnsRequestDetails()
    {
        RequestDetailsDto? result = await _requestsRepository.ReadByIdAsync(2);

        result.Should().NotBeNull().And.Match<RequestDetailsDto>(r => r.Title == "RequestTitle2");
    }

    public void Dispose() => _context.Dispose();
}