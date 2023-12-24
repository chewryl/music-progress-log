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

        public ProgressLogController(IProgressLogRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var progressLogs = await _repository.GetAllAsync();

                var progressLogsDtos = new List<ProgressLogDto>();
                foreach (var progressLog in progressLogs)
                {
                    var progressLogDto = new ProgressLogDto
                    {
                        Id = progressLog.Id,
                        Date = progressLog.Date,
                        Description = progressLog.Description,
                        Title = progressLog.Title,
                        AudioFile = progressLog.AudioFile
                    };

                    progressLogsDtos.Add(progressLogDto);
                }

                return Ok(progressLogsDtos);
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

                var progressLogDto = new ProgressLogDto
                {
                    Id = progressLog.Id,
                    Date = progressLog.Date,
                    Description = progressLog.Description,
                    Title = progressLog.Title,
                    AudioFile = progressLog.AudioFile
                };

                return Ok(progressLogDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddProgressLogRequestDto addProgressLogRequestDto)
        {
            ProgressLog progressLogDomainModel;
            try
            {
                progressLogDomainModel = new ProgressLog
                {
                    Title = addProgressLogRequestDto.Title,
                    Date = addProgressLogRequestDto.Date,
                    Description = addProgressLogRequestDto.Description,
                    AudioFile = addProgressLogRequestDto.AudioFile
                };

                progressLogDomainModel = await _repository.CreateAsync(progressLogDomainModel);

                // Map domain model back to DTO to send back
                var progressLogDto = new ProgressLogDto()
                {
                    Id = progressLogDomainModel.Id,
                    Title = progressLogDomainModel.Title,
                    Date = progressLogDomainModel.Date,
                    Description = progressLogDomainModel.Description,
                    AudioFile = progressLogDomainModel.AudioFile
                };

                return CreatedAtAction(nameof(Create), new { id = progressLogDomainModel.Id }, progressLogDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProgressLogRequestDto progressLogDto)
        {
            try
            {
                var progressLog = await _repository.UpdateAsync(id, new ProgressLog
                {
                    Title = progressLogDto.Title,
                    Description = progressLogDto.Description,
                    AudioFile = new AudioFile
                    {
                        FileName = progressLogDto.AudioFile.FileName,
                        FileData = progressLogDto.AudioFile.FileData,
                        FileLocation = progressLogDto.AudioFile.FileLocation,
                        MIMEType = progressLogDto.AudioFile.MIMEType
                    }
                });

                if (progressLog == null)
                {
                    return NotFound(id);
                }

                // convert updated domain model to DTO to send back
                var progressLogUpdatedDto = new ProgressLogDto
                {
                    Id = progressLog.Id,
                    Title = progressLog.Title,
                    Description = progressLog.Description,
                    Date = progressLog.Date,
                    AudioFile = progressLog.AudioFile
                };

                return Ok(progressLogUpdatedDto);
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
