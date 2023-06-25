using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPL_Application
{
    internal class DrawLine : Shape
    {
        int xCor, yCor;
        public DrawLine() : base()

        {

        }

        public override void set(params int[] list)

        {

            base.set(list[0], list[1]);

            this.xCor = list[2];

            this.yCor = list[3];

        }

        public override void draw(Graphics g, bool fill, Color color)

        {

            Pen p = new Pen(color, 2);

            PointF firstPoint = new PointF(x, y);

            PointF secondpoint = new PointF(xCor, yCor);

            g.DrawLine(p, firstPoint, secondpoint);

        }
    }
}
