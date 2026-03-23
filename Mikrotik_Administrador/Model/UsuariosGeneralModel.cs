namespace Mikrotik_Administrador.Model
{
    public class UsuariosGeneralModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Address { get; set; }
        public int IdMikrotik { get; set; }
        public bool Antena { get; set; }
        public string IdInterno { get; set; }
        public string Estatus { get; set; } 
        public int IdPlan { get; set; }
    }
}
