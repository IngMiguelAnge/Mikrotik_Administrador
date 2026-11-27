using System;

namespace Mikrotik_Administrador.Items
{
    public class InstalacionModel
    {
        public int Id { get; set; }
        public int IdUsuarioM { get; set; }
        public int IdUsuario { get; set; }
        public string Folio {  get; set; }
        public DateTime Fecha_Instalacion {  get; set; }
        public string Modalidad {  get; set; }
        public string RB {  get; set; }
        public string Antena {  get; set; }
        public string Cable {  get; set; }
        public string Router {  get; set; }
        public string Cargador {  get; set; } 
        public string Tubo {  get; set; }
        public string IP {  get; set; }
        public string Sector {  get; set; }
        public int Caja { get; set; }
        public string Puerto_Externo { get; set; }
        public int Spliter { get; set; }
        public int Puerto_Interno { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public decimal Total { get; set; }
        public string Nota { get; set; }
    }
}
