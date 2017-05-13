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

        public Population(Population prototypePopulation)
        {
            Chromosomes = new List<Chromosome>();
            foreach (var cms in prototypePopulation.Chromosomes)
            {
                Chromosomes.Add(new Chromosome(cms));
            }
            //Chromosomes = prototypePopulation.Chromosomes;
        }

        //public void CalculateFitnessOfAllChromosomes()
        //{
        //    //this.ResetFitness();
        //    foreach (var chromosome in this.Chromosomes)
        //    {
        //        chromosome.CalculateFitness(Form1.materialWidth, Form1.materialLength);
        //    }
        //}
        public void CalculateFitnessOfAllChromosomes()
        {
            this.ResetFitness();
            for (int i = 0; i < Chromosomes.Count; i++)
            {
                Chromosomes[i].CalculateFitness(Form1.materialWidth, Form1.materialLength);
            }
        }
        
        public void SortByFitness()
        {
            this.Chromosomes = this.Chromosomes.OrderBy(x => x.Fitness).ToList();
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
            for (int c = 0; c < this.Chromosomes.Count; c++)
            {
                this.Chromosomes[c].Fitness = 0;
            }
        }

        public void CutInHalf()
        {
            this.Chromosomes.RemoveRange(this.Chromosomes.Count / 2, Chromosomes.Count);
        }

        public int FindMinFitness()
        {
            return this.Chromosomes.Min(x => x.Fitness);
        }

        public Population GetFirstHalfPopulation(int half = -99)
        {
            if (half == -99) half = (int)(Chromosomes.Count / 2);
            Population firstHalf = new Population();
            for (int i = 0; i < half; i++)
            {
                firstHalf.AddChromosome(Chromosomes[i]);
            }
            return firstHalf;
        }
        public Population GetSecondHalfPopulation(int half = -99)
        {
            int popSize = Chromosomes.Count;
            if (half == -99) half = (int)(popSize / 2);
            Population secondHalf = new Population();
            for (int i = half; i < popSize; i++)
            {
                secondHalf.AddChromosome(Chromosomes[i]);
            }
            return secondHalf;
        }
    }
}
