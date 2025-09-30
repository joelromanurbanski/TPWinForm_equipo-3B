using System;
using System.Windows.Forms;

namespace tp_winform_equipo_3b
{
    partial class Catalogo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Catalogo));
            this.dgvListaProd = new System.Windows.Forms.DataGridView();
            this.pbxArt = new System.Windows.Forms.PictureBox();
            this.agregarProducto = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminarFisico = new System.Windows.Forms.Button();
            this.Filtrar = new System.Windows.Forms.Label();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.cbCambiarImagen = new System.Windows.Forms.ComboBox();
            this.btnEliminarImagen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaProd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxArt)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvListaProd
            // 
            this.dgvListaProd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaProd.Location = new System.Drawing.Point(12, 59);
            this.dgvListaProd.Name = "dgvListaProd";
            this.dgvListaProd.Size = new System.Drawing.Size(841, 365);
            this.dgvListaProd.TabIndex = 0;
            this.dgvListaProd.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListaProd_CellContentClick);
            // 
            // pbxArt
            // 
            this.pbxArt.Location = new System.Drawing.Point(860, 59);
            this.pbxArt.Name = "pbxArt";
            this.pbxArt.Size = new System.Drawing.Size(346, 365);
            this.pbxArt.TabIndex = 1;
            this.pbxArt.TabStop = false;
            this.pbxArt.Click += new System.EventHandler(this.pbxArt_Click);
            // 
            // agregarProducto
            // 
            this.agregarProducto.BackColor = System.Drawing.Color.Lime;
            this.agregarProducto.Location = new System.Drawing.Point(13, 431);
            this.agregarProducto.Name = "agregarProducto";
            this.agregarProducto.Size = new System.Drawing.Size(143, 23);
            this.agregarProducto.TabIndex = 2;
            this.agregarProducto.Text = "Agregar Producto";
            this.agregarProducto.UseVisualStyleBackColor = false;
            this.agregarProducto.Click += new System.EventHandler(this.agregarProducto_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.Orange;
            this.btnModificar.Location = new System.Drawing.Point(354, 431);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(143, 23);
            this.btnModificar.TabIndex = 3;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminarFisico
            // 
            this.btnEliminarFisico.BackColor = System.Drawing.Color.Red;
            this.btnEliminarFisico.Location = new System.Drawing.Point(710, 431);
            this.btnEliminarFisico.Name = "btnEliminarFisico";
            this.btnEliminarFisico.Size = new System.Drawing.Size(143, 23);
            this.btnEliminarFisico.TabIndex = 4;
            this.btnEliminarFisico.Text = "Eliminar";
            this.btnEliminarFisico.UseVisualStyleBackColor = false;
            this.btnEliminarFisico.Click += new System.EventHandler(this.btnEliminarFisico_Click);
            // 
            // Filtrar
            // 
            this.Filtrar.AutoSize = true;
            this.Filtrar.Location = new System.Drawing.Point(17, 31);
            this.Filtrar.Name = "Filtrar";
            this.Filtrar.Size = new System.Drawing.Size(107, 13);
            this.Filtrar.TabIndex = 5;
            this.Filtrar.Text = "Filtrar por descripción";
            this.Filtrar.Click += new System.EventHandler(this.Filtrar_Click);
            // 
            // txtFiltro
            // 
            this.txtFiltro.Location = new System.Drawing.Point(130, 29);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(574, 20);
            this.txtFiltro.TabIndex = 6;
            //this.txtFiltro.TextChanged += new System.EventHandler(this.txtFiltro_TextChanged);
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(710, 28);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(143, 22);
            this.btnFiltrar.TabIndex = 7;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);

            // 
            // cbCambiarImagen
            // 
            this.cbCambiarImagen.FormattingEnabled = true;
            this.cbCambiarImagen.Location = new System.Drawing.Point(859, 29);
            this.cbCambiarImagen.Name = "cbCambiarImagen";
            this.cbCambiarImagen.Size = new System.Drawing.Size(310, 21);
            this.cbCambiarImagen.TabIndex = 8;
            this.cbCambiarImagen.SelectedIndexChanged += new System.EventHandler(this.cbCambiarImagen_SelectedIndexChanged);
            // 
            // btnEliminarImagen
            // 
            this.btnEliminarImagen.BackColor = System.Drawing.Color.Red;
            this.btnEliminarImagen.Location = new System.Drawing.Point(1176, 29);
            this.btnEliminarImagen.Name = "btnEliminarImagen";
            this.btnEliminarImagen.Size = new System.Drawing.Size(29, 23);
            this.btnEliminarImagen.TabIndex = 9;
            this.btnEliminarImagen.Text = "❎";
            this.btnEliminarImagen.UseVisualStyleBackColor = false;
            this.btnEliminarImagen.Click += new System.EventHandler(this.btnEliminarImagen_Click);
            // 
            // Catalogo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 550);
            this.Controls.Add(this.btnEliminarImagen);
            this.Controls.Add(this.cbCambiarImagen);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.txtFiltro);
            this.Controls.Add(this.Filtrar);
            this.Controls.Add(this.btnEliminarFisico);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.agregarProducto);
            this.Controls.Add(this.pbxArt);
            this.Controls.Add(this.dgvListaProd);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Catalogo";
            this.Text = "Catalogo";
            this.Load += new System.EventHandler(this.Catalogo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaProd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxArt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Filtrar_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void dgvListaProd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvListaProd;
        private System.Windows.Forms.PictureBox pbxArt;
        private System.Windows.Forms.Button agregarProducto;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminarFisico;
        private System.Windows.Forms.Label Filtrar;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.Button btnFiltrar;
        private ComboBox cbCambiarImagen;
        private Button btnEliminarImagen;
    }
}

