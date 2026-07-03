using System;

namespace Mikrotik_Administrador.Model
{
    public class ListDetallesMensualidadModel
    {
        public string Descripcion { get; set; }
        public string Cantidad { get; set; }
        public string Estatus { get; set; }
        public DateTime FechaOrden { get; set; }
        public int OrdenVisual { get; set; }
    }
}
