using AutoMapper;
using Beca.PlaylistInfo.API.Models;
using Beca.PlaylistInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Beca.PlaylistInfo.API.Controllers
{


    [Route("api/playlist/{playlistid}/cancion")]
    [ApiController]
    public class CancionesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlaylistRepository _playlistRepository;

        public CancionesController(IPlaylistRepository playlistRepository, IMapper mapper)
        {
            _playlistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));    
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CancionDto>>> GetCanciones(int playlistid)
        {
            var cancionesEntity = await _playlistRepository.GetCancionesAsync(playlistid);
            return Ok(cancionesEntity);
            return Ok(_mapper.Map<IEnumerable<CancionDto>>(cancionesEntity));
        }

        [HttpGet("{cancionid}")]
        public async Task<ActionResult<CancionDto>> GetCancion(int playlistId, int cancionId)
        {
            var cancionEntity = await _playlistRepository.GetCancionAsync(cancionId, playlistId);
            return Ok(_mapper.Map<CancionDto>(cancionEntity));
        }

    }
}
