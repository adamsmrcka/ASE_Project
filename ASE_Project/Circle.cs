using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Project
{
    internal class Circle : Shape
    {
        protected int size;

        public Circle() { }

        override public void Set(Color colour, params int[] parameters)
        {
            this.colour = colour;
            XPos = parameters[0];
            YPos = parameters[1];
            size = parameters[2];
        }

        override public void Draw(Graphics g)
        {
            g.DrawEllipse(new Pen(colour), new Rectangle(XPos, YPos, size, size));
        }
    }
}

