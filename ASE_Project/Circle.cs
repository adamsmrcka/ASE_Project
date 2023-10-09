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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="colour"> Colour of the pen</param>
        /// <param name="fill"> Boolean if fill or just draw</param>
        /// <param name="posX"> X position of the circle centre</param>
        /// <param name="posY"> Y position of the circle centre</param>
        /// <param name="parameters"> Array of parameters (diameter)</param>
        override public void set(Color colour, bool fill, int posX, int posY, params int[] parameters)
        {
            this.colour = colour;
            xPos = posX;
            yPos = posY;
            fillShape = fill;
            size = parameters[0];
        }

        /// <summary>
        /// Draws the Circle either filled or just an outline
        /// </summary>
        /// <param name="g"></param>
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

