using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPL_Application
{
    internal class Circle : Shape
    {
            int radius;

            public Circle() : base()
            {

            }
            public Circle(Color colour, int x, int y, int radius) : base(colour, x, y)
            {

                this.radius = radius; //the only thingthat is different from shape
            }


            public override void set( params int[] list)
            {
                //list[0] is x, list[1] is y, list[2] is radius
                base.set(list[0], list[1]);
                this.radius = list[2];


            }



            public override void draw(Graphics g, bool fill, Color color )
            {

            if (fill == true)
            {
                SolidBrush b = new SolidBrush(color);
                g.FillEllipse(b, x, y, radius * 2, radius * 2);
            }
            else
            {
                Pen p = new Pen(color, 2);
                g.DrawEllipse(p, x, y, radius * 2, radius * 2);
            }

        }

            public override string ToString() //all classes inherit from object and ToString() is abstract in object
            {
                return base.ToString() + "  " + this.radius;
            }
    }
}
