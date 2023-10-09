using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ASE_Project
{
    /// <summary>
    /// The Canvas class acts as a controller for drawing onto a Graphics context.
    /// </summary>
    public class Canvas
    {
        private static int defaultPosX = 10;
        private static int defaultPosY = 10;
        private static int defaultPenWidth = 1;
        private static bool defaultFill = false;
        private static Color defaultPenColour = Color.Black;

        Graphics g;
        Pen pen;

        public static int posX = defaultPosX;
        public static int posY = defaultPosY;
        public static Color penColour = defaultPenColour;
        public static bool fill = defaultFill;

        /// <summary>
        /// Constructor for Canvas. Sets default color and default pen.
        /// </summary>
        public Canvas(Graphics g)
        {
            this.g = g;
            pen = new Pen(penColour, defaultPenWidth);
        }
        public Graphics getGraphics() { return g; }
        /// <summary>
        /// Sends instructions (variables) to Set and Draw the identified shape
        /// </summary>
        /// <param name="shape">The shape identified to be drawn</param>
        /// <param name="colour">The colour selected for the drawing</param>
        /// <param name="fill">Toggle between fill and ouline settings</param>
        /// <param name="posX">The x value of the starting position</param>
        /// <param name="posY">The y value of the starting position</param>
        /// <param name="parameters">Parameters required for the shape to be drawn (ie. height, diametr, end position, etc.)</param>
        public void drawShape(Shape shape, Color colour, bool fill, int posX, int posY, params int[] parameters)
        {
            shape.set(colour, fill, posX, posY, parameters);
            shape.draw(this.g);
        }
        /// <summary>
        /// Resets the drawing(Canvas) settings to the default settings (cursor position, colour and fill settings)
        /// </summary>
        public void restoreCanvas()
        {
            posX = defaultPosX;
            posY = defaultPosY;
            penColour = defaultPenColour;
            pen = new Pen(penColour);
            fill = defaultFill;
        }
        /// <summary>
        /// Cleans Canvas - removes all drawings
        /// </summary>
        public void clearCanvas()
        {
            this.g.Clear(Color.White);
        }
        /// <summary>
        /// Changes the position of the "cursor" for the drawing process
        /// </summary>
        /// <param name="moveTo">Array of X and Y values</param>
        public void moveTo(int[] moveTo)
        {
            posX = moveTo[0];
            posY = moveTo[1];
        }
        /// <summary>
        /// Changes the pen colour for the drawing process
        /// </summary>
        /// <param name="newcolour">User selected new colour</param>
        public void changeColor(Color newcolour)
        {
            penColour = newcolour;
            pen = new Pen(penColour);
        }
        /// <summary>
        /// Toggles between fill and outline methods for the drawing process
        /// </summary>
        public void toggleFill()
        {
            if (fill)
            {
                fill = false;
            }
            else
            {
                fill = true;
            }
        }
    }
}
