using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Items
{
    public partial class VerImagen : Form
    {
        public byte[] Imagen { get; set; }
        public VerImagen()
        {
            InitializeComponent();
        }

        private void VerImagen_Load(object sender, EventArgs e)
        {
            if (Imagen != null)
            {
                using (MemoryStream ms = new MemoryStream(Imagen))
                {
                    PBImagen.Image = Image.FromStream(ms);
                    PBImagen.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }
    }
}
