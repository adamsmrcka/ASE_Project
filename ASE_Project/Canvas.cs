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
        public void drawShape(Shape shape, Color colour, bool fill, int posX, int posY, params int[] parameters)
        {
            shape.set(colour, fill, posX, posY, parameters);
            shape.draw(this.g);
        }
        public void restoreCanvas()
        {
            posX = defaultPosX;
            posY = defaultPosY;
            penColour = defaultPenColour;
            pen = new Pen(penColour);
            fill = defaultFill;
        }
        public void clearCanvas() 
        {
            this.g.Clear(Color.White);
        }
        public void moveTo(int[] moveTo)
        {
            posX = moveTo[0]; 
            posY = moveTo[1];
        }
        public void changeColor(Color newcolour)
        {
            penColour = newcolour;
            pen = new Pen(penColour);
        }
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
