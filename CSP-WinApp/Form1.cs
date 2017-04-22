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
    public partial class Form1 : Form
    {
        const int POPULATION_SIZE = 10;
        int individualSize;

        List<Coordinate> inputList;
        List<Rectangle> parts;
        Population population;

        public Form1()
        {
            InitializeComponent();
            InitializeModel();
        }

        public void InitializeModel()
        {
            individualSize = 0;
            inputList = new List<Coordinate>();
            parts = new List<Rectangle>();
            population = new Population();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            GetFromDataGridView();
            parts = CreateRectangles(inputList);
            InitializePopulation();
        }

        private void GetFromDataGridView()
        {
            for (int rows = 0; rows < dataGridViewParts.Rows.Count - 1; rows++)
            {
                Coordinate coordinate = new Coordinate();
                coordinate.Width = Convert.ToInt32(dataGridViewParts.Rows[rows].Cells[0].Value.ToString());
                coordinate.Length = Convert.ToInt32(dataGridViewParts.Rows[rows].Cells[1].Value.ToString());
                coordinate.Count = Convert.ToInt32(dataGridViewParts.Rows[rows].Cells[2].Value.ToString());
                inputList.Add(coordinate);
                //for (int col = 0; col < dataGridViewParts.Rows[rows].Cells.Count; col++)
                //{
                //    string value = dataGridViewParts.Rows[rows].Cells[col].Value.ToString();
                //}
            }
        }
        
        public static List<Rectangle> CreateRectangles(List<Coordinate> coordinates)
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            foreach (var part in coordinates)
            {
                for (int i = 0; i < part.Count; i++)
                {
                    Rectangle rectangle = new Rectangle(0, 0, part.Width, part.Length, 0);
                    rectangles.Add(rectangle);
                }
            }
            return rectangles;
        }

        private void InitializePopulation()
        {
            foreach (var part in parts)
            {
                Individual individual = new Individual();
                individual.Chromosome.Add(part.X);
                individual.Chromosome.Add(part.Y);
                individual.Chromosome.Add(part.Orientation);
                population.AddIndividual(individual);
            }
            //individualSize = population.Count * 3;
        }

        //private int CalculateFitness(Rectangle rect1, Rectangle rect2)
        //{
        //    System.Drawing.Rectangle drawRect1 = new System.Drawing.Rectangle(rect1.X, rect1.Y, rect1.Width, rect1.Length);
        //    System.Drawing.Rectangle drawRect2 = new System.Drawing.Rectangle(rect2.X, rect2.Y, rect2.Width, rect2.Length);
        //    System.Drawing.Rectangle intersectArea = System.Drawing.Rectangle.Intersect(drawRect1, drawRect2);
        //    return intersectArea.Width * intersectArea.Height;
        //}
    }
}
