using AutoMapper;
using Beca.PlaylistInfo.API.Entities;
using Beca.PlaylistInfo.API.Models;
using Beca.PlaylistInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Beca.PlaylistInfo.API.Controllers
{

    [ApiController]
    [Route("api/playlist")]

    public class PlaylistsController : ControllerBase
    {
        private readonly ILogger<Playlist> _logger;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IMapper _mapper;
        const int maxPlaylistPageSize = 20;

        public PlaylistsController(ILogger<Playlist> logger,IPlaylistRepository playlistRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _playlistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));    
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistWithoutCancionesDto>>> GetPlaylists([FromQuery(Name ="filteronname")]string ? nombre
            , int pageNumber=1, int pageSize=10)
        {
            if (pageSize > maxPlaylistPageSize)
            {
                pageSize = maxPlaylistPageSize;
            }

            var playlistEntities = await _playlistRepository.GetPlaylistsAsync(nombre, pageNumber, pageSize);
            return Ok(_mapper.Map<IEnumerable<PlaylistWithoutCancionesDto>>(playlistEntities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaylist(int id, bool incluyecanciones = false)
        {
            var playlist = await _playlistRepository.GetPlaylistAsync(id,incluyecanciones);
            if (playlist == null)
            {
                return NotFound();
            }
            if (incluyecanciones)
            {
                return Ok(_mapper.Map<PlaylistDto>(playlist));
            }
            return Ok(_mapper.Map<PlaylistWithoutCancionesDto>(playlist));

        }

    }
}
