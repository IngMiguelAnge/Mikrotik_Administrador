using System;

namespace Mikrotik_Administrador.Model
{
    public class ProrrogaModel
    {
        public int Id { get; set; }
        public int IdUsuarioM { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estatus { get; set; }
    }
}
