using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Project
{
    public class Circle : Shape
    {
        public static int circleSize;

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
            colourShape = colour;
            xPos = posX;
            yPos = posY;
            fillShape = fill;
            circleSize = parameters[0];
        }

        /// <summary>
        /// Draws the Circle either filled or just an outline
        /// </summary>
        /// <param name="g"></param>
        override public void draw(Graphics g)
        {
            if (!fillShape)
            {
                g.DrawEllipse(new Pen(colourShape), xPos - (circleSize / 2), yPos - (circleSize / 2), circleSize, circleSize);
            }
            else
            {
                g.FillEllipse(new SolidBrush(colourShape), xPos - (circleSize / 2), yPos - (circleSize / 2), circleSize, circleSize);
            }
        }
    }
}

