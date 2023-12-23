using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicProgressLogAPI.Data;
using MusicProgressLogAPI.Models.Domain;
using MusicProgressLogAPI.Models.DTO;

namespace MusicProgressLogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressLogController : ControllerBase
    {
        private readonly MusicProgressLogDbContext _dbContext;

        public ProgressLogController(MusicProgressLogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var progressLogs = await _dbContext.ProgressLogs.Include(x => x.AudioFile).ToListAsync();

                var progressLogsDtos = new List<ProgressLogDto>();
                foreach (var progressLog in progressLogs)
                {
                    var progressLogDto = new ProgressLogDto
                    {
                        Id = progressLog.Id,
                        Date = progressLog.Date,
                        Description = progressLog.Description,
                        Title = progressLog.Title
                    };

                    var audioFile = await _dbContext.AudioFiles.FirstOrDefaultAsync(x => x.Id == progressLog.AudioFile.Id);

                    if (audioFile != null)
                    {
                        progressLogDto.AudioFile = audioFile;
                    }

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
                var progressLog = await _dbContext.ProgressLogs.Include(x => x.AudioFile).FirstOrDefaultAsync(x => x.Id == id);

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

                await _dbContext.ProgressLogs.AddAsync(progressLogDomainModel);
                await _dbContext.SaveChangesAsync();

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
                var progressLog = await _dbContext.ProgressLogs.Include(x => x.AudioFile).FirstOrDefaultAsync(x => x.Id == id);
                if (progressLog == null)
                {
                    return NotFound(id);
                }

                progressLog.Title = progressLogDto.Title;
                progressLog.Description = progressLogDto.Description;
                progressLog.AudioFile.FileName = progressLogDto.AudioFile.FileName;
                progressLog.AudioFile.FileData = progressLogDto.AudioFile.FileData;
                progressLog.AudioFile.FileLocation = progressLogDto.AudioFile.FileLocation;
                progressLog.AudioFile.MIMEType = progressLogDto.AudioFile.MIMEType;

                await _dbContext.SaveChangesAsync();

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
                var progressLog = await _dbContext.ProgressLogs.Include(x => x.AudioFile).FirstOrDefaultAsync(x => x.Id == id);

                if (progressLog == null)
                {
                    return NotFound(id);
                }

                _dbContext.ProgressLogs.Remove(progressLog);
                _dbContext.AudioFiles.Remove(progressLog.AudioFile);
                await _dbContext.SaveChangesAsync();

                return Ok(progressLog.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
