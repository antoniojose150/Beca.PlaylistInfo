using Beca.PlaylistInfo.API.Entities;

namespace Beca.PlaylistInfo.API.Services
{
    public interface IPlaylistRepository
    {

        Task<IEnumerable<Playlist>> GetPlaylistsAsync();

        Task<Playlist?> GetPlaylistAsync(int playlistId, bool incluyeCancion);

        Task<IEnumerable<Cancion>> GetCancionesAsync(int playlistId);

        Task<Cancion?> GetCancionAsync(int cancionId, int playlistId);
    }
}
