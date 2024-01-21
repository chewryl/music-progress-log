using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicProgressLogAPI.CustomActionFilters;
using MusicProgressLogAPI.Models.Domain;
using MusicProgressLogAPI.Models.DTO;
using MusicProgressLogAPI.Repositories.Interfaces;

namespace MusicProgressLogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PieceController : ControllerBase
    {
        private readonly IUserRepository<Piece> _repository;
        private readonly IMapper _mapper;

        public PieceController(IUserRepository<Piece> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{userId:Guid}")]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> GetAllForUser(Guid userId, [FromQuery] string? piece = null, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 30)
        {
            return Ok(await _repository.GetAllForUserWithFilterAsync(userId, "piece", piece));
        }


        [HttpGet]
        [Route("{userId:Guid}/{pieceId:Guid}")]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> GetById([FromRoute] Guid userId, [FromRoute] Guid pieceId)
        {
            var piece = await _repository.GetByIdAsync(pieceId);
            if (piece == null)
            {
                return NotFound(pieceId);
            }

            return Ok(piece);
        }

        [HttpPost]
        [Route("{userId:Guid}")]
        [ValidateModel]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> Create([FromRoute] Guid userId, [FromBody] PieceDto pieceDto)
        {
            if (pieceDto.UserId == Guid.Empty)
            {
                pieceDto.UserId = userId;
            }
            var piece = _mapper.Map<Piece>(pieceDto);
            return Ok(await _repository.CreateAsync(piece));
        }

        [HttpPut]
        [Route("{userId:Guid}/{pieceId:Guid}")]
        [ValidateModel]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> Update([FromRoute] Guid userId, [FromRoute] Guid pieceId, [FromBody] PieceDto piece)
        {
            var existing = await _repository.GetByIdAsync(pieceId);
            if (existing == null)
            {
                return NotFound(pieceId);
            }

            existing.Name = piece.Name;
            existing.Composer = piece.Composer;
            existing.Instrument = piece.Instrument;

            _repository.UpdateAsync(existing.Id, existing);
            await _repository.SaveAsync();

            return Ok(existing);
        }

        [HttpDelete]
        [Route("{userId:Guid}/{pieceId:Guid}")]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> Delete([FromRoute] Guid userId, [FromRoute] Guid pieceId)
        {
            var deletedPieceId = await _repository.DeleteAsync(pieceId);
            if (deletedPieceId == null)
            {
                return NotFound();
            }

            return Ok(deletedPieceId);
        }
    }
}
