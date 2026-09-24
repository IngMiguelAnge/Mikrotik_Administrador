namespace Mikrotik_Administrador.Model
{
    public class UsuariosGeneralModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Address { get; set; }
        public int IdMikrotik { get; set; }
        public string IdInterno { get; set; }
        public string Estatus { get; set; } 
        public int IdCliente {  get; set; }
        public int IdPlan { get; set; }
        public int IdPlanOriginal { get; set; }
        public int IdMikrotikOriginal {  get; set; }
    }
}
