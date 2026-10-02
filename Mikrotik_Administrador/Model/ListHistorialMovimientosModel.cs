using System;

namespace Mikrotik_Administrador.Model
{
    public class ListHistorialMovimientosModel
    {
        public int Id {  get; set; }
        public string Descripcion {  get; set; }
        public string Pagina { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaCreacion {  get; set; }
        public string Estatus { get; set; }
        public int IdMikrotik { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public bool IsAntena { get; set; }
    }
}
