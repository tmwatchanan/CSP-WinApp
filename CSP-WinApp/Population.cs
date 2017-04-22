using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_WinApp
{
    class Population
    {
        private List<Individual> individuals;

        public List<Individual> Individuals { get; set; }

        public Population()
        {
            Individuals = new List<Individual>();
        }

        public void SortByFitness()
        {
            this.Individuals = this.Individuals.OrderBy(o => o.Fitness).ToList();
        }

        public void AddIndividual(Individual individual)
        {
            this.Individuals.Add(individual);
        }
    }
}
