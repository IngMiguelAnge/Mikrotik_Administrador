using System;

namespace Mikrotik_Administrador.Model
{
    public class ListDetallesMensualidadModel
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estatus { get; set; }
        public string Programacion { get; set; }
        public string Plan { get; set; }
    }
}
