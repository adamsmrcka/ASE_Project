﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace ASE_Project
{
    public class CommandFactory
    {
        public static CommandFactory commandFactory = new CommandFactory();

        public CommandFactory() { }
        /// <summary>
        /// Returns the CommandFactory instance.
        /// </summary>
        /// <returns>CommandFactory</returns>
        public static CommandFactory GetShapeFactory() { return commandFactory; }

        /// <summary>
        /// Takes in a String containing the name of a Shape or Command and returns the requested Object, if it exists.
        /// </summary>
        /// <param name="name">Name of Shape or Command.</param>
        /// <returns>Command object.</returns>
        public Shape GetShape(string name)
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
                default:
                    throw new Exception ("Variable does not exist or Unknown second parameter");

            }
        }
    }
}