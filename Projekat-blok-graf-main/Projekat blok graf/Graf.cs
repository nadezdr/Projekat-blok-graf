using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace Projekat_blok_graf
{
    class Graf
    {
        List<Cvor> cvorovi;
        List<List<Tuple<int,Color>>> lista_povezanosti;
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
            lista_povezanosti = new List<List<Tuple<int, Color>>>(n); //pokusala sa setom ali ne radi
            cvorKojiSePomera = -1;
            

            for (int i = 0; i < n; i++)
            {
                cvorovi.Add(new Cvor(i));
                lista_povezanosti.Add(new List<Tuple<int,Color>>());
                int broj_grana_iz_cvora = r.Next(n/2);
                for (int j = 0; j < broj_grana_iz_cvora; j++)
                {
                    int Do = r.Next(n);
                    if (!lista_povezanosti[i].Any(m=> m.Item1==Do) && cvorovi[i].Broj!=Do)
                        lista_povezanosti[i].Insert(Do,new Color.Black());
                }
                lista_povezanosti[i].Sort();
                
                
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
                Pen p = new Pen(cvorovi[i].Boja);
                g.DrawEllipse(p, cvorovi[i].X-velicina_cvora/2, cvorovi[i].Y-velicina_cvora/2, velicina_cvora, velicina_cvora);
                Font font1 = new Font("Times New Roman", velicina_cvora-5, FontStyle.Bold, GraphicsUnit.Pixel);
                g.DrawString(cvorovi[i].Broj.ToString(), font1, p.Brush, cvorovi[i].X-velicina_cvora/3, cvorovi[i].Y-velicina_cvora/2);

                foreach(var x in lista_povezanosti[i])
                {
                    int k = x.Item1;
                    foreach (Cvor cvor in cvorovi)
                    {
                        if (cvor.Broj == k)
                        {
                            NacrtajGranu(g, cvorovi[i], cvor,velicina_cvora);
                        }
                    }
                    
                }
            }
        }

        public void ObojCvor(Graphics g, Cvor cv, Color c)
        {
            SolidBrush sb = new SolidBrush(c);
            g.FillEllipse(sb, cv.X - velicina_cvora / 2, cv.Y - velicina_cvora / 2, velicina_cvora, velicina_cvora);
            Font font1 = new Font("Times New Roman", velicina_cvora - 5, FontStyle.Bold, GraphicsUnit.Pixel);
            g.DrawString(cv.Broj.ToString(), font1, p.Brush, cv.X - velicina_cvora / 3, cv.Y - velicina_cvora / 2);
        }

        

        public void NacrtajGranu(Graphics g, Cvor Od, Cvor Do, int velicinaCvora,Color? c=null)
        {
            double r = velicinaCvora/2;
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
            using (Pen the_pen = new Pen(c ?? Color.Black))
            {
                AdjustableArrowCap cap = new AdjustableArrowCap(10, 10, false);
                the_pen.CustomEndCap = cap;

                g.DrawLine(the_pen, OdTacka, DoTacka);
            }


            //moze preko vektora
            int BAx = Od.X - Do.X;
            int BAy = Od.Y - Do.Y;
            double BA = Math.Sqrt(BAx * BAx + BAy * BAy);

            
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

        List<bool> poseceni;
        Stack<Cvor> st;
        List<Tuple<Cvor,bool>> redosled_prolaska;
        List<Tuple<Cvor, Cvor>> redosled_grana;
        public void dfs_poc(Form f)
        {
            Cvor c = cvorovi[0];
            poseceni = new List<bool>(broj_cvorova);
            for (int i = 0; i < broj_cvorova; i++)
                poseceni.Add(false);
            //return c;

            //sa stekom
            st = new Stack<Cvor>();
            st.Push(c);
            redosled_prolaska = new List<Tuple<Cvor,bool>>(broj_cvorova);//false na pocetku tj kad prolazi prvi put 
            redosled_grana = new List<Tuple<Cvor, Cvor>>();//isto i ovde
        }

        public void dfs(Form f, ref Cvor c)
        {

            poseceni[c.Broj] = true;
            
            c.Boja = Color.DarkBlue;
            f.Refresh();
            foreach (var x in lista_povezanosti[c.Broj])
            {
                int i = x.Item1;
                if (!poseceni[i])
                {
                    
                    Cvor cv = cvorovi[i];
                    redosled_prolaska.Add(new Tuple<Cvor, bool>(c, false));
                    redosled_grana.Add(new Tuple<Cvor, Cvor>(c, cv));
                    cv.Boja = Color.Blue;
                    f.Refresh();

                    //NacrtajGranu(g, c, cv, velicina_cvora, Color.Blue);
                    dfs(f,ref cv);
                    redosled_prolaska.Add(new Tuple<Cvor, bool>(c, true));
                }
            }
        }

        public void crtanjeGrafa()
        {

        }

        Cvor prethodni = new Cvor(-1);
        public void otkucajDfsStek()
        {
            
            if(st.Count!=0)
            {
                Cvor cvor = st.Peek();
                
                //cout<<cvor<<" ";
                st.Pop();
                cvor.Boja = Color.Red;
                //f.Refresh();
                //prethodni.Boja = Color.Pink;
                for (int i = lista_povezanosti[cvor.Broj].Count() - 1; i >= 0; i--)
                {
                    int sused = lista_povezanosti[cvor.Broj][i].Item1;
                    if (!poseceni[sused])
                    {
                        //cout<<cvor<<" "<<sused;
                        st.Push(cvorovi[sused]);
                        poseceni[sused] = true;
                        //cvorovi[sused].Boja = Color.Red;
                    }
                }
                prethodni = cvor;
            }
            
        }

        


    }
}
