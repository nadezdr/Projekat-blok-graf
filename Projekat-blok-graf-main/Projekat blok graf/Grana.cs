using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_blok_graf
{
    class Grana
    {
        //int tezina;
        Cvor Od;
        Cvor Do;
        Color boja;

        public Color Boja
        {
            get { return boja; }
            set { boja = value; }
        } 

        public Grana(Cvor Od, Cvor Do)
        {
            this.Od = Od;
            this.Do = Do;
        }
    }
}
