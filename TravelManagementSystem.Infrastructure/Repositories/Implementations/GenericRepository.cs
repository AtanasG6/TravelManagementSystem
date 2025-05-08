using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystem.Application.Parameters.Base;
using TravelManagementSystem.Application.Repositories.Interfaces;
using TravelManagementSystem.Domain.Common;
using TravelManagementSystem.Infrastructure.Extensions;
using TravelManagementSystem.Infrastructure.Persistence;
using X.PagedList;

namespace TravelManagementSystem.Infrastructure.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenericRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IPagedList<T>> GetAllAsync(QueryParameters parameters)
        {
            var userId = _httpContextAccessor.HttpContext.GetUserId();
            var isAdmin = _httpContextAccessor.HttpContext.IsAdmin();

            var query = _dbSet.AsQueryable();

            if (!isAdmin)
            {
                query = query.Where(x => x.IsActive);
            }

            if (typeof(T).IsSubclassOf(typeof(AuditableEntity)) && userId.HasValue && !isAdmin)
            {
                query = query.Where(x => ((AuditableEntity)(object)x).CreatedById == userId.Value);
            }

            query = query
                .IncludeNavigationProperties()
                .ApplyFiltering(parameters)
                .ApplySorting(parameters);

            return await query.ToPagedListAsync(parameters.Page, parameters.PageSize);
        }


        //public async Task<int> CountAsync(QueryParameters parameters)
        //{
        //    var userId = _httpContextAccessor.HttpContext.GetUserId();
        //    var isAdmin = _httpContextAccessor.HttpContext.IsAdmin();

        //    var query = _dbSet.AsQueryable();

        //    if (!isAdmin)
        //    {
        //        query = query.Where(x => x.IsActive);

        //        if (typeof(T).IsSubclassOf(typeof(AuditableEntity)) && userId.HasValue)
        //        {
        //            query = query.Where(x => ((AuditableEntity)(object)x).CreatedById == userId.Value);
        //        }
        //    }

        //    query = query.ApplyFiltering(parameters);

        //    return await query.CountAsync();
        //}

        public async Task<T?> GetByIdAsync(int id)
        {
            var userId = _httpContextAccessor.HttpContext.GetUserId();
            var isAdmin = _httpContextAccessor.HttpContext.IsAdmin();

            var query = _dbSet
                .Where(x => x.Id == id)
                .IncludeNavigationProperties();

            if (!isAdmin)
            {
                query = query.Where(x => x.IsActive);

                if (typeof(T).IsSubclassOf(typeof(AuditableEntity)) && userId.HasValue)
                {
                    query = query.Where(x => ((AuditableEntity)(object)x).CreatedById == userId.Value);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            entity.CreatedOn = DateTime.UtcNow;
            entity.IsActive = true;

            if (entity is AuditableEntity auditableEntity)
            {
                var userId = _httpContextAccessor.HttpContext.GetUserId();
                if (userId.HasValue)
                {
                    auditableEntity.CreatedById = userId.Value;
                }
            }

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedOn = DateTime.UtcNow;

            if (entity is AuditableEntity auditableEntity)
            {
                var userId = _httpContextAccessor.HttpContext.GetUserId();
                var isAdmin = _httpContextAccessor.HttpContext.IsAdmin();
                if (auditableEntity.CreatedById != userId && !isAdmin)
                    throw new UnauthorizedAccessException("Нямате достъп до този запис.");
                if (userId.HasValue)
                {
                    auditableEntity.UpdatedById = userId.Value;
                }
            }

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsActive = false;
                entity.UpdatedOn = DateTime.UtcNow;

                if (entity is AuditableEntity auditableEntity)
                {
                    var userId = _httpContextAccessor.HttpContext.GetUserId();
                    var isAdmin = _httpContextAccessor.HttpContext.IsAdmin();
                    if (auditableEntity.CreatedById != userId && !isAdmin)
                        throw new UnauthorizedAccessException("Нямате достъп до този запис.");
                    if (userId.HasValue)
                    {
                        auditableEntity.UpdatedById = userId.Value;
                    }
                }

                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var isAdmin = _httpContextAccessor.HttpContext.IsAdmin();

            var query = _dbSet.AsQueryable();

            if (!isAdmin)
            {
                query = query.Where(x => x.IsActive);
            }

            return await query.AnyAsync(x => x.Id == id);
        }
    }
}
