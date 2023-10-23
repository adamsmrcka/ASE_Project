using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Project
{
    public abstract class Shape
    {
        public static int xPos, yPos;
        public static Color colourShape;
        protected Graphics g;
        public static bool fillShape;
        public Shape()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="colour">Pen colour</param>
        /// <param name="fill">Boolean fill or outline</param>
        /// <param name="posX">Starting x position</param>
        /// <param name="posY">Starting y position</param>
        /// <param name="parameters">Array of entered parameters for the command</param>
        abstract public void set(Color colour, bool fill, int posX, int posY, params int[] parameters);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        abstract public void draw(Graphics g);

    }
}
