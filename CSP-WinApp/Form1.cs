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
            DisplayForm displayForm = new DisplayForm();
            displayForm.Width = (DisplayForm.SCALING_FACTOR * materialWidth) + DisplayForm.RESERVE_BORDER;
            displayForm.Height = (DisplayForm.SCALING_FACTOR * materialLength) + DisplayForm.RESERVE_BORDER;
            displayForm.Show();
            displayForm.DrawMaterial((DisplayForm.SCALING_FACTOR * materialWidth), (DisplayForm.SCALING_FACTOR * materialLength));
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

        private void ShowPopulationInForm()
        {
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
            for (int gen = 2; gen <= 2; gen++) //GA.MAX_GENERATION
            {
                ShowPopulationInForm();
                Population firstHalf = GA.GetFirstHalfPopulation(population, GA.ELITISM_SIZE); // VIPs (elites), do not touch
                Population secondHalf = GA.GetSecondHalfPopulation(population, GA.ELITISM_SIZE); // others needs reproduction
                GA.Crossover(secondHalf, GA.GENE_SIZE);
                GA.Mutation(secondHalf);
                population = GA.CombineFirstAndSecondPopulation(firstHalf, secondHalf);
                foreach (var chromosome in population.Chromosomes)
                {
                    chromosome.CalculateFitness(materialWidth, materialLength);
                }
                population.SortByFitness();
                GA.LAST_GENERATION = gen;
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
