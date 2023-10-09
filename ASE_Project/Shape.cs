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
        protected int xPos, yPos;
        protected Color colour;
        protected Graphics g;
        protected bool fillShape;
        /// <summary>
        /// 
        /// </summary>
        public Shape()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="fill"></param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="parameters"></param>
        abstract public void set(Color colour, bool fill, int posX, int posY, params int[] parameters);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        abstract public void draw(Graphics g);

    }
}
