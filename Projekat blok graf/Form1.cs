using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekat_blok_graf
{
    public partial class Form1 : Form
    {
        Graf g;
        bool prvo_crtanje;
        public Form1()
        {
            g = new Graf(10);
            prvo_crtanje = true;
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (prvo_crtanje)
            {
                g.NacrtajGrafuKrug(e.Graphics, pictureBox1.Width, pictureBox1.Height, 30);
                prvo_crtanje = false;
            }
            g.NacrtajUPictureBox(e.Graphics);
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            g.MisUCvoru(e.X, e.Y);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            g.PomeranjeMisa(e.X, e.Y);
            Refresh();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            g.OtpustajMisa(e.X, e.Y);
            Refresh();
        }
    }
}
