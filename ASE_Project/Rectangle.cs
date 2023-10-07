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

        public override void Set(Color colour, params int[] parameters)
        {
            this.colour = colour;
            rectangleParameters = parameters;
        }

        public override void Draw(Graphics g)
        {
            g.DrawRectangle(new Pen(colour), new System.Drawing.Rectangle(rectangleParameters[0], rectangleParameters[1], rectangleParameters[2], rectangleParameters[3]));
        }

        public override void SetPolygon(Color colour, Point[] points)
        {
            throw new NotImplementedException();
        }
    }
}
