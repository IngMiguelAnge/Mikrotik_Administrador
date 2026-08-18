using System;

namespace Mikrotik_Administrador.Model
{
    public class ListMensualidadesModel
    {
        public int Id { get; set; }
        public int DiaCorte { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaLimite { get; set; }
        public string Responsable { get; set; }
        public decimal Mensualidad { get; set; }
        public decimal Recibido { get; set; }
        public decimal Faltante { get; set; }
    }
}
