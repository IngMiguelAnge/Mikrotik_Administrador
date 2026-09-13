using Microsoft.Identity.Client;

namespace Mikrotik_Administrador.Model
{
    public class UbicacionesCercanas
    {
        public int Id { get; set; }
        public string Servicio { get; set; }
        public string Address { get; set; }
        public string Estatus { get; set; }
        public string Mikrotik { get; set; }
    }
}
