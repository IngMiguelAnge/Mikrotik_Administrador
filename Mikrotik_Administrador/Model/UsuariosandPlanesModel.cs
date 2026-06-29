using Microsoft.SqlServer.Server;

namespace Mikrotik_Administrador.Model
{
    public class UsuariosandPlanesModel
    {
        public string Cliente { get; set; }
        public int IdUser { get; set; }
        public string Usuario { get; set; }
        public int IdPlan { get; set; }
        public string Plan { get; set; }
        public decimal Precio { get; set; }
        public string Velocidad { get; set; }
        public string EstatusServicio { get; set; }
        public string Mikrotik { get; set; }
        public int IdMes {  get; set; }
        public string FechaLimite {  get; set; }
    }
}
