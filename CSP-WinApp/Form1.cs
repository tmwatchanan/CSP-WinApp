﻿using System;
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
        public static int materialWidth;
        public static int materialLength;
        public static int materialLongest;

        List<Coordinate> inputList;
        List<Rectangle> parts;
        Population population;

        DisplayForm displayForm;

        public Form1()
        {
            InitializeComponent();
            InitializeModel();
            buttonNextGen.Enabled = false;
            buttonAuto.Enabled = false;
        }

        public void InitializeModel()
        {
            inputList = new List<Coordinate>();
            parts = new List<Rectangle>();
            population = new Population();
            labelGenerationNumber.Text = "0";
        }

        private void InitializeDisplayForm()
        {
            displayForm = new DisplayForm(materialWidth, materialLength, GA.GENE_SIZE);
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            PerformGA();
            displayForm.Width = (DisplayForm.SCALING_FACTOR * materialWidth + 16);// + DisplayForm.RESERVE_BORDER;
            displayForm.Height = (DisplayForm.SCALING_FACTOR * materialLength + 38);// + DisplayForm.RESERVE_BORDER;
            displayForm.Show();
            buttonSubmit.Enabled = false;
            buttonNextGen.Enabled = true;
            buttonAuto.Enabled = true;
            //displayForm.DrawMaterial((DisplayForm.SCALING_FACTOR * materialWidth), (DisplayForm.SCALING_FACTOR * materialLength));
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
            materialLongest = Math.Max(materialWidth, materialLength);
            string binary = Convert.ToString(materialLongest, 2);
            GA.BINARY_SIZE = binary.Length;
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

        private void ShowPopulationInForm()
        {
            dataGridViewPopulation.DataSource = null;
            dataGridViewPopulation.Rows.Clear();

            dataGridViewPopulation.Refresh();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("GEN#");
            dataTable.Columns.Add("Chromosome");

            DataRow dataRow;
            foreach (var chromosome in population.Chromosomes)
            {
                dataRow = dataTable.NewRow();
                dataRow[0] = GA.LAST_GENERATION.ToString();
                string chromosomeString = "{" + chromosome.Fitness + "}";
                for (int i = 0; i < chromosome.Genes.Count; i++)
                {
                    chromosomeString += "(" + chromosome.Genes[i].X + "," + chromosome.Genes[i].Y + ")[" + chromosome.Genes[i].Orientation + "]<" + chromosome.Genes[i].Width + "x" + chromosome.Genes[i].Length + ">";
                    chromosomeString += " | ";
                }
                dataRow[1] = chromosomeString;
                dataTable.Rows.Add(dataRow);
            }
            dataGridViewPopulation.DataSource = dataTable;
        }

        private void PerformGA()
        {
            GetMaterialSizeFromForm();
            GetFromDataGridView();
            parts = CreateRectangles(inputList);

            InitializePopulation();
            InitializeDisplayForm();
            labelGenerationNumber.Text = Convert.ToString(GA.LAST_GENERATION);
            ShowPopulationInForm();
            displayForm.DrawParts(population, 0); // 0 for the best
        }

        private void GAEvolve()
        {
            GA.LAST_GENERATION += 1;
            //Population firstHalf = GA.GetFirstHalfPopulation(population, GA.ELITISM_SIZE); // VIPs (elites), do not touch
            //Population secondHalf = GA.GetSecondHalfPopulation(population, GA.ELITISM_SIZE); // others needs reproduction
            Population parentPopulation = new Population(population);
            Population offspringPopulation = GA.BinaryTournament(parentPopulation);
            //Population newPopulation = new Population();
            Population combinedPopulation = GA.CombineFirstAndSecondPopulation(parentPopulation, offspringPopulation);
            //newPopulation.Chromosomes = combinedPopulation.Chromosomes;
            Population newPopulation = new Population(combinedPopulation);
            newPopulation.CalculateFitnessOfAllChromosomes();
            newPopulation.SortByFitness();
            //newPopulation.CutInHalf();
            //population.Chromosomes = newPopulation.Chromosomes.Take(newPopulation.Chromosomes.Count / 2).ToList();
            population = new Population(newPopulation.GetFirstHalfPopulation());
            population.CalculateFitnessOfAllChromosomes();
            population.SortByFitness();
            //population = newPopulation;
            if (population.FindMinFitness() < GA.MIN_FITNESS)
            {
                GA.MIN_FITNESS = population.FindMinFitness();
                GA.StillMinCount = 0;
            }
            else if (population.FindMinFitness() == GA.MIN_FITNESS)
            {
                ++GA.StillMinCount;
            }
            //else
            //{
            //    GA.StillMinCount = 0;
            //}
            labelGenerationNumber.Text = Convert.ToString(GA.LAST_GENERATION);
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
                population.AddChromosome(chromosome);
            }
            population.CalculateFitnessOfAllChromosomes();
            population.SortByFitness();
        }

        private void buttonNextGen_Click(object sender, EventArgs e)
        {
            GAEvolve();
            ShowPopulationInForm();
            displayForm.DrawParts(population, 0); // 0 for the best
            if (GA.LAST_GENERATION > GA.MAX_GENERATION)
            {
                buttonNextGen.Enabled = false;
            }
        }

        private void buttonAuto_Click(object sender, EventArgs e)
        {
            //while (true)
            //{
            //    if (population.Chromosomes[0].Fitness == 0) break;
            //    GAEvolve();
            //}
            for (int i = GA.LAST_GENERATION; i <= GA.MAX_GENERATION; i++)
            {
                if (population.Chromosomes[0].Fitness == 0)
                {
                    break;
                }
                GAEvolve();
            }
            ShowPopulationInForm();
            displayForm.DrawParts(population, 0); // 0 for the best
            buttonAuto.Enabled = false;
        }

        private void dataGridViewPopulation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return; //check if row index is not selected
            //if (dataGridViewPopulation.CurrentCell.ColumnIndex.Equals(3))
            {
                //if (dataGridViewPopulation.CurrentCell != null && dataGridViewPopulation.CurrentCell.Value != null)
                if (e.RowIndex < population.Chromosomes.Count)
                {
                    displayForm.DrawParts(population, e.RowIndex);
                }
            }
        }
    }
}
