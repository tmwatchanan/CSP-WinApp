using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_WinApp
{
    class Population
    {
        private List<Chromosome> chromosomes;

        public List<Chromosome> Chromosomes { get; set; }

        public Population()
        {
            Chromosomes = new List<Chromosome>();
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
    }
}
