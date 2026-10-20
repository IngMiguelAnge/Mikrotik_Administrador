using System;

namespace Mikrotik_Administrador.Model
{
    public class ListProrrogasModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Estatus { get; set; }
    }
}
