﻿namespace Beca.PlaylistInfo.API.Models
{
    public class CancionDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }
    }
}
