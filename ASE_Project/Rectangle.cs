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
        private int[] rectangleParameters;
        public Rectangle() { }

        public override void Set(Color colour, bool fill, int posX, int posY, params int[] parameters)
        {
            this.colour = colour;
            xPos = posX;
            yPos = posY;
            fillShape = fill;
            rectangleParameters = parameters;
        }

        public override void Draw(Graphics g)
        {
            if (!fillShape)
            {
                g.DrawRectangle(new Pen(colour), new System.Drawing.Rectangle(xPos - (rectangleParameters[0] / 2), yPos - (rectangleParameters[1] / 2), rectangleParameters[0], rectangleParameters[1]));
            }
            else
            {
                g.FillRectangle(new SolidBrush(colour), new System.Drawing.Rectangle(xPos - (rectangleParameters[0] / 2), yPos - (rectangleParameters[1] / 2), rectangleParameters[0], rectangleParameters[1]));
            }
        }
    }
}
