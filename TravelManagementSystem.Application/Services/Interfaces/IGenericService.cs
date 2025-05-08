using Microsoft.AspNetCore.JsonPatch;
using TravelManagementSystem.Application.Parameters.Base;
using TravelManagementSystem.Application.Wrappers;
using TravelManagementSystem.Domain.Common;

namespace TravelManagementSystem.Application.Services.Interfaces
{
    public interface IGenericService<TViewDto, TCreateDto, TUpdateDto, TPatchDto, TQueryParameters, TEntity>
        where TEntity : BaseEntity
        where TViewDto : class
        where TCreateDto : class
        where TUpdateDto : class
        where TPatchDto : class
        where TQueryParameters : QueryParameters    
    {
        Task<PagedResponse<TViewDto>> GetAllAsync(TQueryParameters parameters);

        Task<TViewDto?> GetByIdAsync(int id);

        Task<TViewDto> CreateAsync(TCreateDto createDto);

        Task<TViewDto?> UpdateAsync(int id, TUpdateDto updateDto);

        Task<TViewDto?> PatchAsync(int id, JsonPatchDocument<TPatchDto> patchDocument);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}
