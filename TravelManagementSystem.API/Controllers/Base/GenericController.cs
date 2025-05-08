using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TravelManagementSystem.Application.Parameters.Base;
using TravelManagementSystem.Application.Services.Interfaces;
using TravelManagementSystem.Application.Wrappers;
using TravelManagementSystem.Domain.Common;

namespace TravelManagementSystem.API.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class GenericController<TViewDto, TCreateDto, TUpdateDto, TPatchDto, TQueryParameters, TEntity> : ControllerBase
        where TEntity : BaseEntity
        where TViewDto : class
        where TCreateDto : class
        where TUpdateDto : class
        where TPatchDto : class
        where TQueryParameters : QueryParameters
    {
        protected readonly IGenericService<TViewDto, TCreateDto, TUpdateDto, TPatchDto, TQueryParameters, TEntity> _service;

        protected GenericController(IGenericService<TViewDto, TCreateDto, TUpdateDto, TPatchDto, TQueryParameters, TEntity> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<TViewDto>>> GetAllAsync([FromQuery] TQueryParameters parameters)
        {
            var result = await _service.GetAllAsync(parameters);
            result.Message = "Данните са извлечени успешно.";
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TViewDto>>> GetByIdAsync(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound(ApiResponse<TViewDto>.FailureResponse(new List<string> { "Записът не е намерен." }));
            }
            return Ok(ApiResponse<TViewDto>.SuccessResponse(data, "Данните са извлечени успешно."));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TViewDto>>> CreateAsync([FromBody] TCreateDto createDto)
        {
            var created = await _service.CreateAsync(createDto);
            return Ok(ApiResponse<TViewDto>.SuccessResponse(created, "Записът е създаден успешно."));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<TViewDto>>> UpdateAsync(int id, [FromBody] TUpdateDto updateDto)
        {
            var updated = await _service.UpdateAsync(id, updateDto);
            if (updated == null)
            {
                return NotFound(ApiResponse<TViewDto>.FailureResponse(new List<string> { "Записът не е намерен за обновяване." }));
            }
            return Ok(ApiResponse<TViewDto>.SuccessResponse(updated, "Записът е обновен успешно."));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ApiResponse<TViewDto>>> PatchAsync(int id, [FromBody] JsonPatchDocument<TPatchDto> patchDocument)
        {
            var patched = await _service.PatchAsync(id, patchDocument);
            if (patched == null)
            {
                return NotFound(ApiResponse<TViewDto>.FailureResponse(new List<string> { "Записът не е намерен за частично обновяване." }));
            }
            return Ok(ApiResponse<TViewDto>.SuccessResponse(patched, "Записът е частично обновен успешно."));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteAsync(int id)
        {
            var exists = await _service.ExistsAsync(id);
            if (!exists)
            {
                return NotFound(ApiResponse<object>.FailureResponse(new List<string> { "Записът не е намерен за изтриване." }));
            }

            await _service.DeleteAsync(id);
            return Ok(ApiResponse<object>.SuccessResponse(null, "Записът е изтрит успешно."));
        }
    }
}
