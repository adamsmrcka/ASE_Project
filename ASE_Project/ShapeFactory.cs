using System;

namespace ASE_Project
{
    /// <summary>
    /// Shape Factory
    /// Used as a sigle point of creation for shape commands.
    /// </summary>
    public class ShapeFactory
    {
        public static ShapeFactory commandFactory = new ShapeFactory();

        public ShapeFactory() { }
        /// <summary>
        /// Returns the CommandFactory instance
        /// </summary>
        /// <returns>CommandFactory</returns>
        public static ShapeFactory getShapeFactory() { return commandFactory; }

        /// <summary>
        /// Takes in a String containing the name of a Shape or Command and returns the requested Object, if it exists
        /// </summary>
        /// <param name="name">Name of Shape or Command</param>
        /// <returns>Command object</returns>
        public Shape getShape(string name)
        {
            switch (name)
            {
                case "circle":
                    return new Circle();
                case "triangle":
                    return new Triangle();
                case "rectangle":
                    return new Rectangle();
                case "drawto":
                    return new Line();
                case "polygon":
                    return new Polygon();
                default:
                    throw new Exception("Shape does not exist or unknown command");

            }
        }
    }
}
