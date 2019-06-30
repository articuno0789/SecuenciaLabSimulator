using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo
{
    /*static int orden = 0;

    int num;
    Vector2 centro;
    int radio;
    Rectangle rec;

    GraphicsPath gp;
    Pen circ;
    Pen lin;
    Font letra;
    List<Nodo> conec;

    public Nodo(int x, int y, int r)
    {
        centro = new Vector2(x, y);
        radio = r;
        rec = new Rectangle(x - r, y - r, 2 * r, 2 * r);

        gp = new GraphicsPath();
        gp.AddEllipse(rec);

        circ = new Pen(Brushes.Red, 3); // Node circulo
        letra = new Font("arial", 12); // Texto

        num = ++orden;
        conec = new List<Nodo>();

        lin = new Pen(Brushes.DimGray, 3);
        GraphicsPath gpLin = new GraphicsPath();
        gpLin.AddLine(new Vector2(0, 0), new Vector2(3, -3));
        gpLin.AddLine(new Vector2(0, 0), new Vector2(-3, -3));
        lin.CustomEndCap = new CustomLineCap(null, gpLin);
    }

    public virtual void DibujaNodo(Graphics g)
    {
        g.FillPath(Brushes.Green, gp);  // Circulo
        g.DrawPath(circ, gp);
        g.DrawString(num.ToString(), letra, Brushes.Black, rec, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
    }

    public virtual void DibujaArista(Graphics g)
    {
        foreach (Nodo item in conec)
        {
            double tg = (double)(centro.Y - item.centro.Y) / (item.centro.X - centro.X);
            double atg = Math.Atan(tg);

            int a = (int)(radio * Math.Cos(atg));
            int b = (int)(radio * Math.Sin(atg));

            if ((centro.X < item.centro.X))
            {
                a *= -1;
                b *= -1;
            }

            Vector2 p = new Vector2(item.centro.X + a, item.centro.Y - b);
            g.DrawLine(lin, centro, p);
        }
    }

    public bool Adentro(Vector2 pt)
    {
        return gp.IsVisible(pt);
    }

    public void Posicion(Vector2 pt)
    {
        gp.Transform(new Matrix(1, 0, 0, 1, pt.x - centro.x, pt.y - centro.y));
        centro = pt;
        rec = Rectangle.Round(gp.GetBounds());
    }

    public void ConectarA(Nodo n)
    {
        conec.Add(n);
    }

    public void Desconectar(Nodo n)
    {
        conec.Remove(n);
    }*/
}
