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
        protected int xPos, yPos;
        protected Color colour;
        protected Graphics g;
        protected bool fillShape;

        public Shape()
        {
        }
        abstract public void set(Color colour, bool fill, int posX, int posY, params int[] parameters);

        abstract public void draw(Graphics g);

    }
}
