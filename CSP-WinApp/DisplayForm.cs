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

        public DisplayForm(int width, int height)
        {
            InitializeComponent();
        }

        private void InitializeSpace(int width, int height)
        {
            pictureBox1.InitialImage = null;
            //pictureBox1.Image = null;
            pictureBox1.Invalidate();
            Bitmap img = new Bitmap(SCALING_FACTOR * width, SCALING_FACTOR * height);
            pictureBox1.Image = img;
            pictureBox1.Size = img.Size;
        }

        private Color RandomColor()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            int red = r.Next(0, byte.MaxValue + 1);
            int green = r.Next(0, byte.MaxValue + 1);
            int blue = r.Next(0, byte.MaxValue + 1);
            Color randomColor = Color.FromArgb(red, green, blue);
            return randomColor;
        }

        public void DrawParts(Population population, int chromosomeIdx)
        {
            InitializeSpace(Form1.materialWidth, Form1.materialLength);
            Chromosome chromosome = population.Chromosomes[chromosomeIdx];
            for (int g = 0; g < chromosome.Genes.Count; g++)
            {
                Gene gene = chromosome.Genes[g];
                Color color = RandomColor();
                using (var graphic = Graphics.FromImage(pictureBox1.Image))
                using (Font font1 = new Font("Arial", 7, FontStyle.Bold, GraphicsUnit.Point)) // dynamic font size: ((int)(SCALING_FACTOR * gene.Width / 5)), but still not perfectly works
                {
                    System.Drawing.Rectangle partRectangle = new System.Drawing.Rectangle(SCALING_FACTOR * gene.X, SCALING_FACTOR * gene.Y, SCALING_FACTOR * gene.Width, SCALING_FACTOR * gene.Length);
                    graphic.DrawRectangle(new Pen(color), partRectangle);
                    graphic.FillRectangle(new SolidBrush(color), partRectangle);
                    string rectangleString = "Chromosome#" + chromosomeIdx;
                    rectangleString += "\n" + " Gene@" + Convert.ToString(g);
                    rectangleString += "\n" + "origin: (" + gene.X + "," + gene.Y + ")";
                    rectangleString += "\n" + "size: [" + gene.Width + "," + gene.Length + "]";
                    graphic.DrawString(rectangleString, font1, new SolidBrush(Color.White), partRectangle);
                    pictureBox1.Refresh();
                }
            }
        }
    }
}
