using System.ComponentModel.DataAnnotations;

namespace Beca.PlaylistInfo.API.Models
{
    public class CancionUpdateDto 
    {
        [Required(ErrorMessage = "you should provide a name value")]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Descripcion { get; set; } 

    }
}
