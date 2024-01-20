using AutoMapper;
using MusicProgressLogAPI.Models;
using MusicProgressLogAPI.Models.Domain;
using MusicProgressLogAPI.Repositories.Interfaces;
using MusicProgressLogAPI.Services.Interfaces;
using System.Net;

namespace MusicProgressLogAPI.Services
{
    public class ProgressLogService : IProgressLogService
    {
        private readonly IUserRepository<ProgressLog> _progressLogRepository;
        private readonly IRepository<Piece> _pieceRepository;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IRepository<AudioFile> _audioRepository;
        private readonly IMapper _mapper;

        public ProgressLogService(
            IUserRepository<ProgressLog> progressLogRepository,
            IRepository<Piece> pieceRepository,
            IRepository<ApplicationUser> userRepository,
            IRepository<AudioFile> audioRepository,
            IMapper mapper)
        {
            _progressLogRepository = progressLogRepository;
            _pieceRepository = pieceRepository;
            _userRepository = userRepository;
            _audioRepository = audioRepository;
            _mapper = mapper;
        }

        public async Task<ProgressLogConfig> AddProgressLogForUser(Guid userId, ProgressLog progressLog)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    return new ProgressLogConfig
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"User with UserRelationship Id '{userId}' does not exist."
                    };
                }

                var piece = await _pieceRepository.GetByIdAsync(progressLog.PieceId);
                if (piece == null)
                {
                    return new ProgressLogConfig
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"Piece with Id '{progressLog.PieceId}' does not exist."
                    };
                }

                progressLog.UserId = userId;

                progressLog = await _progressLogRepository.CreateAsync(progressLog);
                return new ProgressLogConfig
                {
                    StatusCode = HttpStatusCode.Created,
                    ProgressLogs = new List<ProgressLog> { progressLog }
                };
            }
            catch (Exception ex)
            {
                return new ProgressLogConfig
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public async Task<ProgressLogConfig> DeleteProgressLog(Guid progressLogId)
        {
            try
            {
                var deletedProgressLogId = await _progressLogRepository.DeleteAsync(progressLogId);
                if (deletedProgressLogId == null)
                {
                    return new ProgressLogConfig
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"Progress Log with Id '{progressLogId}' does not exist."
                    };
                }

                return new ProgressLogConfig
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = $"Progress Log with Id '{progressLogId}' deleted."
                };

            }
            catch (Exception ex)
            {
                return new ProgressLogConfig
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public async Task<ProgressLogConfig> GetAllProgressLogsForUser(Guid userId, string? filterOn = null, string? filterQuery = null, int pageNumber = 1, int pageSize = 30)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    return new ProgressLogConfig
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"User with UserRelationship Id '{userId}' does not exist."
                    };
                }

                var progressLogs = await _progressLogRepository.GetAllForUserWithFilterAsync(userId, filterOn, filterQuery, pageNumber, pageSize);
                return new ProgressLogConfig { StatusCode = HttpStatusCode.OK, ProgressLogs = progressLogs };
            }
            catch (Exception ex)
            {
                return new ProgressLogConfig
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public async Task<ProgressLogConfig> GetProgressLogForUser(Guid userId, Guid progressLogId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    return new ProgressLogConfig
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"User with UserRelationship Id '{userId}' does not exist."
                    };
                }

                var progressLog = await _progressLogRepository.GetByIdAsync(progressLogId);
                if (progressLog == null)
                {
                    return new ProgressLogConfig
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"Progress Log with Id '{progressLogId}' does not exist."
                    };
                }

                return new ProgressLogConfig
                {
                    StatusCode = HttpStatusCode.OK,
                    ProgressLogs = new List<ProgressLog> { progressLog }
                };
            }
            catch (Exception ex)
            {
                return new ProgressLogConfig
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public async Task<ProgressLogConfig> UpdateProgressLog(Guid progressLogId, ProgressLog progressLogToUpdate)
        {
            try
            {
                var progressLog = await _progressLogRepository.GetByIdAsync(progressLogId);
                if (progressLog == null)
                {
                    return new ProgressLogConfig
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"Progress Log with Id '{progressLogId}' does not exist."
                    };
                }

                var piece = await _pieceRepository.GetByIdAsync(progressLogToUpdate.PieceId);
                if (piece == null)
                {
                    return new ProgressLogConfig
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"Piece with Id '{progressLog.PieceId}' does not exist."
                    };
                }

                progressLog.Description = progressLogToUpdate.Description;
                progressLog.Title = progressLogToUpdate.Title;
                progressLog.PieceId = progressLogToUpdate.PieceId;
                
                if (progressLog.AudioFile == null && progressLogToUpdate.AudioFile != null)
                {
                    // Create audio
                    progressLogToUpdate.AudioFile.ProgressLogId = progressLogId;
                    var audioFile = await _audioRepository.CreateAsync(progressLogToUpdate.AudioFile);
                    progressLog.AudioFile = audioFile;
                }
                else if (progressLog.AudioFile != null && progressLogToUpdate.AudioFile != null)
                {
                    // Update existing audio
                    progressLog.AudioFile.FileName = progressLogToUpdate.AudioFile.FileName;
                    progressLog.AudioFile.FileData = progressLogToUpdate.AudioFile.FileData;
                    progressLog.AudioFile.FileLocation = progressLogToUpdate.AudioFile.FileLocation;
                    progressLog.AudioFile.MIMEType = progressLogToUpdate.AudioFile.MIMEType;
                }
                else if (progressLog.AudioFile != null && progressLogToUpdate.AudioFile == null)
                {
                    // Delete existing audio
                    var audioFileToRemove = progressLog.AudioFile;
                    progressLog.AudioFile = null;
                    await _audioRepository.DeleteAsync(audioFileToRemove.Id);
                }

                _progressLogRepository.UpdateAsync(progressLog.Id, progressLog);
                await _progressLogRepository.SaveAsync();

                return new ProgressLogConfig
                {
                    StatusCode = HttpStatusCode.OK,
                    ProgressLogs = new List<ProgressLog> { progressLog }
                };

            }
            catch (Exception ex)
            {
                return new ProgressLogConfig
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }
    }
}
