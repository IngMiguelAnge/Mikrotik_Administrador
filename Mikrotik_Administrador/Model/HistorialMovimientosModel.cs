using System;

namespace Mikrotik_Administrador.Model
{
    public class HistorialMovimientosModel
    {
        public int Id { get; set; }
        public string Descripcion {  get; set; }
        public string Pagina {  get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estatus {  get; set; }
        public string Address {  get; set; }
        public string Comment { get; set; }
        public bool IsAntena {  get; set; }
        public int IdMikrotik { get; set; }
    }
}
