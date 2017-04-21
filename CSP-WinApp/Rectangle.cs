using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_WinApp
{
    class Rectangle
    {
        private int x;
        private int y;
        private int width;
        private int length;
        private int orientation; // 0 (Horizontal), 1 (Vertical)
        
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public int Orientation { get; set; }

        public Rectangle()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Length = 0;
            Orientation = 0;
        }

        public Rectangle(int x, int y, int width, int length, int orientation)
        {
            X = x;
            Y = y;
            Width = width;
            Length = length;
            Orientation = orientation;
        }

        public int CalculateFitness(Rectangle rect)
        {
            System.Drawing.Rectangle drawRect1 = new System.Drawing.Rectangle(this.X, this.Y, this.Width, this.Length);
            System.Drawing.Rectangle drawRect2 = new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Length);
            System.Drawing.Rectangle intersectArea = System.Drawing.Rectangle.Intersect(drawRect1, drawRect2);
            return intersectArea.Width * intersectArea.Height;
        }
    }
}
