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
    internal class Canvas
    {
        const int DefaultPosX = 0;
        const int DefaultPosY = 0;
        const int DefaultPenWidth = 1;

        Graphics g;
        Pen pen = new Pen(Color.Black, DefaultPenWidth);
        int posX = DefaultPosX;
        int posY = DefaultPosY;

        /// <summary>
        /// Constructor for Canvas. Sets default color and default pen.
        /// </summary>
        public Canvas(Graphics g)
        {
            this.g = g;
        }
        public void DrawLine(int toX, int toY)
        {
            this.g.DrawLine(pen, posX, posY, toX, toY);
            this.posX = toX;
            this.posY = toY;
        }
    }
}
