using Microsoft.EntityFrameworkCore;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Database;

public interface IProjectITDbContext : IDisposable
{
    DbSet<Topic> Topics { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<project> Projects { get; set; }
}