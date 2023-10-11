using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ASE_Project
{
    public class Triangle : Shape
    {
        public static System.Drawing.Point[] trianglePoints = new System.Drawing.Point[3];
        public Triangle() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="colour"> Colour of the pen</param>
        /// <param name="fill"> Boolean if fill or just draw</param>
        /// <param name="posX"> X position of the first triangle point</param>
        /// <param name="posY"> Y position of the first triangle point</param>
        /// <param name="parameters"> Array of parameters (X and Y values of the other 2 triangle points)</param>
        public override void set(Color colour, bool fill, int posX, int posY, params int[] parameters)
        {
            colourShape = colour;
            fillShape = fill;
            trianglePoints[0] = new System.Drawing.Point(posX, posY);
            trianglePoints[1] = new System.Drawing.Point(parameters[0], parameters[1]);
            trianglePoints[2] = new System.Drawing.Point(parameters[2], parameters[3]);

        }
        /// <summary>
        /// Draws a Triangle either filled or just an outline
        /// </summary>
        /// <param name="g"></param>
        public override void draw(Graphics g)
        {
            if (!fillShape)
            {
                g.DrawPolygon(new Pen(colourShape), trianglePoints);
            }
            else
            {
                g.FillPolygon(new SolidBrush(colourShape), trianglePoints);
            }
        }


    }
}
