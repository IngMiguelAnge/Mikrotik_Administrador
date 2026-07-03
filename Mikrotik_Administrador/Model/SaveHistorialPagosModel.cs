using System;

namespace Mikrotik_Administrador.Model
{
    public class SaveHistorialPagosModel
    {
        public int Id { get; set; }
        public int IdUsuarioM { get; set; }
        public DateTime FechaRecibido { get; set; }
        public decimal Cantidad { get; set; }
        public string Comentario { get; set; }
        public int IdBanco { get; set; }
        public string Referencia { get; set; }
        public byte[] Imagen { get; set; }
    }
}
