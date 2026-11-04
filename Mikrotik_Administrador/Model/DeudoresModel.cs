namespace Mikrotik_Administrador.Model
{
    public class DeudoresModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public bool Recogido { get; set; }
        public string Nota { get; set; }
        public bool Estatus { get; set; }
    }
}
