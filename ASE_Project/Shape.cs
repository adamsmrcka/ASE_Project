using System.Drawing;

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
        /// Sets a default set shape method
        /// </summary>
        /// <param name="colour">Pen colour</param>
        /// <param name="fill">Boolean fill or outline</param>
        /// <param name="posX">Starting x position</param>
        /// <param name="posY">Starting y position</param>
        /// <param name="parameters">Array of entered parameters for the command</param>
        abstract public void set(Color colour, bool fill, int posX, int posY, params int[] parameters);
        /// <summary>
        /// Sets a default draw shape method
        /// </summary>
        /// <param name="g">Graphics context for the drawing</param>
        abstract public void draw(Graphics g);

    }
}
