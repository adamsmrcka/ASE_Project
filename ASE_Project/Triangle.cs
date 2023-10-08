﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ASE_Project
{
    internal class Triangle : Shape
    {
        private System.Drawing.Point[] trianglePoints = new System.Drawing.Point[3];
        public Triangle() { }

        public override void Set(Color colour, int posX, int posY, params int[] parameters)
        {
            this.colour = colour;
            trianglePoints[0] = new System.Drawing.Point(posX, posY);
            trianglePoints[1] = new System.Drawing.Point(parameters[0], parameters[1]);
            trianglePoints[2] = new System.Drawing.Point(parameters[2], parameters[3]);

        }
        public override void Draw(Graphics g)
        {
            g.DrawPolygon(new Pen(colour), trianglePoints);
        }


    }
}