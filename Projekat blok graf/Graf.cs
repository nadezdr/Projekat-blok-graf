using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Projekat_blok_graf
{
    class Graf
    {
        List<Cvor> cvorovi;
        List<List<int>> lista_povezanosti;
        static Random r;
        int broj_cvorova;
        int velicina_cvora;
        Pen p;
        int cvorKojiSePomera;

        public Graf(int n)
        {
            p = new Pen(Color.Black, (float)0.5);
            broj_cvorova = n;
            r = new Random();
            cvorovi = new List<Cvor>(n);
            lista_povezanosti = new List<List<int>>(n); //pokusala sa setom ali ne radi
            cvorKojiSePomera = -1;
            

            for (int i = 0; i < n; i++)
            {
                cvorovi.Add(new Cvor(i));
                lista_povezanosti.Add(new List<int>());
                int broj_grana_iz_cvora = r.Next(n/2);
                for (int j = 0; j < broj_grana_iz_cvora; j++)
                {
                    int Do = r.Next(n);
                    if (!lista_povezanosti[i].Contains(Do) && cvorovi[i].Broj!=Do)
                        lista_povezanosti[i].Add(Do);
                }
                
                
            }
        }

        public void NacrtajGrafuKrug(Graphics g,int duzina, int sirina, int velicinaCvora)
        {
            velicina_cvora = velicinaCvora;
            int d = Math.Min(duzina, sirina);
            double r = (d-velicinaCvora)/2.0;
            double ugao=(2*Math.PI)/broj_cvorova;
            double centarX = duzina / 2.0;
            double centarY = sirina / 2.0;
            double x;
            double y;
            for (int i = 0; i < broj_cvorova; i++)
            {
                x = r * (Math.Cos(ugao * i));
                y = r * (Math.Sin(ugao * i));
                cvorovi[i].X = (int)(centarX + x);
                cvorovi[i].Y = (int)(centarY - y);
            }

            NacrtajUPictureBox(g);
        }

        


        public void NacrtajGrafuKvadrat(int duzina, int sirina, int velicinaCvora)
        {
            int d = Math.Min(duzina, sirina)-velicinaCvora;
            int br_redova = (int)Math.Ceiling(Math.Sqrt(broj_cvorova));
            double rastojanje_izmedju_centara = d / br_redova;
            for (int i = 0; i < br_redova; i++)
            {

                for (int j = 0; j < br_redova; j++)
                {

                }
            }

        }

        public void NacrtajUPictureBox(Graphics g)
        {
            for (int i = 0; i < broj_cvorova; i++)
            {
                g.DrawEllipse(p, cvorovi[i].X-velicina_cvora/2, cvorovi[i].Y-velicina_cvora/2, velicina_cvora, velicina_cvora);
                Font font1 = new Font("Times New Roman", velicina_cvora-5, FontStyle.Bold, GraphicsUnit.Pixel);
                g.DrawString(cvorovi[i].Broj.ToString(), font1, p.Brush, cvorovi[i].X-velicina_cvora/3, cvorovi[i].Y-velicina_cvora/2);

                foreach(int k in lista_povezanosti[i])
                {
                    foreach (Cvor cvor in cvorovi)
                    {
                        if (cvor.Broj == k)
                        {
                            NacrtajGranu(g, cvorovi[i], cvor, velicina_cvora);
                        }
                    }
                    
                }
            }
        }

        public void NacrtajGranu(Graphics g, Cvor Od, Cvor Do, int velicinaCvora)
        {
            double r = broj_cvorova/2;
            double cosugla = Math.Abs(Od.X - Do.X)*1.0 / Math.Sqrt((Od.X - Do.X) * (Od.X - Do.X) + (Od.Y - Do.Y) * (Od.Y - Do.Y));
            double sinugla = Math.Abs(Od.Y - Do.Y)*1.0 / Math.Sqrt((Od.X - Do.X) * (Od.X - Do.X) + (Od.Y - Do.Y) * (Od.Y - Do.Y));
            double odrezakX = cosugla * r;
            double odrezakY = sinugla * r;
            
            
            Point OdTacka = new Point();
            Point DoTacka = new Point();
            if(Od.X>Do.X)
            {
                OdTacka.X = (int)(Od.X - odrezakX);
                DoTacka.X = (int)(Do.X + odrezakX);
            }
            else
            {
                OdTacka.X = (int)(Od.X + odrezakX);
                DoTacka.X = (int)(Do.X - odrezakX);
            }
            if(Od.Y>Do.Y)
            {
                OdTacka.Y = (int)(Od.Y - odrezakY);
                DoTacka.Y = (int)(Do.Y + odrezakY);
            }
            else
            {
                OdTacka.Y = (int)(Od.Y + odrezakY);
                DoTacka.Y = (int)(Do.Y - odrezakY);
            }


            //sad strelica
            using (Pen the_pen = new Pen(Color.Black))
            {
                AdjustableArrowCap cap = new AdjustableArrowCap(10, 10, false);
                the_pen.CustomEndCap = cap;

                g.DrawLine(the_pen, OdTacka, DoTacka);
            }

            
        }

        public void MisUCvoru(int x, int y)
        {
            double udaljenost;
            for (int i=0;i<broj_cvorova;i++)
            {
                udaljenost = (x - cvorovi[i].X) * (x - cvorovi[i].X) + (y - cvorovi[i].Y) * (y - cvorovi[i].Y);
                if(udaljenost<velicina_cvora*velicina_cvora)
                {
                    cvorKojiSePomera = i;
                    return;
                }
            }
            cvorKojiSePomera = -1;
        }

        public void PomeranjeMisa(int x, int y)
        {
            if (cvorKojiSePomera != -1)
            {
                cvorovi[cvorKojiSePomera].X = x;
                cvorovi[cvorKojiSePomera].Y = y;
            }
        }

        public void OtpustajMisa(int x, int y)
        {
            if (cvorKojiSePomera != -1)
            {
                cvorovi[cvorKojiSePomera].X = x;
                cvorovi[cvorKojiSePomera].Y = y;
                cvorKojiSePomera = -1;
            }
        }
    }
}
