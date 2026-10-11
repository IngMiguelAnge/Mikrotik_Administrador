using Microsoft.VisualBasic;
using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using Org.BouncyCastle.Math;
using System;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Mikrotik_Administrador.Items
{
    public partial class TiempoDefinido : Form
    {
        public int Id { get; set; }
        public int IdUsuarioM { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        MK mikrotik;
        public TiempoDefinido()
        {
            InitializeComponent();
        }

        private async void TiempoDefinido_Load(object sender, EventArgs e)
        {
            AppRepository obj = new AppRepository();
            var ListaPlanes = obj.GetPlanesbyName(string.Empty, null, false).Result;
            ListaPlanes.Insert(0, new ListPlanesModel { Id = 0, Nombre = "Selecciona un plan", Estatus = "Activo" });

            cbPlanNuevo.DisplayMember = "Nombre"; // Lo que el usuario VE
            cbPlanNuevo.ValueMember = "Id";      // El dato que procesas por DETRÁS
            cbPlanNuevo.DataSource = ListaPlanes.Where(x => x.Nombre != string.Empty).ToList();
            CBPlanOriginal.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBPlanOriginal.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBPlanOriginal.DataSource = ListaPlanes.Where(x => x.Nombre != string.Empty).ToList();

            if (Id != 0)
            {
                var Cambios = await obj.GetTiempoCambiobyId(Id);
                CBModo.SelectedItem = Cambios.Modo;
                dtpFechaInicio.Value = Cambios.FechaInicio;
                NUDDias.Value = Cambios.Dias;
                NUDHoras.Value = Cambios.Horas;
                lblFechaFin.Text = Cambios.FechaFin.ToString("dd/MM/yyyy");
                IdUsuarioM = Cambios.IdUsuarioM;
                cbPlanNuevo.SelectedValue = Cambios.IdPlan;
                CBPlanOriginal.SelectedValue = Cambios.IdPlanOriginal;
                if(CBMikrotiksNuevo.DataSource != null)
                    CBMikrotiksNuevo.SelectedValue = Cambios.IdMikrotikReceptor;
                if(CBMikrotiksOriginal.DataSource != null)
                    CBMikrotiksOriginal.SelectedValue = Cambios.IdMikrotikOriginal; 
            }
            else
            {
                CBModo.SelectedIndex = 0;
                cbPlanNuevo.SelectedIndex = 0;
                CBPlanOriginal.SelectedIndex = 0;
            }
        }

        public void CambiarFinal()
        {
            //if (primera || CBModo.SelectedIndex == 0)
            if (CBModo.SelectedIndex == 0)
                {
                lblFechaFin.Text = string.Empty;
                return;
            }
            lblFechaFin.Text = dtpFechaInicio.Value.AddDays((int)NUDDias.Value - 1).AddHours((int)NUDHoras.Value).ToString("dd/MM/yyyy");
        }
        private void dtpFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            CambiarFinal();
        }

        private void NUDDias_ValueChanged(object sender, EventArgs e)
        {
            CambiarFinal();
        }

        private void NUDHoras_ValueChanged(object sender, EventArgs e)
        {
            CambiarFinal();
        }

        private void CBModo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblFechaFin.Visible = true;
            if (CBModo.SelectedIndex == 0)
            {
                NUDDias.Enabled = false;
                NUDHoras.Enabled = false;
                dtpFechaInicio.Enabled = false;
                lblFechaFin.Text = string.Empty;
            }
            else
            { 
                dtpFechaInicio.Enabled = true;
                if(CBModo.SelectedIndex == 3)
                {
                    NUDDias.Enabled = false;
                    NUDHoras.Enabled = false;
                    lblFechaFin.Visible = false;
                }
                else
                {
                    NUDDias.Enabled = true;
                    NUDHoras.Enabled = true;
                    CambiarFinal();
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (CBModo.SelectedIndex == 0)
            {
                MessageBox.Show("Debe seleccionar de que forma aplicara el cambio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
            if(cbPlanNuevo.SelectedValue == null || (int)cbPlanNuevo.SelectedValue == 0)
            {
                MessageBox.Show("Debe seleccionar un plan a utilizar durante el periodo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (CBPlanOriginal.SelectedValue == null || (int)CBPlanOriginal.SelectedValue == 0)
            {
                MessageBox.Show("Debe seleccionar un plan a utilizar despues del periodo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(CBMikrotiksNuevo.SelectedValue == null || (int)CBMikrotiksNuevo.SelectedValue == 0)
            {
                MessageBox.Show("Debe seleccionar un mikrotik a utilizar durante el periodo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(CBMikrotiksOriginal.SelectedValue == null || (int)CBMikrotiksOriginal.SelectedValue == 0)
            {
                MessageBox.Show("Debe seleccionar un mikrotik a utilizar despues del periodo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TiempoCambioModel td = new TiempoCambioModel
            { 
            Id=Id,
            IdUsuarioM=IdUsuarioM,
            Modo=CBModo.SelectedItem.ToString(),
            FechaInicio=dtpFechaInicio.Value,
            Dias=(int)NUDDias.Value,
            Horas=(int)NUDHoras.Value,
            FechaFin=Convert.ToDateTime(lblFechaFin.Text),
            IdPlan=(int)cbPlanNuevo.SelectedValue,
            IdPlanOriginal=(int)CBPlanOriginal.SelectedValue,
            IdMikrotikReceptor=(int)CBMikrotiksNuevo.SelectedValue,
            IdMikrotikOriginal=(int)CBMikrotiksOriginal.SelectedValue,
            Nota = Id == 0 ? "Creado desde servicios cliente" : "Modificado por la página de cambios",
            Estatus = Convert.ToDateTime(lblFechaFin.Text) < DateTime.Now ? "Completado" :
            dtpFechaInicio.Value > DateTime.Now ? "Pendiente" :
            "Ejecutando"
            };
            FechaInicio = td.FechaInicio;
            FechaFin = td.FechaFin;
            AppRepository obj = new AppRepository();
            bool r = obj.SaveTiempoCambio(td).Result;
            if (r) {
                MessageBox.Show("Se guardo correctamente el cambio de plan.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al guardar el cambio de plan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        
        private void cbPlanNuevo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if((int)cbPlanNuevo.SelectedValue == 0)
            {
                CBMikrotiksNuevo.DataSource = null;
                return;
            }
            AppRepository obj = new AppRepository();
            var ListaMikrotiks = obj.GetMikrotiksByIdPlan((int)cbPlanNuevo.SelectedValue,true).Result;
            ListaMikrotiks.Insert(0, new ListMikrotikModel { Id = 0, Nombre = "Selecciona un mikrotik", Estatus = "Activo" });

            CBMikrotiksNuevo.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBMikrotiksNuevo.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBMikrotiksNuevo.DataSource = ListaMikrotiks.ToList();
            CBMikrotiksNuevo.SelectedIndex = 0;
        }

        private void CBPlanOriginal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((int)CBPlanOriginal.SelectedValue == 0)
            {
                CBMikrotiksOriginal.DataSource = null;
                return;
            }
            AppRepository obj = new AppRepository();
            var ListaMikrotiks = obj.GetMikrotiksByIdPlan((int)cbPlanNuevo.SelectedValue, true).Result;
            ListaMikrotiks.Insert(0, new ListMikrotikModel { Id = 0, Nombre = "Selecciona un mikrotik", Estatus = "Activo" });

            CBMikrotiksOriginal.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBMikrotiksOriginal.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBMikrotiksOriginal.DataSource = ListaMikrotiks.ToList();
            CBMikrotiksOriginal.SelectedIndex = 0;
        }
    }
}