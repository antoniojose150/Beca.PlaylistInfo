using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beca.PlaylistInfo.API.Entities
{
    public class Playlist
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(300)]
        public string ? Descripcion { get; set; }

        public ICollection<Cancion> Canciones { get; set; } = new List<Cancion>();


        public Playlist(string nombre)
        {
            Nombre = nombre;
        }

    }
}
