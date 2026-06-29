using System;

namespace Mikrotik_Administrador.Model
{
    public class MensualidadModel
    {
        public int Id { get; set; }
        public DateTime FechaLimite {  get; set; }
        public bool Pagado { get; set; }
        public int IdPlan { get; set; }
        public string PlanCerrado { get; set; }
        public decimal CantidadCerrada { get; set; }
        public int IdUsuarioM { get; set; }
    }
}
