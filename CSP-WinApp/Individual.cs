using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_WinApp
{
    class Individual
    {
        int fitness;
        int x;
        int y;
        int orientation;
        int width;
        int length;

        public int Fitness { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Orientation { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }

        public Individual()
        {
            this.Fitness = 0;
            this.X = 0;
            this.Y = 0;
            this.Orientation = 0;
            this.Width = 0;
            this.Length = 0;
        }

        public void CalculateItsFitness(int width, int length)
        {
            int boundPoint = this.X + this.Width + this.Length;
            Boolean outOfBound = (boundPoint > width | boundPoint > length);
            this.Fitness += (outOfBound ? 123456789 : 0);
        }

        public static int CalculateFitnessWithOther(Individual ind1, Individual ind2)
        {
            System.Drawing.Rectangle drawRect1 = new System.Drawing.Rectangle(ind1.X, ind1.Y, ind1.Width, ind1.Length);
            System.Drawing.Rectangle drawRect2 = new System.Drawing.Rectangle(ind2.X, ind2.Y, ind2.Width, ind2.Length);
            System.Drawing.Rectangle intersectArea = System.Drawing.Rectangle.Intersect(drawRect1, drawRect2);
            return intersectArea.Width * intersectArea.Height;
        }
    }
}
