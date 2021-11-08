
namespace BeClever_GeneradorDePedidos
{
    partial class FormErrores
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormErrores));
            this.listErrores = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listErrores
            // 
            this.listErrores.FormattingEnabled = true;
            this.listErrores.Location = new System.Drawing.Point(24, 28);
            this.listErrores.Name = "listErrores";
            this.listErrores.Size = new System.Drawing.Size(430, 186);
            this.listErrores.TabIndex = 0;
            // 
            // FormErrores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(481, 241);
            this.Controls.Add(this.listErrores);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormErrores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registros no guardados ";
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ListBox listErrores;
    }
}