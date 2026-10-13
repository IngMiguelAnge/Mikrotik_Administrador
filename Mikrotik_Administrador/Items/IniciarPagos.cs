using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Mikrotik_Administrador.Items
{
    public partial class IniciarPagos : Form
    {
        public int IdMensualidad { get; set; }
        public int IdUsuarioM { get; set; }
        public int IdResponsable { get; set; }

        private decimal MensualidadFinal;
        public IniciarPagos()
        {
            InitializeComponent();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            AppRepository obj = new AppRepository();
            var existencia = await obj.GetExistMensualidadProxima(
           IdUsuarioM,
           IdMensualidad,
           dtpFechaInicio.Value,
           Convert.ToDateTime(lblFechaCorte.Text)
       );

            // 2. Si existen registros, mostramos el error y el return AHORA SÍ cancela el guardado
            if (existencia != null && existencia.Count > 0)
            {
                if (existencia.First().Id != IdMensualidad) // Aseguramos que no sea el mismo registro que estamos editando
                {
                    MessageBox.Show(
                    "No se puede ingresar esta fecha, ya hay un registro aproximado a la fecha establecida.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                    return; // Detiene la ejecución completa del método
                }               
            }
            // 3. Si la validación pasa, se crea el objeto
            MensualidadModel mensualidad = new MensualidadModel
            {
                Id = IdMensualidad,
                Pagado = true,//No importa si pones true internamente se calculara
                IdUsuarioM = IdUsuarioM,
                DiaCorte = (int)NUDCorte.Value,
                FechaInicio = dtpFechaInicio.Value,
                FechaLimite = Convert.ToDateTime(lblFechaCorte.Text),
                IdUsuario = IdResponsable,
                Mensualidad = MensualidadFinal,
            };

            // 4. Guardamos usando await en lugar de .Result (evita bloqueos de interfaz)
            int guardado = await obj.SaveMensualidad(mensualidad);

            if (guardado != 0)
            {
                MessageBox.Show("Mensualidad guardada");
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Error al guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void IniciarPagos_Load(object sender, EventArgs e)
        {    
            AppRepository obj = new AppRepository();
            if (IdMensualidad != 0)
            {
                var mensualidad = obj.GetMensualidadbyId(IdMensualidad).Result;
                if (mensualidad != null)
                {
                    dtpFechaInicio.Value = mensualidad.FechaInicio;
                    NUDCorte.Value = mensualidad.DiaCorte;
                }
                else
                {
                    MessageBox.Show("No se encontró la mensualidad especificada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            else
            {
                dtpFechaInicio.Value = DateTime.Now;
                DateTime fechaCorte = new DateTime(DateTime.Now.AddMonths(1).Year,
                    DateTime.Now.AddMonths(1).Month, 1);
                lblFechaCorte.Text = fechaCorte.ToString("dd/MM/yyyy");
            }
            var Servicio = obj.GetUsuariosMikrotiksById(IdUsuarioM).Result;
            var PlanOriginal = obj.GetPlanById(Servicio.IdPlanOriginal).Result;
            var PlanActual = obj.GetPlanById(Servicio.IdPlan).Result;
            lblMensualidadOriginal.Text = "Costo de la mensualidad original del servicio: $" + PlanOriginal.Precio;
            lblMensualidadActual.Text = "Costo de la mensualidad actual del servicio: $" + PlanActual.Precio;
        }
        public async void CalcularCostos()
        {
            var tramosCalculados = await CalcularTramosMensualidadSolocambiosAsync(IdUsuarioM, dtpFechaInicio.Value, Convert.ToDateTime(lblFechaCorte.Text));

            // Sumamos el costo total de los tramos que cayeron dentro de esta mensualidad
            decimal costoTotalcambios = tramosCalculados.Sum(t => t.Costo);

            lblCostoCambios.Text = "Costo de los cambios realizados al servicio: $" + costoTotalcambios.ToString();
            var tramossincambios = await ObtenerSoloTramosBaseAsync(IdUsuarioM, dtpFechaInicio.Value, Convert.ToDateTime(lblFechaCorte.Text));
            decimal CostoTotalSinCambios = tramossincambios.Sum(t => t.Costo);
            lblSincambios.Text = "Costo total sin cambios: $" + CostoTotalSinCambios.ToString();
            var tramostotales = await CalcularTramosMensualidadAsync(IdUsuarioM, dtpFechaInicio.Value, Convert.ToDateTime(lblFechaCorte.Text));
            MensualidadFinal = tramostotales.Sum(t => t.Costo) == 0 ? tramossincambios.Sum(t => t.Costo) :
            tramostotales.Sum(t => t.Costo);

            lblCostoFinal.Text = "Costo total por la mensualidad: $" + MensualidadFinal.ToString();

        }
        public async Task<List<ListDetallesMensualidadModel>> CalcularTramosMensualidadAsync(int idUsuarioM, DateTime desde, DateTime hasta)
        {
            AppRepository obj = new AppRepository();
            var Detalles = await obj.GetTiempoCambioforDetalles(idUsuarioM, desde, hasta);
            if (Detalles == null || !Detalles.Any())
            {
                return new List<ListDetallesMensualidadModel>();
            }

            List<ListDetallesMensualidadModel> ListDestalles = new List<ListDetallesMensualidadModel>();

            var usuarioMikrotik = await obj.GetUsuariosMikrotiksById(idUsuarioM);
            var planBasePeriodo = await obj.GetPlanById(usuarioMikrotik.IdPlanOriginal);
            string nombrePlanActual = planBasePeriodo != null ? planBasePeriodo.Nombre : "Plan Base";
            decimal precioPlanActual = planBasePeriodo != null ? planBasePeriodo.Precio : 0m;

            var cambioAnterior = Detalles
                .Where(x => x.FechaFin < desde && x.Estatus != "Cancelado")
                .OrderByDescending(x => x.FechaFin)
                .FirstOrDefault();

            if (cambioAnterior != null)
            {
                var planAnt = await obj.GetPlanById(cambioAnterior.IdPlanOriginal);
                if (planAnt != null)
                {
                    nombrePlanActual = planAnt.Nombre;
                    precioPlanActual = planAnt.Precio;
                }
            }

            var cambiosOrdenados = Detalles
                .Where(x => x.FechaInicio <= hasta && x.FechaFin >= desde && x.Estatus != "Cancelado")
                .OrderBy(x => x.FechaInicio)
                .ToList();

            DateTime cursor = desde;

            foreach (var cambio in cambiosOrdenados)
            {
                if (cursor < cambio.FechaInicio)
                {
                    DateTime finTramoBase = cambio.FechaInicio.AddDays(-1);
                    if (finTramoBase > hasta) finTramoBase = hasta;

                    if (cursor <= finTramoBase)
                    {
                        int diasBase = (int)(finTramoBase - cursor).TotalDays + 1;
                        decimal costoBase = RedondearMontoFinanciero(diasBase * (precioPlanActual / 30.0m));

                        ListDestalles.Add(new ListDetallesMensualidadModel
                        {
                            Id = 0, // O el Id de la mensualidad base correspondiente si lo manejas
                            FechaInicio = cursor,
                            FechaFin = finTramoBase,
                            Estatus = "Activo",
                            Plan = nombrePlanActual,
                            Costo = costoBase
                        });
                    }
                    cursor = cambio.FechaInicio;
                }

                DateTime inicioCambioEfectivo = cursor > cambio.FechaInicio ? cursor : cambio.FechaInicio;
                DateTime finCambioEfectivo = hasta < cambio.FechaFin ? hasta : cambio.FechaFin;

                if (inicioCambioEfectivo <= finCambioEfectivo)
                {
                    int diasCambio = (int)(finCambioEfectivo - inicioCambioEfectivo).TotalDays + 1;
                    var planNuevo = await obj.GetPlanById(cambio.IdPlan);
                    decimal precioPlanNuevo = planNuevo != null ? planNuevo.Precio : 0m;
                    decimal costoCambio = RedondearMontoFinanciero(diasCambio * (precioPlanNuevo / 30.0m));

                    ListDestalles.Add(new ListDetallesMensualidadModel
                    {
                        Id = cambio.Id, // ID del cambio / mensualidad a afectar
                        FechaInicio = inicioCambioEfectivo,
                        FechaFin = finCambioEfectivo,
                        Estatus = cambio.Estatus,
                        Plan = cambio.Plan,
                        Costo = costoCambio
                    });

                    cursor = finCambioEfectivo.AddDays(1);
                }

                var planOrigCambio = await obj.GetPlanById(cambio.IdPlanOriginal);
                if (planOrigCambio != null)
                {
                    nombrePlanActual = planOrigCambio.Nombre;
                    precioPlanActual = planOrigCambio.Precio;
                }
            }

            if (cursor <= hasta)
            {
                DateTime fechaFinTramoFinal = hasta;
                if (cursor <= fechaFinTramoFinal)
                {
                    int diasFinales = (int)(fechaFinTramoFinal - cursor).TotalDays + 1;
                    decimal costoFinal = RedondearMontoFinanciero(diasFinales * (precioPlanActual / 30.0m));

                    ListDestalles.Add(new ListDetallesMensualidadModel
                    {
                        Id = 0,
                        FechaInicio = cursor,
                        FechaFin = fechaFinTramoFinal,
                        Estatus = "Activo",
                        Plan = nombrePlanActual,
                        Costo = costoFinal
                    });
                }
            }

            return ListDestalles;
        }
        public async Task<List<ListDetallesMensualidadModel>> ObtenerSoloTramosBaseAsync(int idUsuarioM, DateTime desde, DateTime hasta)
        {
            AppRepository obj = new AppRepository();
            var Detalles = await obj.GetTiempoCambioforDetalles(idUsuarioM, desde, hasta);

            List<ListDetallesMensualidadModel> ListDestalles = new List<ListDetallesMensualidadModel>();

            var usuarioMikrotik = await obj.GetUsuariosMikrotiksById(idUsuarioM);
            var planBasePeriodo = await obj.GetPlanById(usuarioMikrotik.IdPlanOriginal);
            string nombrePlanActual = planBasePeriodo != null ? planBasePeriodo.Nombre : "Plan Base";
            decimal precioPlanActual = planBasePeriodo != null ? planBasePeriodo.Precio : 0m;

            var cambioAnterior = Detalles?
                .Where(x => x.FechaFin < desde && x.Estatus != "Cancelado")
                .OrderByDescending(x => x.FechaFin)
                .FirstOrDefault();

            if (cambioAnterior != null)
            {
                var planAnt = await obj.GetPlanById(cambioAnterior.IdPlanOriginal);
                if (planAnt != null)
                {
                    nombrePlanActual = planAnt.Nombre;
                    precioPlanActual = planAnt.Precio;
                }
            }

            var cambiosOrdenados = Detalles?
                .Where(x => x.FechaInicio <= hasta && x.FechaFin >= desde && x.Estatus != "Cancelado")
                .OrderBy(x => x.FechaInicio)
                .ToList() ?? new List<TiempoCambioModel>();

            DateTime cursor = desde;

            foreach (var cambio in cambiosOrdenados)
            {
                if (cursor < cambio.FechaInicio)
                {
                    DateTime finTramoBase = cambio.FechaInicio.AddDays(-1);
                    if (finTramoBase > hasta) finTramoBase = hasta;

                    if (cursor <= finTramoBase)
                    {
                        int diasBase = (int)(finTramoBase - cursor).TotalDays + 1;
                        decimal costoBase = 0m;

                        bool esMesCompletoExacto = (cursor.Day == finTramoBase.AddDays(1).Day) &&
                                                   (cursor.AddMonths(1).Date == finTramoBase.AddDays(1).Date);

                        if (esMesCompletoExacto || diasBase >= 28 && diasBase <= 31 && cursor.Month != finTramoBase.Month)
                        {
                            costoBase = precioPlanActual;
                        }
                        else
                        {
                            costoBase = RedondearMontoFinanciero(diasBase * (precioPlanActual / 30.0m));
                        }

                        ListDestalles.Add(new ListDetallesMensualidadModel
                        {
                            Id = 0,
                            FechaInicio = cursor,
                            FechaFin = finTramoBase,
                            Estatus = "Activo",
                            Plan = nombrePlanActual,
                            Costo = costoBase
                        });
                    }
                }

                cursor = cambio.FechaFin >= hasta ? hasta.AddDays(1) : cambio.FechaFin.AddDays(1);

                var planOrigCambio = await obj.GetPlanById(cambio.IdPlanOriginal);
                if (planOrigCambio != null)
                {
                    nombrePlanActual = planOrigCambio.Nombre;
                    precioPlanActual = planOrigCambio.Precio;
                }
            }

            if (cursor <= hasta)
            {
                DateTime fechaFinTramoFinal = hasta;
                if (cursor <= fechaFinTramoFinal)
                {
                    int diasFinales = (int)(fechaFinTramoFinal - cursor).TotalDays + 1;
                    decimal costoFinal = 0m;

                    // Validación robusta para el tramo final de un mes completo (ej. 14 de enero al 14 de febrero)
                    bool esMesCompletoFinal = (cursor.Date == desde.Date && fechaFinTramoFinal.Date == desde.AddMonths(1).Date);

                    // Si es un mes completo exacto, ignoramos si trajo 31 días y cobramos la tarifa plana
                    if (esMesCompletoFinal)
                    {
                        costoFinal = precioPlanActual; // Forzará los $300 cerrados
                    }
                    else
                    {
                        costoFinal = RedondearMontoFinanciero(diasFinales * (precioPlanActual / 30.0m));
                    }

                    ListDestalles.Add(new ListDetallesMensualidadModel
                    {
                        Id = 0,
                        FechaInicio = cursor,
                        FechaFin = fechaFinTramoFinal,
                        Estatus = "Activo",
                        Plan = nombrePlanActual,
                        Costo = costoFinal
                    });
                }
            }

            return ListDestalles;
        }
        public async Task<List<ListDetallesMensualidadModel>> CalcularTramosMensualidadSolocambiosAsync(int idUsuarioM, DateTime desde, DateTime hasta)
        {
            AppRepository obj = new AppRepository();
            var Detalles = await obj.GetTiempoCambioforDetalles(idUsuarioM, desde, hasta);
            if (Detalles == null || !Detalles.Any())
            {
                return new List<ListDetallesMensualidadModel>();
            }

            List<ListDetallesMensualidadModel> ListDestalles = new List<ListDetallesMensualidadModel>();

            // Ordenar los eventos de cambio cronológicamente dentro del rango relevante
            var cambiosOrdenados = Detalles
                .Where(x => x.FechaInicio <= hasta && x.FechaFin >= desde && x.Estatus != "Cancelado")
                .OrderBy(x => x.FechaInicio)
                .ToList();

            DateTime cursor = desde;

            foreach (var cambio in cambiosOrdenados)
            {
                // NOTA: Omitimos el bloque de "cursor < cambio.FechaInicio" 
                // para que no agregue el espacio libre del plan base.
                if (cursor < cambio.FechaInicio)
                {
                    cursor = cambio.FechaInicio; // Solo avanzamos el cursor sin guardar nada base
                }

                DateTime inicioCambioEfectivo = cursor > cambio.FechaInicio ? cursor : cambio.FechaInicio;
                DateTime finCambioEfectivo = hasta < cambio.FechaFin ? hasta : cambio.FechaFin;

                if (inicioCambioEfectivo <= finCambioEfectivo)
                {
                    int diasCambio = (int)(finCambioEfectivo - inicioCambioEfectivo).TotalDays + 1;
                    var planNuevo = await obj.GetPlanById(cambio.IdPlan);
                    decimal precioPlanNuevo = planNuevo != null ? planNuevo.Precio : 0m;
                    decimal costoCambio = RedondearMontoFinanciero(diasCambio * (precioPlanNuevo / 30.0m));

                    // ESTO SÍ SE AGREGA: Únicamente los cambios de plan reales
                    ListDestalles.Add(new ListDetallesMensualidadModel
                    {
                        Id = cambio.Id, // ID del cambio
                        FechaInicio = inicioCambioEfectivo,
                        FechaFin = finCambioEfectivo,
                        Estatus = cambio.Estatus,
                        Plan = cambio.Plan,
                        Costo = costoCambio
                    });

                    cursor = finCambioEfectivo.AddDays(1);
                }
            }

            // NOTA: Omitimos también el bloque final ("if (cursor <= hasta)") 
            // para que no agregue el cierre del plan base.

            return ListDestalles;
        }
        private decimal RedondearMontoFinanciero(decimal monto)
        {
            decimal parteEntera = Math.Floor(monto);
            decimal parteDecimal = monto - parteEntera;

            if (parteDecimal > 0.00m && parteDecimal < 0.30m)
                return parteEntera;
            else if (parteDecimal >= 0.30m && parteDecimal <= 0.50m)
                return parteEntera + 0.50m;
            else if (parteDecimal > 0.50m)
                return parteEntera + 1.00m;

            return parteEntera;
        }
        private void NUDCorte_ValueChanged(object sender, EventArgs e)
        {
            CambiarFecha();
        }
        private void dtpFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            CambiarFecha();
        }
        public void CambiarFecha()
        {
            int diaDeseado = (int)NUDCorte.Value;
            DateTime fechaInicio = dtpFechaInicio.Value;

            // 1. Determinamos el año y mes base del inicio
            int anioCorte = fechaInicio.Year;
            int mesCorte = fechaInicio.Month;

            // 2. Evaluamos si el corte cae en el mismo mes o pasa al siguiente
            // Si el día de corte es menor o igual al día de inicio, corresponde al mes siguiente.
            if (diaDeseado <= fechaInicio.Day)
            {
                // Sumamos un mes respetando el salto de año si es diciembre
                DateTime siguienteMes = fechaInicio.AddMonths(1);
                anioCorte = siguienteMes.Year;
                mesCorte = siguienteMes.Month;
            }

            // 3. Obtenemos el máximo de días que tiene el mes calculado (ej. 28 para febrero, 30, 31, etc.)
            int diasEnElMes = DateTime.DaysInMonth(anioCorte, mesCorte);

            // 4. Si el día deseado es mayor que los días del mes (ej. elegiste 30 pero febrero tiene 28), 
            // se ajusta automáticamente al último día disponible del mes.
            int diaFinal = Math.Min(diaDeseado, diasEnElMes);

            // 5. Creamos la fecha final de corte y la asignamos al Label
            DateTime fechaCorte = new DateTime(anioCorte, mesCorte, diaFinal);
            lblFechaCorte.Text = fechaCorte.ToString("dd/MM/yyyy");
            CalcularCostos();
        }

        private void NUDCorte_KeyUp(object sender, KeyEventArgs e)
        {
            CambiarFecha();
        }
    }
}
