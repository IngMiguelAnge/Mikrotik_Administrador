using System;

namespace Mikrotik_Administrador.Model
{
    public class ListInstalacionesModel
    {
        public int Id { get; set; }
        public int IdUsuarioM { get; set; }
        public string Folio { get; set; }
        public DateTime Fecha_Instalacion { get; set; }
        public string Modalidad { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public decimal Total { get; set; }
    }
}
