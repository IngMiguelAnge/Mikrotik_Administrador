namespace Mikrotik_Administrador.Model
{
    public class UsuariosGeneralModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Address { get; set; }
        public int Id_Mikrotik { get; set; }
        public bool Antena { get; set; }
        public string Id_Interno { get; set; }
        public string Estatus { get; set; } 
    }
}
