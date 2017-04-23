using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_WinApp
{
    public class Gene
    {
        int x;
        int y;
        int orientation;
        int width;
        int length;
        
        public int X { get; set; }
        public int Y { get; set; }
        public int Orientation { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }

        public Gene()
        {
            this.X = 0;
            this.Y = 0;
            this.Orientation = 0;
            this.Width = 0;
            this.Length = 0;
        }

        public Gene(int x, int y, int orientation, int width, int length)
        {
            this.X = x;
            this.Y = y;
            this.Orientation = orientation;
            this.Width = width;
            this.Length = length;
        }
    }
}
