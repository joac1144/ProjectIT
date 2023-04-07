using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ProjectIT.Server.Database;
using ProjectIT.Server.Repositories.Implementations;
using ProjectIT.Server.Repositories.Interfaces;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace Repository.Tests;

public class TopicsRepositoryTests : IDisposable
{
    private readonly IProjectITDbContext _context;
    private readonly ITopicsRepository _topicsRepository;

    public TopicsRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ProjectITDbContext>();
        builder.UseSqlite(connection);
        var context = new ProjectITDbContext(builder.Options);
        context.Database.EnsureCreated();

        context.AddRange(
            new Topic
            {
                Name = "Machine learning",
                Category = TopicCategory.ArtificialIntelligence
            },
            new Topic
            {
                Name = "Computer vision",
                Category = TopicCategory.ArtificialIntelligence
            },
            new Topic
            {
                Name = "C#",
                Category = TopicCategory.ProgrammingLanguages
            }
        );

        context.SaveChanges();

        _context = context;
        _topicsRepository = new TopicsRepository(context);
    }

    [Fact]
    public async void ReadAllAsync_ReturnsAllTopics()
    {
        IEnumerable<Topic> result = await _topicsRepository.ReadAllAsync();

        result.Should().NotBeNull().And.Match<IEnumerable<Topic>>(topics => topics.First().Name == "Machine learning" && topics.Last().Name == "C#");
    }

    public void Dispose() => _context.Dispose();
}