using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Project
{
    internal class Polygon : Shape
    {
        public static List<Point> polygonPointsList = new List<Point>();
        public Polygon() { }
        /// <summary>
        /// Sets the properties of the Polygon and prepares it for drawing
        /// </summary>
        /// <param name="colour"> Colour of the pen</param>
        /// <param name="fill"> Boolean if fill or just draw</param>
        /// <param name="posX"> X position of the first polygon point</param>
        /// <param name="posY"> Y position of the first polygon point</param>
        /// <param name="parameters"> Array of parameters (X and Y values of the other polygon points)</param>
        public override void set(Color colour, bool fill, int posX, int posY, params int[] parameters)
        {
            colourShape = colour;
            fillShape = fill;
            polygonPointsList.Add(new Point(posX, posY));
            for (int i = 0; i < parameters.Length; i  = i + 2)
            {
                polygonPointsList.Add(new Point(parameters[i], parameters[i + 1]));
            }

        }
        /// <summary>
        /// Draws a Polygon either filled or just an outline
        /// </summary>
        /// <param name="g">Graphics context for the drawing</param>
        public override void draw(Graphics g)
        {

            if (!fillShape)
            {
                g.DrawPolygon(new Pen(colourShape), polygonPointsList.ToArray());
            }
            else
            {
                g.FillPolygon(new SolidBrush(colourShape), polygonPointsList.ToArray());
            }
        }
    }
}
