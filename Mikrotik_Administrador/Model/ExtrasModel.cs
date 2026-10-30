using System;
namespace Mikrotik_Administrador.Model
{
    public class ExtrasModel
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public decimal Costo { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public DateTime? Fecha_Recibido { get; set; }
        public DateTime? Fecha_Reparacion { get; set; }
        public DateTime? Fecha_Entregado {  get; set; }
        public DateTime? Fecha_Recogido { get; set; }
        public string Estatus { get; set; }
        public int IdUsuarioM { get; set; }
        public int? IdUsuarioRecibe { get; set; }
        public int? IdUsuarioRepara { get; set; }
        public int? IdUsuarioEntrega { get; set; }
        public int? IdUsuarioRecoge { get; set; }
        public string Nota { get; set; }
    }
}
