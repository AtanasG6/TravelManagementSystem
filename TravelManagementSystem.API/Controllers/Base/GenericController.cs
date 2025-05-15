using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FluentValidation;
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

        protected GenericController(
            IGenericService<TViewDto, TCreateDto, TUpdateDto, TPatchDto, TQueryParameters, TEntity> service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<TViewDto>>> GetAllAsync([FromQuery] TQueryParameters parameters)
        {
            try
            {
                var result = await _service.GetAllAsync(parameters);
                result.Message = "Данните са извлечени успешно.";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<TViewDto>.FailureResponse("Възникна грешка при обработката на заявката."));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TViewDto>>> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(ApiResponse<TViewDto>.FailureResponse("Невалидно ID."));
                }

                var data = await _service.GetByIdAsync(id);
                if (data == null)
                {
                    return NotFound(ApiResponse<TViewDto>.FailureResponse("Записът не е намерен."));
                }

                return Ok(ApiResponse<TViewDto>.SuccessResponse(data, "Данните са извлечени успешно."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<TViewDto>.FailureResponse("Възникна грешка при обработката на заявката."));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TViewDto>>> CreateAsync([FromBody] TCreateDto createDto)
        {
            try
            {
                if (createDto == null)
                {
                    return BadRequest(ApiResponse<TViewDto>.FailureResponse("Невалидни данни."));
                }

                var created = await _service.CreateAsync(createDto);
                return StatusCode(StatusCodes.Status201Created,
                    ApiResponse<TViewDto>.SuccessResponse(created, "Записът е създаден успешно."));
            }
            catch (ValidationException vex)
            {
                var errors = vex.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(ApiResponse<TViewDto>.FailureResponse(errors, "Грешка при валидация."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<TViewDto>.FailureResponse("Възникна грешка при обработката на заявката."));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<TViewDto>>> UpdateAsync(int id, [FromBody] TUpdateDto updateDto)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(ApiResponse<TViewDto>.FailureResponse("Невалидно ID."));
                }

                if (updateDto == null)
                {
                    return BadRequest(ApiResponse<TViewDto>.FailureResponse("Невалидни данни."));
                }

                var updated = await _service.UpdateAsync(id, updateDto);
                if (updated == null)
                {
                    return NotFound(ApiResponse<TViewDto>.FailureResponse("Записът не е намерен за обновяване."));
                }

                return Ok(ApiResponse<TViewDto>.SuccessResponse(updated, "Записът е обновен успешно."));
            }
            catch (ValidationException vex)
            {
                var errors = vex.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(ApiResponse<TViewDto>.FailureResponse(errors, "Грешка при валидация."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<TViewDto>.FailureResponse("Възникна грешка при обработката на заявката."));
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ApiResponse<TViewDto>>> PatchAsync(int id, [FromBody] JsonPatchDocument<TPatchDto> patchDocument)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(ApiResponse<TViewDto>.FailureResponse("Невалидно ID."));
                }

                if (patchDocument == null)
                {
                    return BadRequest(ApiResponse<TViewDto>.FailureResponse("Невалидни данни за частично обновяване."));
                }

                var patched = await _service.PatchAsync(id, patchDocument);
                if (patched == null)
                {
                    return NotFound(ApiResponse<TViewDto>.FailureResponse("Записът не е намерен за частично обновяване."));
                }

                return Ok(ApiResponse<TViewDto>.SuccessResponse(patched, "Записът е частично обновен успешно."));
            }
            catch (ValidationException vex)
            {
                var errors = vex.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(ApiResponse<TViewDto>.FailureResponse(errors, "Грешка при валидация."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<TViewDto>.FailureResponse("Възникна грешка при обработката на заявката."));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(ApiResponse<object>.FailureResponse("Невалидно ID."));
                }

                var exists = await _service.ExistsAsync(id);
                if (!exists)
                {
                    return NotFound(ApiResponse<object>.FailureResponse("Записът не е намерен за изтриване."));
                }

                await _service.DeleteAsync(id);
                return Ok(ApiResponse<object>.SuccessResponse(null, "Записът е изтрит успешно."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<object>.FailureResponse("Възникна грешка при обработката на заявката."));
            }
        }

        [HttpHead("{id}")]
        public async Task<ActionResult> ExistsAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                var exists = await _service.ExistsAsync(id);
                if (!exists)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}