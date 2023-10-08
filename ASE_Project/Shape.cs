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

        public Shape()
        {
        }
        abstract public void Set(Color colour, int posX, int posY, params int[] parameters);

        abstract public void Draw(Graphics g);

    }
}
