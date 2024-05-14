using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintApplicationAssignment
{
    [Serializable]
    class Triangle
    {
        Point Start, End;
        private Pen pen;

        public Triangle(Point S, Point E)
        {
            Start = S;
            End = E;
        }
        public void Draw(Graphics g, Pen p)
        {
            double xMid = (Start.X + End.X) / 2;
            Point first = new Point(Start.X + End.Y);
            Point mid = new Point((int)xMid, Start.Y);
            g.DrawLine(p, first, mid);
            g.DrawLine(p, first, End);
            g.DrawLine(p, End, mid);
        }
    }
}
