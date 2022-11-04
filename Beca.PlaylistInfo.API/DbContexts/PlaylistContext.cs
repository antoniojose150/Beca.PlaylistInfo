using Beca.PlaylistInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beca.PlaylistInfo.API.DbContexts
{
    public class PlaylistContext : DbContext
    {

        public DbSet<Playlist> Playlists { get; set; } = null!;

        public DbSet<Cancion> Canciones { get; set; } = null!;

        public PlaylistContext(DbContextOptions<PlaylistContext> options) :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Playlist>().HasData(
                new Playlist("linkin park")
                {
                    Id = 2,
                    Descripcion = "linking park lista"
                });
            modelBuilder.Entity<Cancion>().HasData(
                new Cancion("in the end")
                {
                    Id=2,
                    Descripcion = "cancion buenisima",
                    PlaylistId=2
                });
            base.OnModelCreating(modelBuilder);
        }


    }
}
