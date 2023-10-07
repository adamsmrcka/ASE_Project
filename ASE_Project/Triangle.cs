using System;
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
        private System.Drawing.Point[] trianglePoints;
        public Triangle() { }

        public override void Set(Color colour, params int[] parameters)
        {
            throw new NotImplementedException();
        }
        public override void SetRectangle(Color colour, System.Drawing.Point[] points)
        {
            this.colour = colour;
            trianglePoints = points;
        }
        public override void Draw(Graphics g)
        {
            g.DrawPolygon(new Pen(colour), trianglePoints);
        }


    }
}
