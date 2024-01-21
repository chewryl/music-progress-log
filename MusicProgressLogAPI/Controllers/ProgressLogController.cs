using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicProgressLogAPI.CustomActionFilters;
using MusicProgressLogAPI.Models.Domain;
using MusicProgressLogAPI.Models.DTO;
using MusicProgressLogAPI.Services.Interfaces;
using System.Net;

namespace MusicProgressLogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProgressLogController : ControllerBase
    {
        private readonly IProgressLogService _progressLogService;
        private readonly IMapper _mapper;

        public ProgressLogController(IProgressLogService progressLogService, IMapper mapper)
        {
            _progressLogService = progressLogService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("{userId:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Create([FromRoute] Guid userId, [FromBody] ProgressLogDto progressLogRequestDto)
        {
            var progressLog = _mapper.Map<ProgressLog>(progressLogRequestDto);

            var progressLogConfig = await _progressLogService.AddProgressLogForUser(userId, progressLog);

            if (progressLogConfig.StatusCode != HttpStatusCode.Created)
            {
                return StatusCode((int)progressLogConfig.StatusCode, progressLogConfig.Message);
            }

            return StatusCode((int)progressLogConfig.StatusCode, progressLogConfig.ProgressLogs.First());
        }

        [HttpGet]
        [Route("{userId:Guid}")]
        public async Task<IActionResult> GetAllForUser([FromRoute] Guid userId, [FromQuery] string? filterOn = null, [FromQuery] string? filterQuery = null, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 30)
        {
            var progressLogConfig = await _progressLogService.GetAllProgressLogsForUser(userId, filterOn, filterQuery, pageNumber, pageSize);

            if (progressLogConfig.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)progressLogConfig.StatusCode, progressLogConfig.Message);
            }

            return StatusCode((int)progressLogConfig.StatusCode, progressLogConfig.ProgressLogs);
        }

        [HttpGet]
        [Route("{userId:Guid}/{progressLogId:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid userId, [FromRoute] Guid progressLogId)
        {
            var progressLogConfig = await _progressLogService.GetProgressLogForUser(userId, progressLogId);

            if (progressLogConfig.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)progressLogConfig.StatusCode, progressLogConfig.Message);
            }

            return StatusCode((int)progressLogConfig.StatusCode, progressLogConfig.ProgressLogs.First());
        }

        [HttpPut]
        [Route("{progressLogId:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid progressLogId, [FromBody] ProgressLogDto progressLogToUpdateDto)
        {
            var progressLog = _mapper.Map<ProgressLog>(progressLogToUpdateDto);
            var progressLogConfig = await _progressLogService.UpdateProgressLog(progressLogId, progressLog);

            if (progressLogConfig.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)progressLogConfig.StatusCode, progressLogConfig.Message);
            }

            return StatusCode((int)progressLogConfig.StatusCode, progressLogConfig.ProgressLogs.First());
        }

        [HttpDelete]
        [Route("{progressLogId:Guid}")]
        public async Task<IActionResult> Delete(Guid progressLogId)
        {
            var progressLogConfig = await _progressLogService.DeleteProgressLog(progressLogId);
            return StatusCode((int)progressLogConfig.StatusCode, progressLogConfig.Message);
        }
    }
}