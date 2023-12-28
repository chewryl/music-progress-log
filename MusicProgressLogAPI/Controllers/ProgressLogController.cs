﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicProgressLogAPI.Models.Domain;
using MusicProgressLogAPI.Models.DTO;
using MusicProgressLogAPI.Repositories;

namespace MusicProgressLogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressLogController : ControllerBase
    {
        private readonly IProgressLogRepository _repository;
        private readonly IMapper _mapper;

        public ProgressLogController(IProgressLogRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var progressLogs = await _repository.GetAllAsync();
                var progressLogDtos = _mapper.Map<List<ProgressLogDto>>(progressLogs);

                return Ok(progressLogDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var progressLog = await _repository.GetByIdAsync(id);

                if (progressLog == null)
                {
                    return NotFound(id);
                }

                return Ok(_mapper.Map<ProgressLogDto>(progressLog));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddProgressLogRequestDto addProgressLogRequestDto)
        {
            ProgressLog progressLog;
            try
            {
                progressLog = _mapper.Map<ProgressLog>(addProgressLogRequestDto);
                progressLog = await _repository.CreateAsync(progressLog);
                var createdProgressLogDto = _mapper.Map<ProgressLogDto>(progressLog);

                return CreatedAtAction(nameof(Create), new { id = progressLog.Id }, createdProgressLogDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProgressLogRequestDto updateProgressLogRequestDto)
        {
            try
            {
                var updateProgressLogRequest = _mapper.Map<ProgressLog>(updateProgressLogRequestDto);

                var updatedProgressLog = await _repository.UpdateAsync(id, updateProgressLogRequest);

                if (updatedProgressLog == null)
                {
                    return NotFound(id);
                }
                
                return Ok(_mapper.Map<ProgressLogDto>(updatedProgressLog));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var deletedProgressLogId = await _repository.DeleteAsync(id);

                if (deletedProgressLogId == null)
                {
                    return NotFound(id);
                }

                return Ok(deletedProgressLogId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}