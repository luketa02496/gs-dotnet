using WorkWell.Api.Models;

namespace WorkWell.Api.Repositories
{
    public interface IUseresRepository
    {
        Task<IEnumerable<Useres>> GetAllAsync();
        Task<Useres?> GetByIdAsync(decimal id);
        Task<decimal> CreateAsync(Useres user);
        Task<bool> DeleteAsync(decimal id);
    }
}
