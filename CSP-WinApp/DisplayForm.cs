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

            Bitmap img = new Bitmap(SCALING_FACTOR * width, SCALING_FACTOR * height);
            pictureBox1.Image = img;
            pictureBox1.Size = pictureBox1.Image.Size;
        }

        //public void DrawMaterial(int width, int height)
        //{
        //    System.Drawing.Graphics graphics = this.CreateGraphics();
        //    SolidBrush blackBrush = new SolidBrush(Color.Black);
        //    //System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(RESERVE_BORDER / 2, RESERVE_BORDER / 2, width, height);
        //    System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(0, 0, width, height);
        //    //graphics.DrawRectangle(System.Drawing.Pens.Red, rectangle);
        //    graphics.FillRectangle(blackBrush, rectangle);
        //    blackBrush.Dispose();
        //    graphics.Dispose();
        //}

        private SolidBrush RandomBrushColor()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            int red = r.Next(0, byte.MaxValue + 1);
            int green = r.Next(0, byte.MaxValue + 1);
            int blue = r.Next(0, byte.MaxValue + 1);
            return new System.Drawing.SolidBrush(Color.FromArgb(red, green, blue));
        }

        public void DrawParts(Population population, int chromosomeIdx)
        {
            Chromosome chromosome = population.Chromosomes[chromosomeIdx];
            for (int g = 0; g < chromosome.Genes.Count; g++)
            {
                Gene gene = chromosome.Genes[g];
                Random r = new Random(Guid.NewGuid().GetHashCode());
                int red = r.Next(0, byte.MaxValue + 1);
                int green = r.Next(0, byte.MaxValue + 1);
                int blue = r.Next(0, byte.MaxValue + 1);
                Color color = Color.FromArgb(red, green, blue);
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
                //brush = RandomBrushColor();
                //System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(DisplayForm.SCALING_FACTOR * gene.X, DisplayForm.SCALING_FACTOR * gene.Y, DisplayForm.SCALING_FACTOR * gene.Width, DisplayForm.SCALING_FACTOR * gene.Length);
                //graphics.FillRectangle(brush, rectangle);
                //using (Font font1 = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point))
                //{
                //    graphics.DrawString(Convert.ToString(g), font1, Brushes.OrangeRed, rectangle);
                //}
            }
        }
    }
}
