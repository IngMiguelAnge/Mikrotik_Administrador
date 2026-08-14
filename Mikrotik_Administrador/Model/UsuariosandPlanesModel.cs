using Microsoft.SqlServer.Server;

namespace Mikrotik_Administrador.Model
{
    public class UsuariosandPlanesModel
    {
        public string Identificador { get; set; }
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public int IdUser { get; set; }
        public string Usuario { get; set; }
        public int IdPlan { get; set; }
        public string Plan { get; set; }
        public string Estatus { get; set; }
        public string Mikrotik { get; set; }
        public string Mensualidad { get; set; }
    }
}
