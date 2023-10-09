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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="colour"> Colour of the pen</param>
        /// <param name="fill"> Boolean if fill or just draw</param>
        /// <param name="posX"> X position of the start point</param>
        /// <param name="posY"> Y position of the start point</param>
        /// <param name="parameters"> Array of parameters (X and Y values of the end points)</param>
        override public void set(Color colour, bool fill, int posX, int posY, params int[] parameters)
        {
            this.colour = colour;
            xPos = posX;
            yPos = posY;
            toX = parameters[0];
            toY = parameters[1];
        }
        /// <summary>
        /// Draws a line
        /// </summary>
        /// <param name="g"></param>
        override public void draw(Graphics g)
        {
            g.DrawLine(new Pen(colour), xPos, yPos, toX, toY);
        }
    }
}
