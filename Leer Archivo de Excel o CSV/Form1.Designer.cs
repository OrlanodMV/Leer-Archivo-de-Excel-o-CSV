namespace U5_Excel
{
    partial class Form1
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
            this.lstLista = new System.Windows.Forms.ListView();
            this.btnLeerArchivo = new System.Windows.Forms.Button();
            this.listViewLineas = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lstLista
            // 
            this.lstLista.HideSelection = false;
            this.lstLista.Location = new System.Drawing.Point(43, 44);
            this.lstLista.Name = "lstLista";
            this.lstLista.Size = new System.Drawing.Size(290, 232);
            this.lstLista.TabIndex = 0;
            this.lstLista.UseCompatibleStateImageBehavior = false;
            // 
            // btnLeerArchivo
            // 
            this.btnLeerArchivo.Location = new System.Drawing.Point(135, 292);
            this.btnLeerArchivo.Name = "btnLeerArchivo";
            this.btnLeerArchivo.Size = new System.Drawing.Size(99, 59);
            this.btnLeerArchivo.TabIndex = 2;
            this.btnLeerArchivo.Text = "Leer Archivo";
            this.btnLeerArchivo.UseVisualStyleBackColor = true;
            this.btnLeerArchivo.Click += new System.EventHandler(this.btnLeerArchivo_Click);
            // 
            // listViewLineas
            // 
            this.listViewLineas.HideSelection = false;
            this.listViewLineas.Location = new System.Drawing.Point(378, 44);
            this.listViewLineas.Name = "listViewLineas";
            this.listViewLineas.Size = new System.Drawing.Size(373, 394);
            this.listViewLineas.TabIndex = 3;
            this.listViewLineas.UseCompatibleStateImageBehavior = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listViewLineas);
            this.Controls.Add(this.btnLeerArchivo);
            this.Controls.Add(this.lstLista);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstLista;
        private System.Windows.Forms.Button btnLeerArchivo;
        private System.Windows.Forms.ListView listViewLineas;
    }
}

