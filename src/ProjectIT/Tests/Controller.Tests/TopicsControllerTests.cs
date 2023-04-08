using Moq;
using ProjectIT.Server.Controllers;
using ProjectIT.Server.Repositories.Interfaces;
using ProjectIT.Shared.Models;

namespace Controller.Tests;

public class TopicsControllerTests
{
    [Fact]
    public async void GetAll_ReturnsTopics()
    {
        // Arrange.
        var repository = new Mock<ITopicsRepository>();
        repository.Setup(pr => pr.ReadAllAsync()).ReturnsAsync(Array.Empty<Topic>());
        var controller = new TopicsController(repository.Object);

        // Act.
        var result = await controller.GetAll();

        // Assert.
        result.Should().BeEquivalentTo(Array.Empty<Topic>());
    }
}