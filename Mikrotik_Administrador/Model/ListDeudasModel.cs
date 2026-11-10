namespace Mikrotik_Administrador.Model
{
    public class ListDeudasModel
    {
        public int Id { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Faltante { get; set; }
        public int Year { get; set; }
        public string Mes { get; set; }
        public decimal Recibido { get; set; }
        public string Estatus { get; set; }
    }
}
