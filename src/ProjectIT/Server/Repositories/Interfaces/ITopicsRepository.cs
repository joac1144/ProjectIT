using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Repositories.Interfaces;

public interface ITopicsRepository
{
    Task<IEnumerable<Topic>> ReadAllAsync();
}