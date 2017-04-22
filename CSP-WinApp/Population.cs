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
            this.Individuals = this.Individuals.OrderBy(o => o.Fitness).ToList(); // From min to max
        }

        public void AddIndividual(Individual individual)
        {
            this.Individuals.Add(individual);
        }

        public int GetSize()
        {
            return Individuals.Count;
        }
        
        public void RandomAllIndividuals(int width, int length)
        {
            foreach (var individual in this.Individuals)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                individual.X = rand.Next(width); // 0 - width
                individual.Y = rand.Next(length); // 0 - length
            }
        }

        public void CalculateFitness(int width, int length)
        {
            for (int i = 0; i < Individuals.Count; i++)
            {
                for (int j = i; j < Individuals.Count; j++)
                {
                    Individuals[i].CalculateItsFitness(width, length);
                    Individual.CalculateFitnessWithOther(Individuals[i], Individuals[j]);
                }
            }
        }
    }
}
