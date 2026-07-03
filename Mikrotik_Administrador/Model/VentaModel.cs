using System.ComponentModel;

namespace Mikrotik_Administrador.Model
{
    public class VentaModel
    {
        public decimal Copias { get; set; }
        public bool Imprimir { get; set; }
        public decimal Recibido { get; set; }
        public int IdTicket { get; set; }
        public string Cliente { get; set; }
        public decimal Total { get; set; }
        public string Title { get; set; }
    }
}
