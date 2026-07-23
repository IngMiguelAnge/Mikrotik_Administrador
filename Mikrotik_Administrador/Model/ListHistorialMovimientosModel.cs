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
    }
}
