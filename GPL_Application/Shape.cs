using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPL_Application
{
    public abstract class Shape:Shapes
    {
        protected Color colour; //shape's colour
        protected int x, y;
        public Shape()
        {
            //colour = Color.Red;
            //x = y = 100;
        }


        public Shape(Color color, int x, int y)
        {
            this.colour = color;
            this.x = x; //its x pos
            this.y = y; //its y pos
            //can't provide anything else as "shape" is too general
        }

        //the three methods below are from the Shapes interface
        //here we are passing on the obligation to implement them to the derived classes by declaring them as abstract
        public abstract void draw(Graphics g, bool fill, Color color);

        //set is declared as virtual so it can be overridden by a more specific child version
        //but is here so it can be called by that child version to do the generic stuff
        //note the use of the param keyword to provide a variable parameter list to cope with some shapes having more setup information than others (in Java it is called varargs and uses the … notation
        public virtual void set( params int[] list)
        {
            this.x = list[0];
            this.y = list[1];
        }
        public override string ToString()
        {
            return base.ToString() + "    " + this.x + "," + this.y + " : ";
        }

    }





    

}

