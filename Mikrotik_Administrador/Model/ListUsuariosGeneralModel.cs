using System.IdentityModel.Protocols.WSTrust;
using System.Security.Cryptography.X509Certificates;

namespace Mikrotik_Administrador.Model
{
    public class ListUsuariosGeneralModel
    {
        public int Id { get; set; }
        public string IdInterno { get; set; }
        public string Usuario { get; set; }
        public string Address { get; set; }
        public string Estatus { get; set; }
        public int IdMikrotik { get; set; }
        public string Mikrotik { get; set; }
        public int? IdCliente { get; set; }
        public string Cliente { get; set; }
        public string Tipo { get; set; }
    }
}
