using System;
namespace Mikrotik_Administrador.Model
{
    public class ExtrasModel
    {
        public int Id { get; set; }
        public string Nombre {  get; set; }
        public decimal Costo {  get; set; }
        public DateTime Fecha_Recibido { get; set; }
        public DateTime? Fecha_Entregado {  get; set; }
        public string Estatus { get; set; }
        public int IdUsuarioM { get; set; }
    }
}
