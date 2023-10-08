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
        private static int DefaultPosX = 10;
        private static int DefaultPosY = 10;
        const int DefaultPenWidth = 1;

        Graphics g;
        Pen pen = new Pen(Color.Black, DefaultPenWidth);

        public static int posX = DefaultPosX;
        public static int posY = DefaultPosY;

        /// <summary>
        /// Constructor for Canvas. Sets default color and default pen.
        /// </summary>
        public Canvas(Graphics g)
        {
            this.g = g;
        }
        public Graphics GetGraphics() { return g; }
        public void DrawShape(Shape shape, Color colour, int posX, int posY, params int[] parameters)
        {
            shape.Set(colour, posX, posY, parameters);
            shape.Draw(this.g);
        }
        public void RestoreCanvas()
        {
            posX = DefaultPosX;
            posY = DefaultPosY;
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
    }
}
