using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSP_WinApp
{
    public partial class DisplayForm : Form
    {
        public const int SCALING_FACTOR = 10;
        public const int RESERVE_BORDER = 50;
        public DisplayForm()
        {
            InitializeComponent();
        }

        public void DrawMaterial(int width, int height)
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(RESERVE_BORDER / 2, RESERVE_BORDER / 2, width, height);
            //graphics.DrawRectangle(System.Drawing.Pens.Red, rectangle);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            graphics.FillRectangle(blackBrush, rectangle);
            blackBrush.Dispose();
            graphics.Dispose();
        }
    }
}
