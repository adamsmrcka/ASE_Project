using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Project
{
    public abstract class Shape
    {
        protected int XPos, YPos;
        protected Color colour;

        public Shape()
        {
        }

        abstract public void SetPolygon(Color colour, Point[] points);
        abstract public void Set(Color colour, params int[] parameters);

        abstract public void Draw(Graphics g);

    }
}
