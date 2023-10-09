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
        private static Color defaultPenColour = Color.Black;

        Graphics g;
        Pen pen;

        public static int posX = defaultPosX;
        public static int posY = defaultPosY;
        public static Color penColour = defaultPenColour;

        /// <summary>
        /// Constructor for Canvas. Sets default color and default pen.
        /// </summary>
        public Canvas(Graphics g)
        {
            this.g = g;
            pen = new Pen(penColour, defaultPenWidth);
        }
        public Graphics GetGraphics() { return g; }
        public void DrawShape(Shape shape, Color colour, int posX, int posY, params int[] parameters)
        {
            shape.Set(colour, posX, posY, parameters);
            shape.Draw(this.g);
        }
        public void RestoreCanvas()
        {
            posX = defaultPosX;
            posY = defaultPosY;
        }
        public void ClearCanvas()
        {
            this.g.Clear(Color.White);
        }
        public void MoveTo(int[] moveTo)
        {
            posX = moveTo[0]; 
            posY = moveTo[1];
        }
        public void ChangeColor(Color newcolour)
        {
            penColour = newcolour;
            pen = new Pen(penColour);
        }
    }
}
