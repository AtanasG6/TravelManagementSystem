using TravelManagementSystem.Application.Parameters.Base;
using TravelManagementSystem.Domain.Common;
using X.PagedList;

namespace TravelManagementSystem.Application.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IPagedList<T>> GetAllAsync(QueryParameters parameters);

        Task<T?> GetByIdAsync(int id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);

    }
}
