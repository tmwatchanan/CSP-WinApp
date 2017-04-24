using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_WinApp
{
    public class Population
    {
        private List<Chromosome> chromosomes;

        public List<Chromosome> Chromosomes { get; set; }

        public Population()
        {
            Chromosomes = new List<Chromosome>();
        }

        public void CalculateFitnessOfAllChromosomes()
        {
            foreach (var chromosome in this.Chromosomes)
            {
                chromosome.CalculateFitness(Form1.materialWidth, Form1.materialLength);
            }
        }

        public void SortByFitness()
        {
            this.Chromosomes = this.Chromosomes.OrderBy(o => o.Fitness).ToList(); // From min to max
        }

        public void AddChromosome(Chromosome individual)
        {
            this.Chromosomes.Add(individual);
        }

        public int GetSize()
        {
            return Chromosomes.Count;
        }

        public void ResetFitness()
        {
            foreach (var chromosome in this.Chromosomes)
            {
                chromosome.Fitness = 0;
            }
        }

        public void CutInHalf()
        {
            this.Chromosomes.Take(this.GetSize() / 2);
        }
    }
}
