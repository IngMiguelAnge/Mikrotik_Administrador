using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Catalogos
{
    public partial class Extra : Form
    {
        public int Id { get; set; }
        public int IdUsuarioM { get; set; }
        public Extra()
        {
            InitializeComponent();
        }

        private void Extra_Load(object sender, EventArgs e)
        {
            if (Id != 0)
            {
                AppRepository obj = new AppRepository();
                var Extra = obj.GetExtrabyId(Id).Result;
                txtNombre.Text = Extra.Nombre;
                NUDCosto.Value = Extra.Costo;
                dtpFechaRecibido.Value = Extra.Fecha_Recibido;
                if (Extra.Fecha_Entregado != null)
                    cbSinEntregar.Checked = false;
                else
                    cbSinEntregar.Checked = true;
                dtpFechaEntrega.Value = Extra.Fecha_Entregado ?? Extra.Fecha_Recibido;
            }
        }

        private void cbSinEntregar_CheckedChanged(object sender, EventArgs e)
        {
            if(cbSinEntregar.Checked)
                dtpFechaEntrega.Enabled =false;
            else
                dtpFechaEntrega.Enabled =true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Informacón incompleta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AppRepository obj = new AppRepository();
            if(Id != 0)
            {
                var Historial = obj.GetHistorialPagosExtras(Id, string.Empty, 0, 0).Result;
                decimal suma = Historial?.Where(x => x.Estatus == "Activo").Sum(x => x.Cantidad) ?? 0m;
                if(suma > NUDCosto.Value)
                {
                    MessageBox.Show("Este articulo ya tiene historicos guardados y dan una cantidad de $"+suma.ToString() +
                        ", no se permite modificar con un costo menor al historico activo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
         
            
            ExtrasModel extra = new ExtrasModel()
            {
                Id = Id,
                Nombre = txtNombre.Text.Trim(),
                Costo = NUDCosto.Value,
                Fecha_Recibido = dtpFechaRecibido.Value,
                Fecha_Entregado = cbSinEntregar.Checked ? (DateTime?)null : dtpFechaEntrega.Value,
                IdUsuarioM = IdUsuarioM                
            };
   
            var r = obj.SaveExtras(extra).Result;
            if(r> 0)
            {
                MessageBox.Show("Guardado correctamante", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
