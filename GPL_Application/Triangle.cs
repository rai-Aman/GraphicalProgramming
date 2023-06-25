using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GPL_Application
{
    internal class Triangle:Shape
    {
        int s1, s2, s3 ; 
        public Triangle() : base()
        {
        
        }

        public Triangle(Color colour, int x, int y, int s1, int s2, int s3) : base(colour, x, y)
        {
            this.s1 = s1;
            this.s2 = s2;   
            this.s3 = s3;   

        }
        public override void draw(Graphics g, bool fill, Color color)
        {

            PointF firstPoint = new PointF(s1, s2);
            PointF secPoint = new PointF(s2, s3);

            //finding average point
            double avgY = (Math.Pow(s1, 2) + Math.Pow(s3, 2) - Math.Pow(s2, 2)) / (s1 * 2);
            double avgX = Math.Sqrt(Math.Pow(s3, 2) - Math.Pow(avgY, 2));

            PointF thirdPoint = new PointF((float)avgX, (float)avgY);
            if (fill)
            {
                SolidBrush b = new SolidBrush(color);
                PointF[] allPoints = new PointF[] { firstPoint, secPoint, thirdPoint };
                g.FillPolygon(b, allPoints);
            }
            else
            {
                Pen p = new Pen(color, 2);
                g.DrawLine(p, firstPoint, secPoint);
                g.DrawLine(p, secPoint, thirdPoint);
                g.DrawLine(p, thirdPoint, firstPoint);
            }


        }


        public override void set( params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is width, list[3] is height
            base.set(list[0], list[1]);
            this.s1 = list[2];
            this.s2 = list[3];
            this.s3 = list[4];

        }

    }
}


