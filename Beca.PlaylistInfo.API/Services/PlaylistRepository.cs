using Beca.PlaylistInfo.API.DbContexts;
using Beca.PlaylistInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beca.PlaylistInfo.API.Services
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly PlaylistContext _context;

        public PlaylistRepository(PlaylistContext context)
        {

            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public async Task<Cancion?> GetCancionAsync(int cancionId, int playlistId)
        {
            return await _context.Canciones.Where(c => c.Id == cancionId && c.PlaylistId == playlistId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Cancion>> GetCancionesAsync(int playlistId)
        {
            
            return await _context.Canciones.Where(p => p.PlaylistId == playlistId).ToListAsync();   
        }

        public async Task<Playlist?> GetPlaylistAsync(int playlistId, bool incluyeCancion)
        {
            if (incluyeCancion)
            {
                return await _context.Playlists.Include(p => p.Canciones).Where(p => p.Id == playlistId).FirstOrDefaultAsync();
            }

            return await _context.Playlists.Where(p => p.Id == playlistId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsAsync()
        {
            return await _context.Playlists.ToListAsync();
            // version ordenada
            //return await _context.Playlists.OrderBy(c => c.Nombre).ToListAsync();
        }
        public async Task<IEnumerable<Playlist>> GetPlaylistsAsync(string? name, int pageNumber, int pageSize)
        {
            

            var collection = _context.Playlists as IQueryable<Playlist>;
            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Nombre == name);
            }
            

            return await collection.OrderBy(c => c.Nombre)
                .Skip(pageSize * (pageNumber -1))
                .Take(pageSize)
                .ToListAsync();
            // version ordenada
            //return await _context.Playlists.OrderBy(c => c.Nombre).ToListAsync();
        }

        public async Task<bool> PlaylistExistAsync(int playlistId)
        {
            return await _context.Playlists.AnyAsync(c => c.Id == playlistId);
        }

        public async Task AddCancionAsync(int playlistId, Cancion cancion)
        {
            var playlist = await GetPlaylistAsync(playlistId, false);
            if(playlist != null)
            {
                playlist.Canciones.Add(cancion);
            }

        }

        public async Task<bool> SavesChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void DeleteCancion(Cancion cancion)
        {
            _context.Canciones.Remove(cancion);
        }

    }
}
