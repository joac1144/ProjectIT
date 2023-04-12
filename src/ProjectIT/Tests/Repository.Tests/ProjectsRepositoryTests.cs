using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ProjectIT.Server.Database;
using ProjectIT.Server.Repositories.Implementations;
using ProjectIT.Server.Repositories.Interfaces;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace Repository.Tests;

public class ProjectsRepositoryTests : IDisposable
{
    private readonly IProjectITDbContext _context;
    private readonly IProjectsRepository _projectsRepository;

    private const string project1Title = "Title";
    private const string project2Title = "Test";

    public ProjectsRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ProjectITDbContext>();
        builder.UseSqlite(connection);
        var context = new ProjectITDbContext(builder.Options);
        context.Database.EnsureCreated();

        context.AddRange(
            new Project
            {
                Title = "Title", 
                DescriptionHtml = "Description",
                Topics = new Topic[] { },
                Languages = new[] { Language.English },
                Programmes = new[] { Programme.BDS },
                Ects = Ects.Bachelor,
                Semester = new() { Season = Season.Spring, Year = 2024 },
                Supervisor = new() { FullName = "test", Email = "test", Topics = new Topic[] { }, Profession = SupervisorProfession.FullProfessor, Status = SupervisorStatus.Available },
                Students = new Student[] { }
            },
            new Project
            {
                Title = "Test",
                DescriptionHtml = "Test desc",
                Topics = new[]
                {
                    new Topic
                    {
                        Name = "Test Topic",
                        Category = TopicCategory.ArtificialIntelligence
                    },
                    new Topic
                    {
                        Name = "Test Topic 2",
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
                Ects = Ects.Bachelor,
                Semester = new()
                {
                    Season = Season.Spring,
                    Year = 2023
                },
                Supervisor = new()
                {
                    FullName = "Joachim Alexander Kofoed",
                    Email = "jkof@itu.dk",
                    Topics = new Topic[] { },
                    Profession = SupervisorProfession.FullProfessor,
                    Status = SupervisorStatus.Inactive
                },
                Students = new Student[] { }
            }
        );

        context.SaveChanges();

        _context = context;
        _projectsRepository = new ProjectsRepository(context);
    }

    [Fact]
    public async void ReadAllAsync_ReturnsAllProjects()
    {
        IEnumerable<ProjectDetailsDto> result = await _projectsRepository.ReadAllAsync();

        result.Should().NotBeNull().And.Match<IEnumerable<ProjectDetailsDto>>(projects => projects.First().Title == project1Title && projects.Last().Title == project2Title);
    }

    [Fact]
    public async void ReadByIdAsync_NonExistingId_ReturnsNull()
    {
        ProjectDetailsDto? result = await _projectsRepository.ReadByIdAsync(420);
        result.Should().BeNull();
    }

    [Fact]
    public async void ReadByIdAsync_ExistingId_ReturnsProjectDetails()
    {
        ProjectDetailsDto? result = await _projectsRepository.ReadByIdAsync(2);

        result.Should().NotBeNull().And.Match<ProjectDetailsDto>(p => p.Title == project2Title);
    }

    [Fact]
    public async void CreateAsync_CreatesProjectSuccessfully()
    {
        var project = new ProjectCreateDto
        {
            Title = "Test",
            DescriptionHtml = "Test desc",
            Topics = new[]
                    {
                        new Topic
                        {
                            Name = "Test Topic",
                            Category = TopicCategory.ArtificialIntelligence
                        },
                        new Topic
                        {
                            Name = "Test Topic 2",
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
            Ects = Ects.Bachelor,
            Semester = new()
            {
                Season = Season.Spring,
                Year = 2023
            },
            Supervisor = new()
            {
                FullName = "Henrik Kjeldsen",
                Email = "henk@itu.dk",
                Topics = new Topic[] { },
                Profession = SupervisorProfession.FullProfessor,
                Status = SupervisorStatus.Available
            }
        };
        
        var resultId = await _projectsRepository.CreateAsync(project);
        var actualResult = await _context.Projects.FindAsync(resultId);

        actualResult.Should().NotBeNull().And.Match<Project>(p => p.Title == project.Title && p.DescriptionHtml == project.DescriptionHtml);
        actualResult!.Id.Should().Be(resultId);
    }

    [Fact]
    public async Task CreateAsync_MandatoryFieldsNotFilledOut_ThrowsException()
    {
        var project = new ProjectCreateDto
        {
            Title = String.Empty,
            DescriptionHtml = "Test desc",
            Topics = Array.Empty<Topic>(),
            Languages = Array.Empty<Language>(),
            Programmes = Array.Empty<Programme>(),
            Ects = Ects.Bachelor,
            Semester = new()
            {
                Season = Season.Spring,
                Year = 2023
            },
            Supervisor = new()
            {
                FullName = "Henrik Kjeldsen",
                Email = "henk@itu.dk",
                Topics = new Topic[] { },
                Profession = SupervisorProfession.FullProfessor,
                Status = SupervisorStatus.LimitedSupervision
            }
        };

        await Assert.ThrowsAsync<ArgumentNullException>(() => _projectsRepository.CreateAsync(project));
    }

    [Fact]
    public async Task DeleteAsync_ExistingId_ProjectDeleted()
    {
        var result = await _projectsRepository.DeleteAsync(1);

        var project = await _projectsRepository.ReadByIdAsync(1);

        project.Should().BeNull();
        result.Should().Be(1);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingId_ReturnsNull() 
    {
        var result = await _projectsRepository.DeleteAsync(3);

        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_ExistingId_ProjectUpdated()
    {
        var projectUpdateDto = new ProjectUpdateDto
        {
            Id = 1,
            Title = "TitleUpdated",
            DescriptionHtml = "DescriptionUpdated",
            Topics = new Topic[] { },
            Languages = new[] { Language.Danish },
            Programmes = new[] { Programme.MCS },
            Ects = Ects.Master,
            Semester = new() { Season = Season.Autumn, Year = 2025 },
            Supervisor = new() { FullName = "testUpdated", Email = "testUpdated", Topics = new Topic[] { }, Profession = SupervisorProfession.FullProfessor, Status = SupervisorStatus.Available },
        };

        var resultId = await _projectsRepository.UpdateAsync(projectUpdateDto);

        var updatedProject = await _projectsRepository.ReadByIdAsync(resultId);

        updatedProject.Should().Match<ProjectDetailsDto>(p => p.Title == projectUpdateDto.Title &&
                                                         p.DescriptionHtml == projectUpdateDto.DescriptionHtml &&
                                                         p.Topics == projectUpdateDto.Topics &&
                                                         p.Languages == projectUpdateDto.Languages &&
                                                         p.Programmes == projectUpdateDto.Programmes &&
                                                         p.Ects == projectUpdateDto.Ects &&
                                                         p.Semester == projectUpdateDto.Semester &&
                                                         p.Supervisor == projectUpdateDto.Supervisor 
                                                        );

        updatedProject?.Id.Should().Be(resultId);
    }

    [Fact]
    public async Task UpdateAsync_NonExistingId_ReturnsNull() 
    {
        var projectUpdateDto = new ProjectUpdateDto
        {
            Id = 10
        };

        var result = await _projectsRepository.UpdateAsync(projectUpdateDto);

        result.Should().BeNull();
    }

    public void Dispose() => _context.Dispose();
}