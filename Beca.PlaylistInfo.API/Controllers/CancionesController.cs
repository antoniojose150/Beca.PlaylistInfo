using AutoMapper;
using Beca.PlaylistInfo.API.Entities;
using Beca.PlaylistInfo.API.Models;
using Beca.PlaylistInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Beca.PlaylistInfo.API.Controllers
{


    [Route("api/playlist/{playlistid}/cancion")]
    [ApiController]
    public class CancionesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CancionesController> _logger;
        private readonly IPlaylistRepository _playlistRepository;

        public CancionesController(ILogger<CancionesController> logger ,IPlaylistRepository playlistRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)) ;
            _playlistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));    
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CancionDto>>> GetCanciones(int playlistId)
        {
            if (!await _playlistRepository.PlaylistExistAsync(playlistId))
            {
                _logger.LogInformation($"No se ha encontrado una playlist con la id {playlistId}");
                return NotFound();
            }

            var cancionesEntity = await _playlistRepository.GetCancionesAsync(playlistId);
            return Ok(_mapper.Map<IEnumerable<CancionDto>>(cancionesEntity));
        }

        [HttpGet("{cancionid}", Name = "GetCancion")]
        public async Task<ActionResult<CancionDto>> GetCancion(int playlistId, int cancionId)
        {
            if (!await _playlistRepository.PlaylistExistAsync(playlistId))
            {
                return NotFound();
            }

            var cancionEntity = await _playlistRepository.GetCancionAsync(cancionId, playlistId);

            if (cancionEntity == null)
            {
                return NotFound();
            } 

            return Ok(_mapper.Map<CancionDto>(cancionEntity));
        }

        [HttpPost]
        public async Task<ActionResult<CancionDto>> CreateCancion(int playlistId, CancionCreationDto cancion)
        {

            if (!await _playlistRepository.PlaylistExistAsync(playlistId))
            {
                return NotFound();
            }

            var Cancioncreada = _mapper.Map<Entities.Cancion>(cancion);
            
            await _playlistRepository.AddCancionAsync(playlistId,Cancioncreada);

            await _playlistRepository.SavesChangesAsync();

            var cancionToReturn = _mapper.Map<Models.CancionDto>(Cancioncreada);


            return CreatedAtRoute("GetCancion", 
                new
                {
                    playlistId = playlistId,
                    cancionId = cancionToReturn.Id
                }, cancionToReturn
                );


        }
        [HttpPut("{cancionId}")]
        public async Task<ActionResult> UpgrateCancion(int playlistId,int cancionId, CancionUpdateDto cancion)
        {
            
            if (!await _playlistRepository.PlaylistExistAsync(playlistId))
            {
                return NotFound();
            }

            var cancionEntity = await _playlistRepository.GetCancionAsync(cancionId, playlistId);
            if (cancionEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(cancion,cancionEntity);

            await _playlistRepository.SavesChangesAsync();

            return NoContent();


        }

        [HttpPatch("{cancionId}")]
        public async Task<ActionResult> PartiallyUpgrateCancion(int playlistId, int cancionId, JsonPatchDocument<CancionUpdateDto> patchDocument
            )
        {

            if (!await _playlistRepository.PlaylistExistAsync(playlistId))
            { 
                return NotFound();
            }

            var cancionEntity = await _playlistRepository.GetCancionAsync(cancionId, playlistId);
            if (cancionEntity == null)
            {
                return NotFound();
            }

            var cancionPath = _mapper.Map<CancionUpdateDto>(cancionEntity);

            patchDocument.ApplyTo(cancionPath, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(cancionPath))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(cancionPath, cancionEntity);
            await _playlistRepository.SavesChangesAsync();

            return NoContent();


        }

        [HttpDelete("{cancionId}")]
        public async Task<ActionResult> DeleteCancion(int playlistId, int cancionId)
        {

            var cancionEntity = await _playlistRepository.GetCancionAsync(cancionId, playlistId);
            if (cancionEntity == null)
            {
                return NotFound();
            }


            _playlistRepository.DeleteCancion(cancionEntity);
            await _playlistRepository.SavesChangesAsync();


            return NoContent();

        } 

    }
}
