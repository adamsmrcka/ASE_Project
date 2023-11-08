using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace ASE_Project
{
    /// <summary>
    /// The Canvas class acts as a controller for drawing onto a Graphics context.
    /// </summary>
    public class Canvas
    {
        private Form1 form1;

        public static int defaultPosX = 10;
        public static int defaultPosY = 10;
        private static int defaultPenWidth = 1;
        private static bool defaultFill = false;
        private static Color defaultPenColour = Color.Black;

        Graphics g;
        Pen pen;

        public static int posX = defaultPosX;
        public static int posY = defaultPosY;
        public static int oldPosX = posX;
        public static int oldPosY = posY;
        public static Color penColour = defaultPenColour;
        public static bool fill = defaultFill;

        /// <summary>
        /// Constructor for Canvas. Sets default color and default pen.
        /// </summary>
        public Canvas(Graphics g, Form1 form1)
        {
            this.g = g;
            pen = new Pen(penColour, defaultPenWidth);
            this.form1 = form1;
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
            idicateCursor();
        }
        /// <summary>
        /// Resets the drawing (Canvas) settings to the default settings (cursor position, colour and fill settings)
        /// </summary>
        public void restoreCanvas()
        {
            oldPosX = posX;
            oldPosY = posY;
            posX = defaultPosX;
            posY = defaultPosY;
            penColour = defaultPenColour;
            pen = new Pen(penColour);
            fill = defaultFill;
            idicateCursor();
        }
        /// <summary>
        /// Cleans Canvas - removes all drawings
        /// </summary>
        public void clearCanvas()
        {
            this.g.Clear(Color.White);
            reidicateCursor();
        }
        /// <summary>
        /// Changes the position of the "cursor" for the drawing process
        /// </summary>
        /// <param name="moveTo">Array of X and Y values</param>
        public void moveTo(int[] moveTo)
        {
            oldPosX = posX;
            oldPosY = posY;
            posX = moveTo[0];
            posY = moveTo[1];
            idicateCursor();
            form1.updateCursorPositionLabel(posX, posY);
        }
        /// <summary>
        /// Changes the pen colour for the drawing process
        /// </summary>
        /// <param name="newcolour">User selected new colour</param>
        public void changeColor(Color newColour)
        {
            penColour = newColour;
            pen = new Pen(penColour);
            form1.updatePenColourStatusLabel(penColour);
        }
        /// <summary>
        /// Toggles between fill and outline methods for the drawing process
        /// </summary>
        public void toggleFill(string fillRequest)
        {
            if (fillRequest == "off")
            {
                fill = false;
            }
            else if (fillRequest == "on")
            {
                fill = true;
            }
            else
            {
                throw new Exception($"Error: Incorect Fill parameter '{fillRequest}', Expeted On or Off");
            }
            form1.updateFillStatusLabel(fill);
        }
        /// <summary>
        /// Draws a small red circle on the cursor position, on position change the circle moves
        /// </summary>
        public void idicateCursor()
        {
            int diameter = 2;
            Color pixelColor1 = form1.canvasBitmap.GetPixel(oldPosX, oldPosY);
            Color pixelColor2 = form1.canvasBitmap.GetPixel(oldPosX + 2, oldPosY);
            if (pixelColor1 == Color.FromArgb(0, 0, 0, 0) || pixelColor1 == Color.White || pixelColor2 == Color.FromArgb(0, 0, 0, 0) || pixelColor2 == Color.White)
            {
                g.DrawEllipse(new Pen(Color.White), oldPosX - (diameter / 2), oldPosY - (diameter / 2), diameter, diameter);

            }
            else 
            {
                g.DrawEllipse(new Pen(pixelColor1), oldPosX - (diameter / 2), oldPosY - (diameter / 2), diameter, diameter);
            }
            g.DrawEllipse(new Pen(Color.Red), posX - (diameter / 2), posY - (diameter / 2), diameter, diameter);
        }
        /// <summary>
        /// indicates coursor position after clearing
        /// </summary>
        public void reidicateCursor()
        {
            int diameter = 2;
            g.DrawEllipse(new Pen(Color.Red), posX - (diameter / 2), posY - (diameter / 2), diameter, diameter);

        }


    }
}
