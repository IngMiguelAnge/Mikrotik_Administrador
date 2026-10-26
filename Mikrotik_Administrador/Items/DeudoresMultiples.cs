using Mikrotik_Administrador.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Items
{
    public partial class DeudoresMultiples : Form
    {
        public DeudoresMultiples()
        {
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

        }
        private async Task CargarDatosExcelDeudoresAsync(string rutaArchivo)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            btnCargar.Enabled = false;
            //BtnBuscar.Enabled = false;
            //btnDescargar.Enabled = false;

            try
            {
                AppRepository obj = new AppRepository();

                using (var workbook = new ClosedXML.Excel.XLWorkbook(rutaArchivo))
                {
                    // Tomamos la primera hoja del libro de Excel
                    var ws = workbook.Worksheet(1);
                    bool primeraFila = true;

                    // Lista de los nombres de los meses en el orden exacto de tus columnas
                    string[] mesesAnio = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                                   "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

                    foreach (var row in ws.RowsUsed())
                    {
                        if (primeraFila)
                        {
                            primeraFila = false;
                            continue;
                        }

                        // 1. Extraer los datos principales de la fila
                        string nombreCliente = row.Cell(1).GetString().Trim(); // Columna A: Cliente
                        string coordenadas = row.Cell(2).GetString().Trim();   // Columna B: Coordenadas

                        // Validar si la celda del año tiene valor
                        int anio = 0;
                        if (!row.Cell(3).IsEmpty() && int.TryParse(row.Cell(3).GetString(), out int parsedAnio))
                        {
                            anio = parsedAnio; // Columna C: Año
                        }

                        if (string.IsNullOrEmpty(nombreCliente)) continue;

                        // 2. Iterar sobre las columnas de los meses (Enero a Diciembre empiezan a partir de la columna D / índice 4)
                        for (int i = 0; i < mesesAnio.Length; i++)
                        {
                            int numeroColumna = 4 + i; // Columna D es 4, E es 5, etc.
                            var celdaMes = row.Cell(numeroColumna);

                            // Verificar si la celda tiene información (no está vacía)
                            if (!celdaMes.IsEmpty() && decimal.TryParse(celdaMes.GetString().Trim(), out decimal cantidad))
                            {
                                string nombreMes = mesesAnio[i];

                                // AQUÍ PUEDES PROCESAR O GUARDAR CADA REGISTRO ENCONTRADO:
                                // Ejemplo de lo que detectará para Juan Carlos Gaspar Candelario:
                                // Cliente: JUAN CARLOS GASPAR CANDELARIO
                                // Coordenadas: 18.594643,-97.669097
                                // Año: 2025
                                // Mes: Enero
                                // Cantidad: 250.00

                                // TODO: Inserta aquí tu lógica para guardar en Base de Datos o agregar a una lista temporal.
                            }
                        }
                    }
                }

                MessageBox.Show("Excel de deudores cargado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer el archivo Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 100;
                //BtnBuscar.Enabled = true;
                //btnDescargar.Enabled = true;
                btnCargar.Enabled = true;
            }
        }
    }
}
