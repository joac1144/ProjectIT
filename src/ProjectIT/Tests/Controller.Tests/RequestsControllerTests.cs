using Moq;
using ProjectIT.Server.Controllers;
using ProjectIT.Server.Repositories.Interfaces;
using ProjectIT.Shared.Dtos.Requests;

namespace Controller.Tests;

public class RequestsControllerTests
{
    [Fact]
    public async void GetAll_ReturnsRequests()
    {
        // Arrange.
        var repository = new Mock<IRequestsRepository>();
        repository.Setup(r => r.ReadAllAsync()).ReturnsAsync(Array.Empty<RequestDetailsDto>());
        var controller = new RequestsController(repository.Object);

        // Act.
        var result = await controller.GetAll();

        // Assert.
        result.Should().BeEquivalentTo(Array.Empty<RequestDetailsDto>());
    }

    [Fact]
    public async void GetById_NonExistingId_ReturnsNull()
    {
        // Arrange.
        var repository = new Mock<IRequestsRepository>();
        repository.Setup(r => r.ReadByIdAsync(69)).ReturnsAsync(default(RequestDetailsDto));
        var controller = new RequestsController(repository.Object);

        // Act.
        var result = await controller.GetById(69);

        // Assert.
        result.Should().BeNull();
    }

    [Fact]
    public async void GetById_ExistingId_ReturnsRequestDetails()
    {
        // Arrange.
        var expected = new RequestDetailsDto
        {
            Id = 1,
            Title = "Test 1"
        };
        var repository = new Mock<IRequestsRepository>();
        repository.Setup(r => r.ReadByIdAsync(1)).ReturnsAsync(expected);
        var controller = new RequestsController(repository.Object);

        // Act.
        var result = await controller.GetById(1);

        // Assert.
        result.Should().Be(expected);
    }

    [Fact]
    public async void Post_ValidRequest_ReturnsId()
    {
        // Arrange.
        var request = new RequestCreateDto
        {
            Title = "Test 1"
        };
        var repository = new Mock<IRequestsRepository>();
        repository.Setup(r => r.CreateAsync(request)).ReturnsAsync(1);
        var controller = new RequestsController(repository.Object);

        // Act.
        var result = await controller.Post(request);

        // Assert.
        result.Should().Be(1);
    }

    [Fact]
    public async void Post_InvalidRequest_ReturnsNull()
    {
        // Arrange.
        var request = new RequestCreateDto();
        var repository = new Mock<IRequestsRepository>();
        repository.Setup(r => r.CreateAsync(request)).ReturnsAsync(default(int?));
        var controller = new RequestsController(repository.Object);

        // Act.
        var result = await controller.Post(request);

        // Assert.
        result.Should().BeNull();
    }
}