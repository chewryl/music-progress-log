using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicProgressLogAPI.CustomActionFilters;
using MusicProgressLogAPI.Models.Domain;
using MusicProgressLogAPI.Models.DTO;
using MusicProgressLogAPI.Repositories.Interfaces;

namespace MusicProgressLogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PieceController : ControllerBase
    {
        private readonly IRepository<Piece> _repository;
        private readonly IMapper _mapper;

        public PieceController(IRepository<Piece> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var piece = await _repository.GetByIdAsync(id);
            if (piece == null)
            {
                return NotFound(id);
            }

            return Ok(piece);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] PieceDto pieceDto)
        {
            var piece = _mapper.Map<Piece>(pieceDto);
            return Ok(await _repository.CreateAsync(piece));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] PieceDto piece)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound(id);
            }

            existing.Name = piece.Name;
            existing.Composer = piece.Composer;
            existing.Instrument = piece.Instrument;

            _repository.UpdateAsync(existing.Id, existing);
            await _repository.SaveAsync();

            return Ok(existing);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedPieceId = await _repository.DeleteAsync(id);
            if (deletedPieceId == null)
            {
                return NotFound();
            }

            return Ok(deletedPieceId);
        }
    }
}
