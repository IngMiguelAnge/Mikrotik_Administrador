namespace Mikrotik_Administrador.Model
{
    public class DeudaModel
    {
        public int Id { get; set; }
        public decimal Cantidad { get; set; }
        public int Año { get; set; }
        public string Mes { get; set; }
        public bool Estatus { get; set; }
        public int IdDeudor { get; set; }
    }
}
