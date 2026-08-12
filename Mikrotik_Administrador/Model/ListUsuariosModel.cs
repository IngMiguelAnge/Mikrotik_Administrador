namespace Mikrotik_Administrador.Model
{
    public class ListUsuariosModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }       
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Estatus { get; set; }
        public int IdTipo { get; set; }
        public string TipoUsuario { get; set; }
    }
}
