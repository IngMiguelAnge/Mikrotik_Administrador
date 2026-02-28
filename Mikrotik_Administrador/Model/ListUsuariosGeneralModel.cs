using System.IdentityModel.Protocols.WSTrust;
using System.Security.Cryptography.X509Certificates;

namespace Mikrotik_Administrador.Model
{
    public class ListUsuariosGeneralModel
    {
        public int Id { get; set; }
        public string Id_Interno { get; set; }
        public string Nombre { get; set; }
        public string Address { get; set; }
        public string Estatus { get; set; }
        public string Mikrotik { get; set; }
        public int? Id_Cliente { get; set; }
        public string Tipo { get; set; }
    }
}
