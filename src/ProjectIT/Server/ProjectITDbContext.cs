using Microsoft.EntityFrameworkCore;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server;

public class ProjectITDbContext : DbContext
{
    public DbSet<Tag> Tags { get; set; } = null!;

    public ProjectITDbContext(DbContextOptions<ProjectITDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}