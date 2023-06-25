using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPL_Application
{
    internal class Rectangle:Shape
    {
        int width, height;
        public Rectangle() : base()
        {
            //width = 800;
            //height = 100;
        }
        public Rectangle(Color colour, int x, int y, int width, int height) : base(colour, x, y)
        {

            this.width = width; //the only thing that is different from shape
            this.height = height;
        }

        public override void set(params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is width, list[3] is height
            //base.set(colour, list[0], list[1]);
            this.width = list[2];
            this.height = list[3];

        }

        public override void draw(Graphics g, bool fill, Color color)
        {
            if (fill == true)
            {
                SolidBrush b = new SolidBrush(color);
                g.FillRectangle(b, x, y, width, height);
            }
            else
            {
                Pen p = new Pen(color, 2);
                g.DrawRectangle(p, x, y, width, height);
            }
        }

    }
}
