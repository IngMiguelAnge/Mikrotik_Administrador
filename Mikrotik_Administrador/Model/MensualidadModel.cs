using System;

namespace Mikrotik_Administrador.Model
{
    public class MensualidadModel
    {
        public int Id { get; set; }
        public bool Pagado { get; set; }
        public int IdUsuarioM { get; set; }
        public int DiaCorte { get; set; }
        public DateTime FechaInicio {  get; set; }
        public DateTime FechaLimite { get; set; }
        public int IdUsuario { get; set; }

    }
}
