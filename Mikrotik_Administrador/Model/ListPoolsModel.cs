namespace Mikrotik_Administrador.Model
{
    public class ListPoolsModel
    {
        public int Id { get; set; }
        public string IP { get; set; }
        public int IdMikrotik {  get; set; }
        public string Mikrotik {  get; set; }
        public string Estatus { get; set; }
        public string Completado {  get; set; }
    }
}
