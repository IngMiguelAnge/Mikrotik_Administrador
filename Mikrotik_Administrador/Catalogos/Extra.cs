using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using Newtonsoft.Json.Converters;
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

        private async void Extra_Load(object sender, EventArgs e)
        {
            AppRepository obj = new AppRepository();
            var listUsuarios = await obj.GetUsuarios(string.Empty,string.Empty);

            // Insertamos un objeto "fantasma" al inicio para el placeholder
            listUsuarios.Insert(0, new ListUsuariosModel { Id = 0, Nombre = "Selecciona un usuario", Estatus = "Activo" });

            // Configuramos el ComboBox
            CBRecibe.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBRecibe.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBRecibe.DataSource = listUsuarios.Where(x => x.Estatus == "Activo").ToList();
            CBRecibe.SelectedIndex = 0;

            CBRepara.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBRepara.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBRepara.DataSource = listUsuarios.Where(x => x.Estatus == "Activo").ToList();
            CBRepara.SelectedIndex = 0;

            CBEntrega.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBEntrega.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBEntrega.DataSource = listUsuarios.Where(x => x.Estatus == "Activo").ToList();
            CBEntrega.SelectedIndex = 0;
            if (Id != 0)
            {   
                var Extra = obj.GetExtrabyId(Id).Result;
                txtNombre.Text = Extra.Nombre;
                NUDCosto.Value = Extra.Costo;
                dtpFechaRecibido.Value = Extra.Fecha_Recibido;
                if (Extra.Fecha_Entregado != null)
                    cbSinEntregar.Checked = false;
                else
                    cbSinEntregar.Checked = true;
                dtpFechaEntrega.Value = Extra.Fecha_Entregado ?? Extra.Fecha_Recibido;
                CBRecibe.SelectedValue = Extra.IdUsuarioRecibe;
                if (Extra.IdUsuarioRepara != null)
                    CBRepara.SelectedValue = Extra.IdUsuarioRepara;
                if (Extra.IdUsuarioEntrega != null)
                    CBEntrega.SelectedValue = Extra.IdUsuarioEntrega;
            }          
        }

        private void cbSinEntregar_CheckedChanged(object sender, EventArgs e)
        {
            if(cbSinEntregar.Checked)
            {
                dtpFechaEntrega.Enabled = false;
                CBEntrega.SelectedValue = 0;
                CBEntrega.Enabled = false;
            }               
            else
            {
                dtpFechaEntrega.Enabled = true;
                CBEntrega.Enabled = true;
            }        
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == string.Empty || (int)CBRecibe.SelectedValue==0)
            {
                MessageBox.Show("Informacón incompleta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cbSinEntregar.Checked == false && (int)CBEntrega.SelectedValue == 0)
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
                IdUsuarioM = IdUsuarioM,
                IdUsuarioRecibe = (int)CBRecibe.SelectedValue,
                IdUsuarioRepara = (int)CBRepara.SelectedValue == 0 ? (int?)null : (int)CBRepara.SelectedValue,
                IdUsuarioEntrega = (int)CBEntrega.SelectedValue == 0 ? (int?)null : (int)CBEntrega.SelectedValue,
                Nota = txtNombre.Text.ToString().Trim(),
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
