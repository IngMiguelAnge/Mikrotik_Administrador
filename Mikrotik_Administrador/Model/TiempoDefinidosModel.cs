using System;

namespace Mikrotik_Administrador.Model
{
    public class TiempoDefinidosModel
    {
        public int Id { get; set; }
        public int Dias { get; set; }
        public int Horas { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Modo { get; set; }
        public int IdUsuarioM { get; set; }
        public string Estatus { get; set; }
    }
}
