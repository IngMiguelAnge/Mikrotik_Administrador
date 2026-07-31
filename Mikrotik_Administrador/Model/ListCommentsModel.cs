namespace Mikrotik_Administrador.Model
{
    public class ListCommentsModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdMikrotik { get; set; }
        public string Mikrotik { get; set; }
        public string Estatus { get; set; }
    }
}
