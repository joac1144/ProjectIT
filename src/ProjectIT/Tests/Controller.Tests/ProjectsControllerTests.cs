using Microsoft.AspNetCore.Mvc;
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
    public async void GetById_NonExistingId_ReturnsNotFound()
    {
        // Arrange.
        var repository = new Mock<IProjectsRepository>();
        repository.Setup(pr => pr.ReadByIdAsync(69)).ReturnsAsync(default(ProjectDetailsDto));
        var controller = new ProjectsController(repository.Object);

        // Act.
        var result = await controller.GetById(69);

        // Assert.
        result.Result.Should().BeOfType(typeof(NotFoundResult));
    }

    [Fact]
    public async void GetById_ExistingId_ReturnsProjectDetails()
    {
        // Arrange.
        var expected = new ProjectDetailsDto
        {
            Title = "Test 1",
        };
        var repository = new Mock<IProjectsRepository>();
        repository.Setup(pr => pr.ReadByIdAsync(1)).ReturnsAsync(expected);
        var controller = new ProjectsController(repository.Object);

        // Act.
        var result = await controller.GetById(1);

        // Assert.
        result.Value.Should().Be(expected);
    }
}