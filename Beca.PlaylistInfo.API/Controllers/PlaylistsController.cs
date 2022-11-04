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
        private readonly PlaylistRepository _playlistRepository;

        public PlaylistsController(PlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistWithoutCancionesDto>>> GetPlaylists()
        {
            var playlistEntities = await _playlistRepository.GetPlaylistsAsync();

            var results = new List<PlaylistWithoutCancionesDto>();
                foreach( var playlist in playlistEntities)
            {
                results.Add( new PlaylistWithoutCancionesDto
                {
                    Id = playlist.Id,
                    Nombre = playlist.Nombre,
                    Descripcion = playlist.Descripcion

                });
            }

            return Ok(results);
        }

    }
}
