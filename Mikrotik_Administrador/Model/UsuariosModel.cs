using System;

namespace Mikrotik_Administrador.Model
{
    public class UsuariosModel
    {
        public int id { get; set; }
        public int idmikrotik { get; set; }
        public string mikrotik { get; set; } = string.Empty;
        public string idinterno { get; set; }
        public string name { get; set; }
        public string tipo { get; set; }
    }
}
