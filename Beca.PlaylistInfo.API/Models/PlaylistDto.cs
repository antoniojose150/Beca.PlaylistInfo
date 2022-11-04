using Beca.PlaylistInfo.API.Entities;

namespace Beca.PlaylistInfo.API.Models
{
    public class PlaylistDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = String.Empty;

        public string? Descripcion { get; set; }

        public ICollection<CancionDto> Canciones { get; set; } = new List<CancionDto>();
    }
}
