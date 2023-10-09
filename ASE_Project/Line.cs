using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Project
{
    internal class Line : Shape
    {
        private int toX, toY;

        public Line() { }

        override public void set(Color colour, bool fill, int posX, int posY, params int[] parameters)
        {
            this.colour = colour;
            xPos = posX;
            yPos = posY;
            toX = parameters[0];
            toY = parameters[1];
        }

        override public void draw(Graphics g)
        {
            g.DrawLine(new Pen(colour), xPos, yPos, toX, toY);
        }
    }
}
