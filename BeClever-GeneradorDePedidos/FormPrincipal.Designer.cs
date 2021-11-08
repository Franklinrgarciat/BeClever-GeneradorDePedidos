
namespace BeClever_GeneradorDePedidos
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.txt_rutaArchivo = new System.Windows.Forms.TextBox();
            this.btn_Exportar = new System.Windows.Forms.Button();
            this.btn_buscar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_rutaArchivo
            // 
            this.txt_rutaArchivo.Enabled = false;
            this.txt_rutaArchivo.Location = new System.Drawing.Point(22, 53);
            this.txt_rutaArchivo.Name = "txt_rutaArchivo";
            this.txt_rutaArchivo.Size = new System.Drawing.Size(276, 20);
            this.txt_rutaArchivo.TabIndex = 1;
            // 
            // btn_Exportar
            // 
            this.btn_Exportar.BackColor = System.Drawing.Color.White;
            this.btn_Exportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Exportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Exportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Exportar.Image = global::BeClever_GeneradorDePedidos.Properties.Resources.Aceptar;
            this.btn_Exportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Exportar.Location = new System.Drawing.Point(167, 154);
            this.btn_Exportar.Name = "btn_Exportar";
            this.btn_Exportar.Size = new System.Drawing.Size(153, 34);
            this.btn_Exportar.TabIndex = 2;
            this.btn_Exportar.Text = "Exportar a Tango";
            this.btn_Exportar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Exportar.UseVisualStyleBackColor = false;
            this.btn_Exportar.Click += new System.EventHandler(this.btn_Exportar_Click);
            // 
            // btn_buscar
            // 
            this.btn_buscar.BackColor = System.Drawing.Color.White;
            this.btn_buscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_buscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_buscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_buscar.Image = global::BeClever_GeneradorDePedidos.Properties.Resources.Excel;
            this.btn_buscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_buscar.Location = new System.Drawing.Point(330, 46);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(84, 34);
            this.btn_buscar.TabIndex = 0;
            this.btn_buscar.Text = "Buscar";
            this.btn_buscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_buscar.UseVisualStyleBackColor = false;
            this.btn_buscar.Click += new System.EventHandler(this.btn_buscar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_rutaArchivo);
            this.groupBox1.Controls.Add(this.btn_buscar);
            this.groupBox1.Location = new System.Drawing.Point(22, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(451, 122);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cargue el archivo excel";
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(493, 212);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Exportar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: Generar Pedido -Tango ::.";
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_buscar;
        private System.Windows.Forms.TextBox txt_rutaArchivo;
        private System.Windows.Forms.Button btn_Exportar;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

