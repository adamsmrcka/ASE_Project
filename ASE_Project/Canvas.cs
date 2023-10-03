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
        public static int DefaultPosX = 0;
        public static int DefaultPosY = 0;
        const int DefaultPenWidth = 1;

        Graphics g;
        Pen pen = new Pen(Color.Black, DefaultPenWidth);
        

        /// <summary>
        /// Constructor for Canvas. Sets default color and default pen.
        /// </summary>
        public Canvas(Graphics g)
        {
            this.g = g;
        }
        public Graphics GetGraphics() { return g; }

        public void DrawLine(int toX, int toY)
        {
            int posX = DefaultPosX;
            int posY = DefaultPosY;
            g.DrawLine(pen, posX, posY, toX, toY);
            posX = toX;
            posY = toY;
        }
    }
}
