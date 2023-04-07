using Microsoft.EntityFrameworkCore;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Database;

public interface IProjectITDbContext : IDisposable
{
    DbSet<Topic> Topics { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Project> Projects { get; set; }
    DbSet<Request> Requests { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}