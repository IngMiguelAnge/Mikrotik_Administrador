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
        decimal CostoTotal = 0;
        bool primerIngreso = true;
        bool primerIngreso2 = true;
        public Extra()
        {
            InitializeComponent();
        }

        private async void Extra_Load(object sender, EventArgs e)
        {          
            lblCostoTotal.Text = "Costo total: $" + CostoTotal.ToString();
            AppRepository obj = new AppRepository();
            var listUsuarios = await obj.GetUsuarios(string.Empty,string.Empty);

            // Insertamos un objeto "fantasma" al inicio para el placeholder
            listUsuarios.Insert(0, new ListUsuariosModel { Id = 0, Nombre = "Selecciona un usuario", Estatus = "Activo" });

            // Configuramos el ComboBox
            CBRecibe.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBRecibe.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBRecibe.DataSource = listUsuarios.Where(x => x.Estatus == "Activo").ToList();

            CBRepara.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBRepara.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBRepara.DataSource = listUsuarios.Where(x => x.Estatus == "Activo").ToList();

            CBEntrega.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBEntrega.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBEntrega.DataSource = listUsuarios.Where(x => x.Estatus == "Activo").ToList();

            CBRecoge.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBRecoge.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBRecoge.DataSource = listUsuarios.Where(x => x.Estatus == "Activo").ToList();
          
            if (Id != 0)
            {   
                var Extra = obj.GetExtrabyId(Id).Result;
                txtNombre.Text = Extra.Nombre;
                NUDCosto.Value = Extra.Costo;
                NudDescuento.Value = Extra.Descuento;
                CostoTotal = Extra.Total;
                CBRecibe.SelectedValue = Extra.IdUsuarioRecibe;
                dtpFechaRecibido.Value = Extra.Fecha_Recibido ??
                    DateTime.Now;
                CBRepara.SelectedValue = Extra.IdUsuarioRepara;
                dtpFechaReparacion.Value = Extra.Fecha_Reparacion ??
                    DateTime.Now;
                CBRepara.SelectedValue = Extra.IdUsuarioEntrega;
                dtpFechaEntrega.Value = Extra.Fecha_Entregado ??
                    DateTime.Now;
                CBRecoge.SelectedValue = Extra.IdUsuarioRecoge;
                dtpFechaEntrega.Value = Extra.Fecha_Recogido ??
                    DateTime.Now;
                txtNota.Text = Extra.Nota;
                CBTipo.SelectedValue = Extra.Tipo;
            }
            else
            {
                CBRecibe.SelectedIndex = 0;
                CBRepara.SelectedIndex = 0;
                CBEntrega.SelectedIndex = 0;
                CBRecoge.SelectedIndex = 0;
                CBTipo.SelectedIndex = 0;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == string.Empty || (string)CBTipo.SelectedItem == "Seleccione")
            {
                MessageBox.Show("Informacón incompleta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((string)CBTipo.SelectedItem == "Prestamo" && (int)CBEntrega.SelectedValue == 0)
            {
                MessageBox.Show("El prestamo requiere de un encargado de entrega", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((string)CBTipo.SelectedItem == "Venta" && (int)CBEntrega.SelectedValue == 0)
            {
                MessageBox.Show("La venta requiere de un encargado de entrega", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((string)CBTipo.SelectedItem == "Servicio" && (int)CBRecibe.SelectedValue == 0)
            {
                MessageBox.Show("La servicio requiere de un encargado de recibimiento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AppRepository obj = new AppRepository();
            if(Id != 0)
            {
                var Historial = obj.GetHistorialPagosExtras(Id, string.Empty, 0, 0).Result;
                decimal suma = Historial?.Where(x => x.Estatus == "Activo").Sum(x => x.Cantidad) ?? 0m;
                if(suma > CostoTotal)
                {
                    MessageBox.Show("Este articulo ya tiene historicos guardados y dan una cantidad de $"+ suma.ToString() +
                        ", no se permite modificar con un costo menor al historico activo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
         
            
            ExtrasModel extra = new ExtrasModel()
            {
                Id = Id,
                Tipo = (string)CBTipo.SelectedItem,
                Nombre = txtNombre.Text.Trim(),
                Costo = NUDCosto.Value,
                Descuento = NudDescuento.Value,
                Total = CostoTotal,
                Fecha_Recibido = CBRecibe.SelectedIndex == 0? (DateTime?)null
                : dtpFechaRecibido.Value,
                Fecha_Reparacion = CBRepara.SelectedIndex == 0 ? (DateTime?)null
                : dtpFechaReparacion.Value,
                Fecha_Entregado = CBEntrega.SelectedIndex == 0 ? (DateTime?)null
                : dtpFechaEntrega.Value,
                Fecha_Recogido = CBRecoge.SelectedIndex == 0 ? (DateTime?)null
                : dtpFechaRecogida.Value,
                IdUsuarioM = IdUsuarioM,
                IdUsuarioRecibe = (int)CBRecibe.SelectedValue == 0 ? (int?)null : (int)CBRecibe.SelectedValue,
                IdUsuarioRepara = (int)CBRepara.SelectedValue == 0 ? (int?)null : (int)CBRepara.SelectedValue,
                IdUsuarioEntrega = (int)CBEntrega.SelectedValue == 0 ? (int?)null : (int)CBEntrega.SelectedValue,
                IdUsuarioRecoge = (int)CBRecoge.SelectedValue == 0 ? (int?)null : (int)CBRecoge.SelectedValue,
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

        private void CBTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CBTipo.SelectedItem)
            {
                case "Prestamo":
                    NudDescuento.Enabled = true;
                    NudDescuento.Value = 30;
                    CBRecibe.Enabled = false;
                    CBRecibe.SelectedIndex = 0;
                    CBRepara.Enabled = false; 
                    CBRepara.SelectedIndex = 0;
                    CBEntrega.Enabled = true;
                    CBEntrega.SelectedIndex = 0;
                    CBRecoge.Enabled = true;
                    CBRecoge.SelectedIndex = 0;
                    break;
                case "Venta":
                    NudDescuento.Enabled = false;
                    NudDescuento.Value = 0;
                    CBRecibe.Enabled = false;
                    CBRecibe.SelectedIndex = 0;
                    CBRepara.Enabled = false;
                    CBRepara.SelectedIndex = 0;
                    CBEntrega.Enabled = true;
                    CBEntrega.SelectedIndex = 0;
                    CBRecoge.Enabled = false;
                    CBRecoge.SelectedIndex = 0;
                    break;
                case "Servicio":
                    NudDescuento.Enabled = false;
                    NudDescuento.Value = 0;
                    CBRecibe.Enabled = true;
                    CBRecibe.SelectedIndex = 0;
                    CBRepara.Enabled = true;
                    CBRepara.SelectedIndex = 0;
                    CBEntrega.Enabled = true;
                    CBEntrega.SelectedIndex = 0;
                    CBRecoge.Enabled = false;
                    CBRecoge.SelectedIndex = 0;
                    break;
                default:
                    NudDescuento.Enabled = false;
                    NudDescuento.Value = 0;
                    CBRecibe.Enabled = false;
                    CBRecibe.SelectedIndex = 0;
                    CBRepara.Enabled = false;
                    CBRepara.SelectedIndex = 0;
                    CBEntrega.Enabled = false;
                    CBEntrega.SelectedIndex = 0;
                    CBRecoge.Enabled = false;
                    CBRecoge.SelectedIndex = 0;
                    break;
            }
        }
        public void Calcular()
        {
            decimal costo = NUDCosto.Value;
            decimal descuento = NudDescuento.Value;

            decimal montoDescuento = costo * (descuento / 100m);
            CostoTotal = costo - montoDescuento;

            lblCostoTotal.Text = $"Costo total: ${CostoTotal:N2}";
        }

        private void NUDCosto_KeyUp(object sender, KeyEventArgs e)
        {
            if (!primerIngreso && sender is NumericUpDown nud && nud.Controls[1] is TextBox tb)
            {
                int cursorPos = tb.SelectionStart; // Guardamos dónde estabas
                Calcular();
                tb.SelectionStart = Math.Min(cursorPos, tb.Text.Length); // Mantenemos tu posición
            }
        }

        private void NudDescuento_KeyUp(object sender, KeyEventArgs e)
        {
            if (!primerIngreso2 && sender is NumericUpDown nud && nud.Controls[1] is TextBox tb)
            {
                int cursorPos = tb.SelectionStart; // Guardamos dónde estabas
                Calcular();
                tb.SelectionStart = Math.Min(cursorPos, tb.Text.Length); // Mantenemos tu posición
            }
        }

        private void NUDCosto_ValueChanged(object sender, EventArgs e)
        {
            if (!primerIngreso)
            {
                Calcular();
            }
        }

        private void NudDescuento_ValueChanged(object sender, EventArgs e)
        {
            if (!primerIngreso2)
            {
                Calcular();
            }
        }

       
        private void NUDCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            // Validación de punto único y dígitos
            if (e.KeyChar == '.')
            {
                if (NUDCosto.Text.Contains(".")) e.Handled = true;
                return;
            }

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void NudDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            if (e.KeyChar == '.')
            {
                if (NudDescuento.Text.Contains(".")) e.Handled = true;
                return;
            }

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void NUDCosto_Enter(object sender, EventArgs e)
        {
            if (primerIngreso)
            {
                NUDCosto.Text = ""; // O NUDCosto.Value = 0; pero limpio para escritura
                primerIngreso = false;
            }
        }

        private void NudDescuento_Enter(object sender, EventArgs e)
        {
            if (primerIngreso2)
            {
                NudDescuento.Text = "";
                primerIngreso2 = false;
            }
        }

        private void CBRecibe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBRecibe.SelectedIndex == 0)
                dtpFechaRecibido.Enabled = false;
            else
                dtpFechaRecibido.Enabled = true;
        }

        private void CBRepara_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBRepara.SelectedIndex == 0)
                dtpFechaReparacion.Enabled = false;
            else
                dtpFechaReparacion.Enabled = true;
        }

        private void CBEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBEntrega.SelectedIndex == 0)
                dtpFechaEntrega.Enabled = false;
            else
                dtpFechaEntrega.Enabled = true;
        }

        private void CBRecoge_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBRecoge.SelectedIndex == 0)
                dtpFechaRecogida.Enabled = false;
            else
                dtpFechaRecogida.Enabled = true;
        }
    }
}
