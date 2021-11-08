using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidades;
using System.IO;

namespace BeClever_GeneradorDePedidos
{
    public partial class FormPrincipal : Form
    {
        private string LeyendaTextSeleccionarArchivo = "Seleccione un archivo...";
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Archivos Excel(*.xls;*.xlsx;)|*.xls;*.xlsx;";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txt_rutaArchivo.Text = openFileDialog.FileName;
                    btn_Exportar.Enabled = true;
                }
                else
                {
                    txt_rutaArchivo.Text = LeyendaTextSeleccionarArchivo; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Exportar_Click(object sender, EventArgs e)
        {
            try
            {
                
                List<string> errores = new List<string>();
                if (File.Exists(txt_rutaArchivo.Text))
                {
                    List<ArchivoExcel> listaExcel = new List<ArchivoExcel>();
                    NegPedido pedido = new NegPedido();

                    listaExcel = pedido.ArchivoExcelDatos(txt_rutaArchivo.Text);
                    if (listaExcel.Count > 0)
                    {
                        errores = pedido.InsertarDatosDelExcel(listaExcel);
                        if (errores.Count > 0)
                        {
                            // mostrar interfaz de errores
                            FormErrores formErrores = new FormErrores();
                            formErrores.listErrores.Items.AddRange(errores.ToArray());
                            formErrores.Show();
                        }
                        else
                        {
                            MessageBox.Show("Pedido generado correctamente!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al guardar el pedido en Tango.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("ERROR, el archivo seleccionado NO existe", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogErrores(ex);
                throw;
            }
            
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                txt_rutaArchivo.Text = LeyendaTextSeleccionarArchivo;
                btn_buscar.Focus();
                btn_Exportar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LogErrores(Exception ex)
        {
            using (StreamWriter oLog = new System.IO.StreamWriter(Application.StartupPath + "\\Errores.log", true))
            {
                oLog.WriteLine(DateTime.Now.ToString("dd-MM-yyy HH:mm") + " - " + ex.Message);
            }
        }
    }
}
