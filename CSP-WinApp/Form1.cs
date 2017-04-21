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
        List<Coordinate> inputList;
        List<Rectangle> parts;

        public Form1()
        {
            InitializeComponent();
            InitializeModel();
        }

        public void InitializeModel()
        {
            inputList = new List<Coordinate>();
            parts = new List<Rectangle>();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            GetFromDataGridView();
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
                Rectangle rectangle = new Rectangle();
                rectangle.X = 0;
                rectangle.Y = 0;
                rectangle.Width = part.Width;
                rectangle.Length = part.Length;
                rectangle.Orientation = '0'; // Horizontal by default
                parts.Add(rectangle);
            }
        }

        private void InitializePopulation()
        {

        }
    }
}
