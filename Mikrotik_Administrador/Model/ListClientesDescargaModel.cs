namespace Mikrotik_Administrador.Model
{
    public class ListClientesDescargaModel
    {
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public int IdUsuarioM { get; set; }
        public string Usuario { get; set; }
        public string IP { get; set; }
        public string Mikrotik {  get; set; }
        public string PlanOriginal { get; set; }
    }
}
