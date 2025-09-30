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
        private int indexImagen = 0;
        private List<Articulo> listaArticulos;

        public Catalogo()
        {
            InitializeComponent();

            pbxArt.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxArt.Click += pbxArt_Click;
            pbxArt.DoubleClick += pbxArt_DoubleClick;

            dgvListaProd.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvListaProd.SelectionChanged += dgvListaProd_SelectionChanged;

            cbCambiarImagen.SelectedIndexChanged += cbCambiarImagen_SelectedIndexChanged;
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

                if (dgvListaProd.Rows.Count > 0)
                {
                    dgvListaProd.ClearSelection();
                    dgvListaProd.Rows[0].Selected = true;
                    dgvListaProd.CurrentCell = dgvListaProd.Rows[0].Cells[1];

                    Articulo seleccionado = dgvListaProd.SelectedRows[0].DataBoundItem as Articulo;
                    if (seleccionado != null)
                        cargarImagen(seleccionado.FirstImage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar artículos: " + ex.Message);
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
                    pbxArt.LoadAsync(url);
                else
                    pbxArt.Image = null;
            }
            catch
            {
                MessageBox.Show("No se pudo cargar la imagen ni desde la web ni desde el disco.", "Error de imagen");
                pbxArt.Image = null;
            }
        }

        private void dgvListaProd_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvListaProd.SelectedRows.Count > 0)
            {
                Articulo seleccionado = (Articulo)dgvListaProd.SelectedRows[0].DataBoundItem;
                indexImagen = 0;
                cargarImagen(seleccionado.FirstImage());

                cbCambiarImagen.DataSource = null;
                cbCambiarImagen.DataSource = seleccionado.Imagenes;
                cbCambiarImagen.DisplayMember = "UrlImagen";

                if (cbCambiarImagen.Items.Count > 0)
                    cbCambiarImagen.SelectedIndex = 0;
            }
        }


        private void pbxArt_Click(object sender, EventArgs e)
        {
            if (dgvListaProd.CurrentRow == null) return;

            Articulo seleccionado = (Articulo)dgvListaProd.CurrentRow.DataBoundItem;

            if (seleccionado.Imagenes == null || seleccionado.Imagenes.Count == 0)
            {
                cargarImagen("");
                return;
            }

            if (indexImagen >= seleccionado.Imagenes.Count)
                indexImagen = 0;

            string url = seleccionado.Imagenes[indexImagen].UrlImagen;
            cargarImagen(url);

            indexImagen = (indexImagen + 1) % seleccionado.Imagenes.Count;
        }

        private void pbxArt_DoubleClick(object sender, EventArgs e)
        {
            if (dgvListaProd.CurrentRow == null) return;

            Articulo seleccionado = (Articulo)dgvListaProd.CurrentRow.DataBoundItem;
            string url = seleccionado.Imagenes != null && seleccionado.Imagenes.Count > 0
                ? seleccionado.Imagenes[indexImagen == 0 ? 0 : indexImagen - 1].UrlImagen
                : "";

            Form visor = new Form
            {
                WindowState = FormWindowState.Maximized,
                BackColor = Color.Black
            };

            PictureBox imagenGrande = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            try
            {
                imagenGrande.Load(string.IsNullOrWhiteSpace(url) ? "https://via.placeholder.com/800x600.png?text=Sin+Imagen" : url);
            }
            catch
            {
                imagenGrande.Load("https://via.placeholder.com/800x600.png?text=Error+al+cargar");
            }

            visor.Controls.Add(imagenGrande);
            visor.ShowDialog();
        }

        private void agregarProducto_Click(object sender, EventArgs e)
        {
            AgregarProducto ventana = new AgregarProducto();
            ventana.ShowDialog();

            cargar();

            if (ventana.IdArticuloCreado.HasValue)
            {
                int idNuevo = ventana.IdArticuloCreado.Value;

                foreach (DataGridViewRow fila in dgvListaProd.Rows)
                {
                    Articulo art = fila.DataBoundItem as Articulo;
                    if (art != null && art.Id == idNuevo)
                    {
                        fila.Selected = true;
                        dgvListaProd.CurrentCell = fila.Cells[1];
                        cargarImagen(art.FirstImage());
                        break;
                    }
                }
            }
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

        private void TxtFiltro_TextChanged(object sender, EventArgs e)
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

            if (dgvListaProd.Rows.Count > 0)
            {
                dgvListaProd.ClearSelection();
                dgvListaProd.Rows[0].Selected = true;
                dgvListaProd.CurrentCell = dgvListaProd.Rows[0].Cells[1];

                Articulo seleccionado = dgvListaProd.SelectedRows[0].DataBoundItem as Articulo;
                if (seleccionado != null)
                    cargarImagen(seleccionado.FirstImage());
            }
            else
            {
                pbxArt.Image = null;
            }
        }

        private void BtnFiltrar_Click(object sender, EventArgs e)
        {
            txtFiltro_TextChanged(sender, e);
        }

        private void cbCambiarImagen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCambiarImagen.SelectedItem is Imagen imagenSeleccionada)
            {
                pbxArt.ImageLocation = imagenSeleccionada.UrlImagen;
            }
            else
            {
                pbxArt.Image = null;
            }
        }


        private void btnEliminarImagen_Click(object sender, EventArgs e)
        {
            if (cbCambiarImagen.SelectedItem is Imagen imagenSeleccionada && dgvListaProd.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvListaProd.CurrentRow.DataBoundItem;

                // Eliminar de la base de datos
                ImagenSQL imagenSQL = new ImagenSQL();
                imagenSQL.EliminarPorUrl(imagenSeleccionada.UrlImagen, seleccionado.Id);

                // Eliminar del objeto en memoria
                seleccionado.Imagenes.Remove(imagenSeleccionada);

                // Recargar ComboBox
                cbCambiarImagen.DataSource = null;
                cbCambiarImagen.DataSource = seleccionado.Imagenes;
                cbCambiarImagen.DisplayMember = "UrlImagen";

                // Actualizar imagen
                if (cbCambiarImagen.Items.Count > 0)
                {
                    cbCambiarImagen.SelectedIndex = 0;
                    Imagen siguiente = cbCambiarImagen.SelectedItem as Imagen;
                    pbxArt.ImageLocation = siguiente?.UrlImagen;
                }
                else
                {
                    pbxArt.Image = null;
                }

                MessageBox.Show("Imagen eliminada correctamente.");
            }
        }

        private void recargarComboImagenes(Articulo articulo)
        {
            cbCambiarImagen.DataSource = null;
            cbCambiarImagen.DataSource = articulo.Imagenes;
            cbCambiarImagen.DisplayMember = "ImagenUrl";
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltro.Text.ToUpper();

            if (filtro.Length >= 2)
            {
                listaFiltrada = listaArticulos.FindAll(x =>
                    x.Nombre.ToUpper().Contains(filtro) ||
                    x.Marca.Descripcion.ToUpper().Contains(filtro) ||
                    x.Descripcion.ToUpper().Contains(filtro)
                );
            }
            else
            {
                listaFiltrada = listaArticulos;
            }

            dgvListaProd.DataSource = null;
            dgvListaProd.DataSource = listaFiltrada;
            ocultarColumnas();

            if (dgvListaProd.Rows.Count > 0)
            {
                dgvListaProd.ClearSelection();
                dgvListaProd.Rows[0].Selected = true;
                dgvListaProd.CurrentCell = dgvListaProd.Rows[0].Cells[1];

                Articulo seleccionado = dgvListaProd.SelectedRows[0].DataBoundItem as Articulo;
                if (seleccionado != null)
                    cargarImagen(seleccionado.FirstImage());
            }
            else
            {
                pbxArt.Image = null;
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = txtFiltro.Text.ToUpper();

            List<Articulo> listaFiltrada;

            if (filtro.Length >= 2)
            {
                listaFiltrada = listaArticulos.FindAll(x =>
                    x.Nombre.ToUpper().Contains(filtro) ||
                    x.Marca.Descripcion.ToUpper().Contains(filtro) ||
                    x.Descripcion.ToUpper().Contains(filtro)
                );
            }
            else
            {
                listaFiltrada = listaArticulos;
            }

            dgvListaProd.DataSource = null;
            dgvListaProd.DataSource = listaFiltrada;
            ocultarColumnas();

            if (dgvListaProd.Rows.Count > 0)
            {
                dgvListaProd.ClearSelection();
                dgvListaProd.Rows[0].Selected = true;
                dgvListaProd.CurrentCell = dgvListaProd.Rows[0].Cells[1];

                Articulo seleccionado = dgvListaProd.SelectedRows[0].DataBoundItem as Articulo;
                if (seleccionado != null)
                    cargarImagen(seleccionado.FirstImage());
            }
            else
            {
                pbxArt.Image = null;
            }
        }










    }
}

    
