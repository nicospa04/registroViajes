namespace RegistroViajes
{
    partial class CFamilias
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
            this.label1 = new System.Windows.Forms.Label();
            this.TXTFamilia = new System.Windows.Forms.TextBox();
            this.BTNCrearFamilia = new System.Windows.Forms.Button();
            this.BTNEliminarFamilia = new System.Windows.Forms.Button();
            this.BTNAgregarPermiso = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CBPermisos = new System.Windows.Forms.ComboBox();
            this.CBFamilias = new System.Windows.Forms.ComboBox();
            this.BTNAplicar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "Nombre de la Familia:";
            // 
            // TXTFamilia
            // 
            this.TXTFamilia.Location = new System.Drawing.Point(48, 94);
            this.TXTFamilia.Name = "TXTFamilia";
            this.TXTFamilia.Size = new System.Drawing.Size(129, 20);
            this.TXTFamilia.TabIndex = 63;
            // 
            // BTNCrearFamilia
            // 
            this.BTNCrearFamilia.Location = new System.Drawing.Point(48, 119);
            this.BTNCrearFamilia.Name = "BTNCrearFamilia";
            this.BTNCrearFamilia.Size = new System.Drawing.Size(129, 31);
            this.BTNCrearFamilia.TabIndex = 62;
            this.BTNCrearFamilia.Text = "Crear familia";
            this.BTNCrearFamilia.UseVisualStyleBackColor = true;
            // 
            // BTNEliminarFamilia
            // 
            this.BTNEliminarFamilia.Location = new System.Drawing.Point(48, 156);
            this.BTNEliminarFamilia.Name = "BTNEliminarFamilia";
            this.BTNEliminarFamilia.Size = new System.Drawing.Size(129, 31);
            this.BTNEliminarFamilia.TabIndex = 61;
            this.BTNEliminarFamilia.Text = "Eliminar familia";
            this.BTNEliminarFamilia.UseVisualStyleBackColor = true;
            // 
            // BTNAgregarPermiso
            // 
            this.BTNAgregarPermiso.Location = new System.Drawing.Point(488, 109);
            this.BTNAgregarPermiso.Name = "BTNAgregarPermiso";
            this.BTNAgregarPermiso.Size = new System.Drawing.Size(129, 23);
            this.BTNAgregarPermiso.TabIndex = 60;
            this.BTNAgregarPermiso.Text = "Agregar Permiso";
            this.BTNAgregarPermiso.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(224, 34);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(214, 212);
            this.treeView1.TabIndex = 59;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(485, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 58;
            this.label3.Text = "Permisos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(485, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 57;
            this.label2.Text = "Familias";
            // 
            // CBPermisos
            // 
            this.CBPermisos.FormattingEnabled = true;
            this.CBPermisos.Location = new System.Drawing.Point(488, 82);
            this.CBPermisos.Name = "CBPermisos";
            this.CBPermisos.Size = new System.Drawing.Size(129, 21);
            this.CBPermisos.TabIndex = 56;
            // 
            // CBFamilias
            // 
            this.CBFamilias.FormattingEnabled = true;
            this.CBFamilias.Location = new System.Drawing.Point(488, 162);
            this.CBFamilias.Name = "CBFamilias";
            this.CBFamilias.Size = new System.Drawing.Size(129, 21);
            this.CBFamilias.TabIndex = 55;
            // 
            // BTNAplicar
            // 
            this.BTNAplicar.Location = new System.Drawing.Point(488, 189);
            this.BTNAplicar.Name = "BTNAplicar";
            this.BTNAplicar.Size = new System.Drawing.Size(129, 23);
            this.BTNAplicar.TabIndex = 54;
            this.BTNAplicar.Text = "Aplicar";
            this.BTNAplicar.UseVisualStyleBackColor = true;
            // 
            // CFamilias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(693, 299);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TXTFamilia);
            this.Controls.Add(this.BTNCrearFamilia);
            this.Controls.Add(this.BTNEliminarFamilia);
            this.Controls.Add(this.BTNAgregarPermiso);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CBPermisos);
            this.Controls.Add(this.CBFamilias);
            this.Controls.Add(this.BTNAplicar);
            this.Name = "CFamilias";
            this.Text = "CFamilias";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TXTFamilia;
        private System.Windows.Forms.Button BTNCrearFamilia;
        private System.Windows.Forms.Button BTNEliminarFamilia;
        private System.Windows.Forms.Button BTNAgregarPermiso;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CBPermisos;
        private System.Windows.Forms.ComboBox CBFamilias;
        private System.Windows.Forms.Button BTNAplicar;
    }
}