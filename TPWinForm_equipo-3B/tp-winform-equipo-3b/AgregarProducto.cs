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

        public int? IdArticuloCreado { get; private set; } = null;

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

                if (!esModificacion && articulo == null)
                    articulo = new Articulo();

                articulo.Codigo = textCodigo.Text;
                articulo.Nombre = textNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.UrlImagen = txtUrlImagen.Text;

                if (string.IsNullOrWhiteSpace(textCodigo.Text))
                {
                    MessageBox.Show("El código es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textNombre.Text))
                {
                    MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("La descripción es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (decimal.TryParse(textPrecio.Text, out decimal precio))
                    articulo.Precio = precio;
                else
                    articulo.Precio = 0;

                articulo.IdMarca = (int)cbMarca.SelectedValue;
                articulo.IdCategoria = (int)cbCategoria.SelectedValue;

                if (esModificacion)
                {
                    MessageBox.Show("Modificando artículo con ID: " + articulo.Id);
                    articuloSQL.Modificar(articulo);

                    // 🔁 Eliminar imágenes anteriores
                    imagenSQL.EliminarPorArticulo(articulo.Id);

                    // ✅ Agregar todas las nuevas imágenes del ListBox
                    foreach (string url in lstImagenes.Items)
                    {
                        Imagen img = new Imagen
                        {
                            UrlImagen = url,
                            IdArticulo = articulo.Id
                        };
                        imagenSQL.Agregar(img, articulo.Id);
                    }

                    MessageBox.Show("Artículo modificado con éxito");
                }
                else
                {
                    int idNuevo = articuloSQL.AgregarYDevolverId(articulo);
                    IdArticuloCreado = idNuevo;

                    foreach (string url in lstImagenes.Items)
                    {
                        Imagen img = new Imagen
                        {
                            UrlImagen = url,
                            IdArticulo = idNuevo
                        };
                        imagenSQL.Agregar(img, idNuevo);
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

        private void BTN_AgregarImagenes_Click(object sender, EventArgs e)
        {
            string url = txtUrlImagen.Text.Trim();

            if (!string.IsNullOrWhiteSpace(url))
            {
                if (!lstImagenes.Items.Contains(url))
                {
                    lstImagenes.Items.Add(url);
                    MessageBox.Show("URL agregada al artículo.");
                }
                else
                {
                    MessageBox.Show("La URL ya fue agregada.");
                }
            }
            else
            {
                MessageBox.Show("Por favor ingresá una URL válida antes de agregar.");
            }
        }

        private void btnEliminarImagen_Click(object sender, EventArgs e)
        {
            if (lstImagenes.SelectedItem != null)
            {
                lstImagenes.Items.Remove(lstImagenes.SelectedItem);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

