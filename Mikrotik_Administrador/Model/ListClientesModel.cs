namespace Mikrotik_Administrador.Model
{
    public class ListClientesModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Estatus { get; set; }
        public int? TotalServicios { get; set; }
    }
}
