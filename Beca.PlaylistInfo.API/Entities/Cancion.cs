using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beca.PlaylistInfo.API.Entities
{
    public class Cancion
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [MaxLength(200)]
        public string? Descripcion { get; set; }

        [ForeignKey("PlaylistId")]
        public Playlist? Playlist { get; set; }

        public int PlaylistId { get; set; }


        public Cancion( string nombre)
        {
            Nombre = nombre;
        }
    }
}
