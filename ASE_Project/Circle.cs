using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Project
{
    class Circle : Shape
    {
        protected int size;

        public Circle() { }

        override public void Set(Color colour, int posX, int posY, params int[] parameters)
        {
            this.colour = colour;
            xPos = posX;
            yPos = posY;
            size = parameters[0];
        }

        override public void Draw(Graphics g)
        {
            g.DrawEllipse(new Pen(colour), xPos - (size/2), yPos - (size / 2), size, size);
        }
    }
}

