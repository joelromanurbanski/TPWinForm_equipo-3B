using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using SQL;

namespace tp_winform_equipo_3b
{
    public partial class AgregarProducto : Form
    {
        private Articulo articulo = null;
        private bool esModificacion = false;

        public AgregarProducto()
        {
            InitializeComponent();
        }

        public AgregarProducto(Articulo art)
        {
            InitializeComponent();
            articulo = art;
            esModificacion = true;
            this.Text = "Modificar Producto";
        }

        private void AgregarProducto_Load(object sender, EventArgs e)
        {
            MarcaSQL marcaSQL = new MarcaSQL();
            CategoriaSQL categoriaSQL = new CategoriaSQL();

            cbMarca.DataSource = marcaSQL.Listar();
            cbMarca.DisplayMember = "Descripcion";
            cbMarca.ValueMember = "Id";

            cbCategoria.DataSource = categoriaSQL.Listar();
            cbCategoria.DisplayMember = "Descripcion";
            cbCategoria.ValueMember = "Id";

            if (esModificacion && articulo != null)
            {
                textCodigo.Text = articulo.Codigo;
                textNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                textPrecio.Text = articulo.Precio.ToString();
                txtUrlImagen.Text = articulo.UrlImagen;

                cbMarca.SelectedValue = articulo.IdMarca;
                cbCategoria.SelectedValue = articulo.IdCategoria;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloSQL articuloSQL = new ArticuloSQL();
                ImagenSQL imagenSQL = new ImagenSQL();

                if (articulo == null)
                    articulo = new Articulo();

                articulo.Codigo = textCodigo.Text;
                articulo.Nombre = textNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.UrlImagen = txtUrlImagen.Text;

                if (decimal.TryParse(textPrecio.Text, out decimal precio))
                    articulo.Precio = precio;
                else
                    articulo.Precio = 0;

                articulo.IdMarca = (int)cbMarca.SelectedValue;
                articulo.IdCategoria = (int)cbCategoria.SelectedValue;

                if (esModificacion)
                {
                    articuloSQL.Modificar(articulo);
                    MessageBox.Show("Artículo modificado con éxito");
                }
                else
                {
                    int idNuevo = articuloSQL.AgregarYDevolverId(articulo);

                    if (!string.IsNullOrWhiteSpace(txtUrlImagen.Text))
                    {
                        Imagen imagen = new Imagen { UrlImagen = txtUrlImagen.Text };
                        imagenSQL.Agregar(imagen, idNuevo);
                    }

                    MessageBox.Show("Artículo agregado con éxito");
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTN_AgregarImagenes_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Archivos de imagen|*.jpg;*.png;*.jpeg;*.gif";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtUrlImagen.Text = dialog.FileName;
            }
        }
    }
}
