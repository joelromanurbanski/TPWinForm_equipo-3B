using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using SQL;

namespace tp_winform_equipo_3b
{
    internal static class Program
    {
    
        class Articulo

        {
            public int Id { get; set; }
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public int IdMarca { get; set; }
            public int IdCategoria { get; set; }
            public decimal Precio { get; set; }
        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Catalogo());
        }
    }
}
