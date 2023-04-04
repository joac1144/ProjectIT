using Moq;
using ProjectIT.Server.Controllers;
using ProjectIT.Server.Repositories;
using ProjectIT.Shared.Dtos.Projects;

namespace Controller.Tests;

public class ProjectsControllerTests
{
    [Fact]
    public async void GetAll_ReturnsProjects()
    {
        // Arrange.
        var repository = new Mock<IProjectsRepository>();
        repository.Setup(pr => pr.ReadAllAsync()).ReturnsAsync(Array.Empty<ProjectDetailsDto>());
        var controller = new ProjectsController(repository.Object);

        // Act.
        var result = await controller.GetAll();

        // Assert.
        result.Should().BeEquivalentTo(Array.Empty<ProjectDetailsDto>());
    }

    [Fact]
    public async void GetById_NonExistingId_ReturnsNull()
    {
        // Arrange.
        var repository = new Mock<IProjectsRepository>();
        repository.Setup(pr => pr.ReadByIdAsync(69)).ReturnsAsync(default(ProjectDetailsDto));
        var controller = new ProjectsController(repository.Object);

        // Act.
        var result = await controller.GetById(69);

        // Assert.
        result.Should().BeNull();
    }

    [Fact]
    public async void GetById_ExistingId_ReturnsProjectDetails()
    {
        // Arrange.
        var expected = new ProjectDetailsDto
        {
            Id = 1,
            Title = "Test 1"
        };
        var repository = new Mock<IProjectsRepository>();
        repository.Setup(pr => pr.ReadByIdAsync(1)).ReturnsAsync(expected);
        var controller = new ProjectsController(repository.Object);

        // Act.
        var result = await controller.GetById(1);

        // Assert.
        result.Should().Be(expected);
    }

    [Fact]
    public async void Post_ValidProject_ReturnsId()
    {
        // Arrange.
        var project = new ProjectCreateDto
        {
            Title = "Test 1"
        };
        var repository = new Mock<IProjectsRepository>();
        repository.Setup(pr => pr.CreateAsync(project)).ReturnsAsync(1);
        var controller = new ProjectsController(repository.Object);

        // Act.
        var result = await controller.Post(project);

        // Assert.
        result.Should().Be(1);
    }

    [Fact]
    public async void Post_InvalidProject_ReturnsNull()
    {
        // Arrange.
        var project = new ProjectCreateDto();
        var repository = new Mock<IProjectsRepository>();
        repository.Setup(pr => pr.CreateAsync(project)).ReturnsAsync(default(int?));
        var controller = new ProjectsController(repository.Object);

        // Act.
        var result = await controller.Post(project);

        // Assert.
        result.Should().BeNull();
    }

    [Fact]
    public async void Delete_ExistingId_ReturnsId() 
    {
        var repository = new Mock<IProjectsRepository>();
        repository.Setup(pr => pr.DeleteAsync(1)).ReturnsAsync(1);
        var controller = new ProjectsController(repository.Object);

        var result = await controller.Delete(1);

        result.Should().Be(1);
    }

    [Fact]
    public async void Delete_NonExistingId_ReturnsNull()
    {
        var repository = new Mock<IProjectsRepository>();
        repository.Setup(pr => pr.DeleteAsync(70)).ReturnsAsync(default(int?));
        var controller = new ProjectsController(repository.Object);

        var result = await controller.Delete(70);

        result.Should().BeNull();
    }

    [Fact]
    public async void Update_ExistingId_ReturnsId()
    {
        var ProjectUpdateDto = new ProjectUpdateDto
        {
            Id = 1,
            Title = "MyProject",
            Description = "This is my description"
        };

        var repository = new Mock<IProjectsRepository>();
        repository.Setup(pr => pr.UpdateAsync(ProjectUpdateDto)).ReturnsAsync(1);
        var controller = new ProjectsController(repository.Object);

        var result = await controller.Update(1, ProjectUpdateDto);

        result.Should().Be(1);
    }

    [Fact]
    public async void Update_NonExistingId_ReturnsNull()
    {
        var ProjectUpdateDto = new ProjectUpdateDto
        {
            Id = 400,
            Title = "MyProject",
            Description = "This is my description"
        };

        var repository = new Mock<IProjectsRepository>();
        repository.Setup(pr => pr.UpdateAsync(ProjectUpdateDto)).ReturnsAsync(default(int?));
        var controller = new ProjectsController(repository.Object);

        var result = await controller.Update(400, ProjectUpdateDto);

        result.Should().BeNull();

    }
}