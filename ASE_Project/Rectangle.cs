using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Project
{
    public class Rectangle : Shape
    {
        public static int height, width;
        public Rectangle() { }
        /// <summary>
        /// Sets the properties of the Rectangle and prepares it for drawing
        /// </summary>
        /// <param name="colour"> Colour of the pen</param>
        /// <param name="fill"> Boolean if fill or just draw</param>
        /// <param name="posX"> X position of the rectangle centre</param>
        /// <param name="posY"> Y position of the rectangle centre</param>
        /// <param name="parameters"> Array of parameters (width and height)</param>
        public override void set(Color colour, bool fill, int posX, int posY, params int[] parameters)
        {
            colourShape = colour;
            xPos = posX;
            yPos = posY;
            fillShape = fill;
            width = parameters[0];
            height = parameters[1];
        }
        /// <summary>
        /// Draws the Rectangle either filled or just an outline
        /// </summary>
        /// <param name="g">Graphics context for the drawing</param>
        public override void draw(Graphics g)
        {
            if (!fillShape)
            {
                g.DrawRectangle(new Pen(colourShape), new System.Drawing.Rectangle(xPos - (width / 2), yPos - (height / 2), width, height));
            }
            else
            {
                g.FillRectangle(new SolidBrush(colourShape), new System.Drawing.Rectangle(xPos - (width / 2), yPos - (height / 2), width, height));
            }
        }
    }
}
