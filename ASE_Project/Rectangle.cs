using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Project
{
    internal class Rectangle : Shape
    {
        private int height, width;
        public Rectangle() { }

        public override void set(Color colour, bool fill, int posX, int posY, params int[] parameters)
        {
            this.colour = colour;
            xPos = posX;
            yPos = posY;
            fillShape = fill;
            width = parameters[0];
            height = parameters[1];
        }

        public override void draw(Graphics g)
        {
            if (!fillShape)
            {
                g.DrawRectangle(new Pen(colour), new System.Drawing.Rectangle(xPos - (width / 2), yPos - (height / 2), width, height));
            }
            else
            {
                g.FillRectangle(new SolidBrush(colour), new System.Drawing.Rectangle(xPos - (width / 2), yPos - (height / 2), width, height));
            }
        }
    }
}
