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

        int materialWidth;
        int materialLength;

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
            PerformGA();
        }

        private void GetMaterialSizeFromForm()
        {
            try
            {
                materialWidth = Convert.ToInt32(TextBoxMaterialWidth.Text);
            }
            catch (Exception)
            {
                materialWidth = 0;
            }
            try
            {
                materialLength = Convert.ToInt32(TextBoxMaterialLength.Text);
            }
            catch (Exception)
            {
                materialLength = 0;
            }
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

        private void PerformGA()
        {
            GetMaterialSizeFromForm();
            GetFromDataGridView();
            parts = CreateRectangles(inputList);

            InitializePopulation();
            GA.Crossover(population);
        }

        private void InitializePopulation()
        {
            labelPopulation.Text = "[GEN1]";
            foreach (var part in parts)
            {
                Individual individual = new Individual();
                individual.X = part.X;
                individual.Y = part.Y;
                individual.Orientation = part.Orientation;
                individual.Width = part.Width;
                individual.Length = part.Length;
                population.AddIndividual(individual);
            }
            population.RandomAllIndividuals(materialWidth, materialLength);
            population.CalculateFitness(materialWidth, materialLength);
            population.SortByFitness();
            foreach (var individual in population.Individuals)
            {
                labelPopulation.Text += individual.X + "," + individual.Y + " | ";
            }
        }
    }
}
