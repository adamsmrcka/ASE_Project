using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Project
{
    public class Line : Shape
    {
        public static int toX, toY;

        public Line() { }
        /// <summary>
        /// Sets the properties of the DrawLine and prepares it for drawing
        /// </summary>
        /// <param name="colour"> Colour of the pen</param>
        /// <param name="fill"> Boolean if fill or just draw</param>
        /// <param name="posX"> X position of the start point</param>
        /// <param name="posY"> Y position of the start point</param>
        /// <param name="parameters"> Array of parameters (X and Y values of the end points)</param>
        override public void set(Color colour, bool fill, int posX, int posY, params int[] parameters)
        {
            colourShape = colour;
            xPos = posX;
            yPos = posY;
            fillShape = fill;
            toX = parameters[0];
            toY = parameters[1];
        }
        /// <summary>
        /// Draws the line
        /// </summary>
        /// <param name="g">Graphics context for the drawing</param>
        override public void draw(Graphics g)
        {
            g.DrawLine(new Pen(colourShape), xPos, yPos, toX, toY);
        }
    }
}
