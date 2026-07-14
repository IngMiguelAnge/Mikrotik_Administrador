using System;

namespace Mikrotik_Administrador.Model
{
    public class ListTiempoCambioModel
    {
        public int Id { get; set; }
        public int Dias { get; set; }
        public int Horas { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string Estatus { get; set; }
        public string Modo { get; set; }
        public int IdUsuarioM { get; set; }
        public string Nota { get; set; }
        public int IdPlan { get; set; }
        public string PlanNuevo { get; set; }
        public string Usuario { get; set; }
    }
}
