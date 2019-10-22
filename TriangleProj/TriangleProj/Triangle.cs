using System;
using System.Collections.Generic;
using System.Text;

namespace TriangleProj
{
    public class Triangle
    {
        public double A { get; private set; }
        public double B { get; private set; }
        public double C { get; private set; }

        private Triangle() { }

        public static bool CheckIsPossibleToCreate(double a, double b, double c)
        {
            return (a > 0 && b > 0 && c > 0 && a + b > c && b + c > a && a + c > b);
        }
    }
}
