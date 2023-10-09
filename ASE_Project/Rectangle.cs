using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Project
{
    internal class Rectangle : Shape
    {
        private int[] rectangleParameters;
        public Rectangle() { }

        public override void Set(Color colour, int posX, int posY, params int[] parameters)
        {
            this.colour = colour;
            xPos = posX;
            yPos = posY;
            rectangleParameters = parameters;
        }

        public override void Draw(Graphics g)
        {
            g.DrawRectangle(new Pen(colour), new System.Drawing.Rectangle(xPos - (rectangleParameters[0] / 2), yPos - (rectangleParameters[1] / 2), rectangleParameters[0], rectangleParameters[1]));
        }
    }
}
