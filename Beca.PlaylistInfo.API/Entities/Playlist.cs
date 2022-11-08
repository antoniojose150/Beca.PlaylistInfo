using Microsoft.AspNetCore.Mvc;
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
        [MaxLength(50)]
        [FromQuery(Name = "filteronname")]
        public string Nombre { get; set; }

        [MaxLength(200)]
        public string ? Descripcion { get; set; }

        public ICollection<Cancion> Canciones { get; set; } = new List<Cancion>();


        public Playlist(string nombre)
        {
            Nombre = nombre;
        }

    }
}
