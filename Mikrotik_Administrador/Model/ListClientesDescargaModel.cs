namespace Mikrotik_Administrador.Model
{
    public class ListClientesDescargaModel
    {
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public int IdUsuarioM { get; set; }
        public string Usuario { get; set; }
        public string Estatus {  get; set; }
    }
}
