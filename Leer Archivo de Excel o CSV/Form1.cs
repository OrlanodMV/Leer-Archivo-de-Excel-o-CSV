using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace U5_Excel
{

    public partial class Form1 : Form
    {
        private string RUTA_CARPETA;

        public Form1()
        {
            InitializeComponent();

            RUTA_CARPETA = Path.Combine(Application.StartupPath, "Excel");

            ConfigurarControles();

            this.Load += (s, e) => CargarArchivosExcel();
        }

        private void ConfigurarControles()
        {
            lstLista.View = View.Details;
            lstLista.Columns.Add("Archivos Excel", 250);
            lstLista.FullRowSelect = true;
            lstLista.MultiSelect = false;
            lstLista.GridLines = true;
            lstLista.HeaderStyle = ColumnHeaderStyle.Clickable;
            lstLista.DoubleClick += (s, e) => btnLeerArchivo.PerformClick();
        }

        private void CargarArchivosExcel()
        {
            try
            {
                lstLista.Items.Clear();

                if (!Directory.Exists(RUTA_CARPETA))
                {
                    Directory.CreateDirectory(RUTA_CARPETA);
                    MessageBox.Show($"Se creó la carpeta: {RUTA_CARPETA}\nPor favor coloca tus archivos Excel aquí.",
                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var archivos = Directory.GetFiles(RUTA_CARPETA, "*.xlsx", SearchOption.TopDirectoryOnly);

                if (archivos.Length == 0)
                {
                    MessageBox.Show($"No se encontraron archivos Excel (.xlsx) en:\n{RUTA_CARPETA}",
                                  "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Array.Sort(archivos);
                foreach (var archivo in archivos)
                {
                    var item = new ListViewItem(Path.GetFileName(archivo));
                    item.Tag = archivo;
                    lstLista.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error al cargar archivos:\n{ex.Message}");
            }
        }

        private void CargarDatosExcel(string rutaArchivo)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                listViewLineas.BeginUpdate();
                listViewLineas.Items.Clear();
                listViewLineas.Columns.Clear();

                if (!File.Exists(rutaArchivo))
                {
                    MostrarError("El archivo seleccionado ya no existe.");
                    CargarArchivosExcel();
                    return;
                }

                using (var workbook = new XLWorkbook(rutaArchivo))
                {
                    var worksheet = workbook.Worksheet(1) ?? workbook.Worksheets.First();
                    var range = worksheet.RangeUsed();

                    if (range == null)
                    {
                        MessageBox.Show("El archivo no contiene datos en la primera hoja",
                                      "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var primeraFila = range.FirstRow();
                    foreach (var celda in primeraFila.Cells())
                    {
                        string encabezado = celda.GetValue<string>().Trim();
                        if (string.IsNullOrEmpty(encabezado))
                            encabezado = $"Columna {celda.Address.ColumnNumber}";

                        listViewLineas.Columns.Add(encabezado);
                    }

                    foreach (var fila in range.RowsUsed().Skip(1))
                    {
                        var item = new ListViewItem(fila.Cell(1).GetValue<string>());

                        for (int i = 2; i <= range.ColumnCount(); i++)
                        {
                            var celda = fila.Cell(i);
                            item.SubItems.Add(celda.IsEmpty() ? string.Empty : celda.GetValue<string>());
                        }

                        listViewLineas.Items.Add(item);
                    }

                    foreach (ColumnHeader col in listViewLineas.Columns)
                    {
                        col.Width = -2;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error al leer el archivo:\n{ex.Message}\n\nAsegúrate que:\n1. El archivo no esté abierto en Excel\n2. El formato del archivo sea correcto");
            }
            finally
            {
                listViewLineas.EndUpdate();
                Cursor.Current = Cursors.Default;
            }
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnLeerArchivo_Click(object sender, EventArgs e)
        {
            if (lstLista.SelectedItems.Count == 0)
            {
                MessageBox.Show("Por favor selecciona un archivo de la lista",
                              "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string rutaArchivo = lstLista.SelectedItems[0].Tag.ToString();
            CargarDatosExcel(rutaArchivo);
        }
    }
}
