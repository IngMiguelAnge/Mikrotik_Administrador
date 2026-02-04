namespace Mikrotik_Administrador.Model
{
    public class MikrotikModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string IP { get; set; }
        public string Port { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string IpMin { get; set; }
        public string IpMax { get; set; }
        public bool Estatus { get; set; }
        public bool Limite_Alcanzado { get; set; }
    }
}
