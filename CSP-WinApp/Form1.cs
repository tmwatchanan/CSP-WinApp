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
        List<Coordinate> inputList;
        List<Rectangle> parts;
        List<Individual> population;

        public Form1()
        {
            InitializeComponent();
            InitializeModel();
        }

        public void InitializeModel()
        {
            inputList = new List<Coordinate>();
            parts = new List<Rectangle>();
            population = new List<Individual>();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            GetFromDataGridView();
            CreateRetangles();
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
        
        private void CreateRetangles()
        {
            foreach (var part in inputList)
            {
                Rectangle rectangle = new Rectangle(0, 0, part.Width, part.Length, 0);
                parts.Add(rectangle);
            }
        }

        private void InitializePopulation()
        {
            Individual individual = new Individual();
            foreach (var part in parts)
            {
                individual.Chromosome.Add(part.X);
                individual.Chromosome.Add(part.Y);
                individual.Chromosome.Add(part.Orientation);
            }
            return;
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
