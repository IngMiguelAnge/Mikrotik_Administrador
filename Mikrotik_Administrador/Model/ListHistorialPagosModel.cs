using System;

namespace Mikrotik_Administrador.Model
{
    public class ListHistorialPagosModel
    {
        public int Id { get; set; }
        public DateTime FechaRecibido { get; set; }
        public decimal Cantidad { get; set; }
        public string Estatus { get; set; }
        public string Banco { get; set; }
        public string Referencia { get; set; }
        public string Responsable { get; set; }

    }
}
