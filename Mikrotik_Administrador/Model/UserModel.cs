namespace Mikrotik_Administrador.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public bool Estatus { get; set; }
        public int Id_TipoUsuario { get; set; }
    }
}
