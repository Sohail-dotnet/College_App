using System.Linq.Expressions;
using WebApplication1.Data;

namespace College_App.Data.Repository
{
    public interface ICollegeRepository<T>
    {

        Task<List<T>> GetAllAsync();

        public Task<T> GetByIdAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false);

        public Task<T> GetByNameAsync(Expression<Func<T, bool>> filter);

        Task<T> CreateAsync(T dbRecord);
        Task<T> UpdateAsync(T dbRecord);

        Task<bool> DeleteAsync(T dbRecord);
    }
}
