using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicProgressLogAPI.Models.Domain;
using MusicProgressLogAPI.Models.DTO;
using MusicProgressLogAPI.Services.Interfaces;
using System.Net;

namespace MusicProgressLogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [Route("{userRelationshipId:Guid}")]
        public async Task<IActionResult> Create([FromRoute] Guid userRelationshipId, [FromBody] ProgressLogDto progressLogRequestDto)
        {
            var progressLog = _mapper.Map<ProgressLog>(progressLogRequestDto);

            var progressLogConfig = await _progressLogService.AddProgressLogForUser(userRelationshipId, progressLog);

            if (progressLogConfig.StatusCode != HttpStatusCode.Created)
            {
                return StatusCode((int)progressLogConfig.StatusCode, progressLogConfig.Message);
            }

            return StatusCode((int)progressLogConfig.StatusCode, progressLogConfig.ProgressLogs.First());
        }

        [HttpGet]
        [Route("{userRelationshipId:Guid}")]
        public async Task<IActionResult> GetAllForUser([FromRoute] Guid userRelationshipId)
        {
            var progressLogConfig = await _progressLogService.GetAllProgressLogsForUser(userRelationshipId);

            if (progressLogConfig.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)progressLogConfig.StatusCode, progressLogConfig.Message);
            }

            return StatusCode((int)progressLogConfig.StatusCode, progressLogConfig.ProgressLogs);
        }

        [HttpGet]
        [Route("{userRelationshipId:Guid}/{progressLogId:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid userRelationshipId, [FromRoute] Guid progressLogId)
        {
            var progressLogConfig = await _progressLogService.GetProgressLogForUser(userRelationshipId, progressLogId);

            if (progressLogConfig.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)progressLogConfig.StatusCode, progressLogConfig.Message);
            }

            return StatusCode((int)progressLogConfig.StatusCode, progressLogConfig.ProgressLogs.First());
        }

        [HttpPut]
        [Route("{progressLogId:Guid}")]
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