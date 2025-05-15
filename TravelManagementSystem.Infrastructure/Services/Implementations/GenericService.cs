using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using TravelManagementSystem.Application.Parameters.Base;
using TravelManagementSystem.Application.Repositories.Interfaces;
using TravelManagementSystem.Application.Services.Interfaces;
using TravelManagementSystem.Application.Wrappers;
using TravelManagementSystem.Domain.Common;
using X.PagedList;

namespace TravelManagementSystem.Infrastructure.Services.Implementations
{
    public class GenericService<TViewDto, TCreateDto, TUpdateDto, TPatchDto, TQueryParameters, TEntity>
        : IGenericService<TViewDto, TCreateDto, TUpdateDto, TPatchDto, TQueryParameters, TEntity>
        where TEntity : BaseEntity
        where TViewDto : class
        where TCreateDto : class
        where TUpdateDto : class
        where TPatchDto : class
        where TQueryParameters : QueryParameters
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<TViewDto>> GetAllAsync(TQueryParameters parameters)
        {
            var pagedEntities = await _repository.GetAllAsync(parameters);

            var viewDtos = _mapper.Map<List<TViewDto>>(pagedEntities);

            var pagedDtos = new StaticPagedList<TViewDto>(
                viewDtos,
                pagedEntities.PageNumber,
                pagedEntities.PageSize,
                pagedEntities.TotalItemCount
            );

            return new PagedResponse<TViewDto>(pagedDtos);
        }

        public async Task<TViewDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? default : _mapper.Map<TViewDto>(entity);
        }

        public async Task<TViewDto> CreateAsync(TCreateDto createDto)
        {
            var entity = _mapper.Map<TEntity>(createDto);
            await _repository.AddAsync(entity);
            return _mapper.Map<TViewDto>(entity);
        }

        public async Task<TViewDto?> UpdateAsync(int id, TUpdateDto updateDto)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
                return default;

            _mapper.Map(updateDto, existingEntity);
            await _repository.UpdateAsync(existingEntity);

            return _mapper.Map<TViewDto>(existingEntity);
        }

        public async Task<TViewDto?> PatchAsync(int id, JsonPatchDocument<TPatchDto> patchDocument)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return default;

            // За да можем да работим с Patch, трябва да мапнем Entity -> TPatchDto
            var patchDto = _mapper.Map<TPatchDto>(entity);

            // Прилагаме Patch документа върху DTO
            patchDocument.ApplyTo(patchDto);

            // Мапваме обратно DTO-то върху Entity-то
            _mapper.Map(patchDto, entity);

            await _repository.UpdateAsync(entity);

            return _mapper.Map<TViewDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}
