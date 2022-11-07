namespace Beca.PlaylistInfo.API.Models
{
    public class CancionCreationDto
    {
        
        public string Nombre { get; set; }= string.Empty;

        public string? Descripcion { get; set; }

        public PlaylistDto? Playlist { get; set; }

        public int playlistId { get; set; } 
    }
}
