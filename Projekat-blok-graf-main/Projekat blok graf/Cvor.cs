using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_blok_graf
{
    class Cvor
    {
        int broj;
        int x_koordinata;//koordinate centra
        int y_koordinata;
        Color c;

        public int X 
        {
            get { return x_koordinata; }
            set { x_koordinata = value; }
        }

        public int Y
        {
            get { return y_koordinata; }
            set { y_koordinata = value; }
        }

        public int Broj
        {
            get { return broj; }
        }

        public Color Boja
        {
            get { return c; }
            set { c = value; }
        }

        public Cvor(int broj)
        {
            this.broj = broj;
            c = Color.Black;
        }
    }
}
