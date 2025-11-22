using WorkWell.Api.Models;

namespace WorkWell.Api.Repositories
{
    public interface IAssessmentRepository
    {
        Task<IEnumerable<Assessment>> GetAllByUserAsync(decimal useresId);
        Task<Assessment?> GetByIdAsync(decimal id);
        Task<decimal> CreateAsync(Assessment assessment);
        Task<bool> DeleteAsync(decimal id);
    }
}
