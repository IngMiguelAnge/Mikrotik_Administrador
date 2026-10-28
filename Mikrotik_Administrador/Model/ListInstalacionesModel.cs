using System;

namespace Mikrotik_Administrador.Model
{
    public class ListInstalacionesModel
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public int IdUsuarioM { get; set; }
        public string UsuarioM { get; set; }
        public int IdUsuario { get; set; }
        public string Instalador { get; set; }
        public int Puerto { get; set; }
        public string Caja { get; set; }
        public string Spliter { get; set; }
        public int PuertoInterno { get; set; }
        public DateTime FechaInstalacion { get; set; }
    }
}
