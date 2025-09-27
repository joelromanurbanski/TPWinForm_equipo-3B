using Dominio;
using SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace tp_winform_equipo_3b
{
    public partial class Catalogo : Form
    {
        private List<Articulo> listaArticulos;

        public Catalogo()
        {
            InitializeComponent();
        }

        private void Catalogo_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            ArticuloSQL articuloSQL = new ArticuloSQL();
            try
            {
                listaArticulos = articuloSQL.Listar();
                dgvListaProd.DataSource = listaArticulos;
                ocultarColumnas();
                if (listaArticulos.Count > 0)
                    cargarImagen(listaArticulos[0].UrlImagen);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ocultarColumnas()
        {
            dgvListaProd.Columns["Id"].Visible = false;
            dgvListaProd.Columns["UrlImagen"].Visible = false;
        }

        private void cargarImagen(string url)
        {
            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                    pbxArt.Load(url);
                }
                else
                {
                    // Si no hay URL en la BD
                    pbxArt.Load("https://via.placeholder.com/250x250.png?text=Sin+Imagen");
                }
            }
            catch
            {
                // Si la URL no responde o da error
                pbxArt.Load("https://via.placeholder.com/250x250.png?text=Error+Imagen");
            }
        }


        private void dgvListaProd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvListaProd.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvListaProd.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.UrlImagen);
            }
        }

        private void pbxArt_Click(object sender, EventArgs e)
        {
            // Si querés que haga algo al clickear la imagen
        }

        private void agregarProducto_Click(object sender, EventArgs e)
        {
            AgregarProducto ventana = new AgregarProducto();
            ventana.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvListaProd.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvListaProd.CurrentRow.DataBoundItem;
                AgregarProducto ventana = new AgregarProducto(seleccionado);
                ventana.ShowDialog();
                cargar();
            }
            else
            {
                MessageBox.Show("Seleccione un artículo para modificar.");
            }
        }

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            if (dgvListaProd.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvListaProd.CurrentRow.DataBoundItem;
                DialogResult respuesta = MessageBox.Show("¿Está seguro de eliminar este artículo?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    ArticuloSQL articuloSQL = new ArticuloSQL();
                    articuloSQL.Eliminar(seleccionado.Id);
                    cargar();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un artículo para eliminar.");
            }
        }

        private void Filtrar_Click(object sender, EventArgs e)
        {
            // No hace falta usar el click del Label, lo podés dejar vacío
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltro.Text.ToUpper();

            if (filtro.Length >= 2)
                listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro) || x.Marca.Descripcion.ToUpper().Contains(filtro));
            else
                listaFiltrada = listaArticulos;

            dgvListaProd.DataSource = null;
            dgvListaProd.DataSource = listaFiltrada;
            ocultarColumnas();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            // Aplica el mismo filtro que txtFiltro_TextChanged
            txtFiltro_TextChanged(sender, e);
        }
    }
}
