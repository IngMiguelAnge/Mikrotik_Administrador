using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikrotik_Administrador.Model
{
    public class ListCostosExtrasModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal TotalPendiente { get; set; }
        public string Tipo { get; set; }
    }
}
