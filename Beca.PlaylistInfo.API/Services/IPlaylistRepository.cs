using Beca.PlaylistInfo.API.Entities;

namespace Beca.PlaylistInfo.API.Services
{
    public interface IPlaylistRepository
    {

        Task<IEnumerable<Playlist>> GetPlaylistsAsync();

        Task<IEnumerable<Playlist>> GetPlaylistsAsync(string? name, int pageNumber, int pageSize);

        Task<Playlist?> GetPlaylistAsync(int playlistId, bool incluyeCancion);

        Task<IEnumerable<Cancion>> GetCancionesAsync(int playlistId);

        Task<Cancion?> GetCancionAsync(int cancionId, int playlistId);

        Task<bool> PlaylistExistAsync(int playlistId);

        Task AddCancionAsync(int playlistId, Cancion cancion);

        Task<bool> SavesChangesAsync();

        void DeleteCancion(Cancion cancion); 
    }
}
