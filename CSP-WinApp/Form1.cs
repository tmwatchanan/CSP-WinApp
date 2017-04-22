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
            for (int gen = 0; gen < GA.MAX_GENERATION; gen++)
            {
                Population firstHalf = GA.GetFirstHalfPopulation(population, GA.ELITISM_SIZE);
                Population secondHalf = GA.GetSecondHalfPopulation(population, GA.ELITISM_SIZE);
                GA.Crossover(population, GA.GENE_SIZE);
                GA.Mutation(population);
            }
        }

        private void InitializePopulation()
        {
            for (int i = 0; i < GA.POPULATION_SIZE; i++)
            {
                Chromosome chromosome = new Chromosome();
                foreach (var part in parts)
                {
                    chromosome.AddGene(part.X, part.Y, part.Orientation, part.Width, part.Length);
                }
                GA.GENE_SIZE = chromosome.GetSize();
                chromosome.RandomAllGenes(materialWidth, materialLength);
                chromosome.CalculateFitness(materialWidth, materialLength);
                population.AddChromosome(chromosome);
            }
            population.SortByFitness();
        }
    }
}
