using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_WinApp
{
    public class Chromosome
    {
        private int fitness;
        private List<Gene> genes;

        public int Fitness { get; set; }
        public List<Gene> Genes { get; set; }

        public Chromosome()
        {
            this.Fitness = 0;
            this.Genes = new List<Gene>();
        }

        public int GetSize()
        {
            return Genes.Count;
        }

        public void AddGene(int x, int y, int orientation, int width, int height)
        {
            Gene gene = new Gene(x, y, orientation, width, height);
            Genes.Add(gene);
        }

        public void CalculateFitnessOutOfBound()
        {
            System.Drawing.Rectangle rightBound = new System.Drawing.Rectangle(Form1.materialWidth, 0, Form1.materialLongest * 2, Form1.materialLongest * 2);
            System.Drawing.Rectangle bottomBound = new System.Drawing.Rectangle(0, Form1.materialLength, Form1.materialLongest * 2, Form1.materialLongest * 2);
            System.Drawing.Rectangle intersectArea;
            foreach (var gene in Genes)
            {
                System.Drawing.Rectangle geneRect = new System.Drawing.Rectangle(gene.X, gene.Y, gene.Width, gene.Length);
                intersectArea = System.Drawing.Rectangle.Intersect(geneRect, rightBound);
                this.Fitness += intersectArea.Width * intersectArea.Height;
                intersectArea = System.Drawing.Rectangle.Intersect(geneRect, bottomBound);
                this.Fitness += intersectArea.Width * intersectArea.Height;
            }
        }

        public void CalculateFitnessWithOther()
        {
            for (int i = 0; i < Genes.Count; i++)
            {
                Gene firstGene = Genes[i];
                System.Drawing.Rectangle drawRect1 = new System.Drawing.Rectangle(firstGene.X, firstGene.Y, firstGene.Width, firstGene.Length);
                for (int j = i; j < Genes.Count; j++)
                {
                    Gene secondGene = Genes[j];
                    System.Drawing.Rectangle drawRect2 = new System.Drawing.Rectangle(secondGene.X, secondGene.Y, secondGene.Width, secondGene.Length);
                    System.Drawing.Rectangle intersectArea = System.Drawing.Rectangle.Intersect(drawRect1, drawRect2);
                    this.Fitness += intersectArea.Width * intersectArea.Height;
                }
            }
        }

        public void CalculateFitness(int width, int length)
        {
            CalculateFitnessOutOfBound();
            CalculateFitnessWithOther();
        }

        public void RandomAllGenes(int width, int length)
        {
            foreach (var gene in this.Genes)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                gene.X = rand.Next(width); // 0 - width
                gene.Y = rand.Next(length); // 0 - length
                gene.Orientation = rand.Next(1); // 0 - 1
            }
        }
    }
}
