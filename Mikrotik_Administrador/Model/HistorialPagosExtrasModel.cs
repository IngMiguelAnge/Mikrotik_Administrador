using System;

namespace Mikrotik_Administrador.Model
{
    public class HistorialPagosExtrasModel
    {
        public int Id { get; set; }
        public DateTime FechaRecibido { get; set; }
        public decimal Cantidad { get; set; }
        public string Comentario { get; set; }
        public int IdBanco { get; set; }
        public string Banco { get; set; } = string.Empty;
        public string Referencia { get; set; }
        public byte[] Imagen { get; set; }
        public int IdExtra { get; set; }
        public int IdUsuario { get; set; }
    }
}
