using System;

namespace Mikrotik_Administrador.Items
{
    public class InstalacionModel
    {
        public int Id { get; set; }
        public int IdUsuarioM { get; set; }
        public int Puerto {  get; set; }
        public string Caja { get; set; }
        public string Spliter {  get; set; }
        public int Puerto_Interno {  get; set; }
        public int IdUsuario {  get; set; }
        public DateTime Fecha_Instalacion {  get; set; }
    }
}
