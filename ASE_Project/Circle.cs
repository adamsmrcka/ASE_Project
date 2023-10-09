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

        override public void set(Color colour, bool fill, int posX, int posY, params int[] parameters)
        {
            this.colour = colour;
            xPos = posX;
            yPos = posY;
            fillShape = fill;
            size = parameters[0];
        }

        override public void draw(Graphics g)
        {
            if (!fillShape)
            {
                g.DrawEllipse(new Pen(colour), xPos - (size / 2), yPos - (size / 2), size, size);
            }
            else
            {
                g.FillEllipse(new SolidBrush(colour), xPos - (size / 2), yPos - (size / 2), size, size);
            }
        }
    }
}

