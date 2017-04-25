using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_WinApp
{
    static class GA
    {
        public const int MAX_GENERATION = 1000;
        public const int POPULATION_SIZE = 100;
        //public const double ELITISM_RATE = 0.3; // % of population size
        //public const int ELITISM_SIZE = (int)(ELITISM_RATE * POPULATION_SIZE);
        public const int MUTATION_RATE = 10; // 10% from 100%
        public static int GENE_SIZE = 0;
        public static int BINARY_SIZE = 0;
        public static int LAST_GENERATION = 1;

        public static string Encode(int value, int len)
        {
            //Convert.ToString(gene.X, 2)
            return (len > 1 ? Encode(value >> 1, len - 1) : null) + "01"[value & 1];
        }

        public static Population BinaryTournament(Population population)
        {
            Population parentPopulation = new Population();
            int chromosomeCount = population.Chromosomes.Count;
            for (int c = 0; c < (chromosomeCount / 2); c++)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                int[] random = new int[2] { rand.Next(0, chromosomeCount - 1), rand.Next(0, chromosomeCount - 1) };
                List<Chromosome> offspringChromosomeList = new List<Chromosome>();
                foreach (int rnd in random)
                {
                    offspringChromosomeList.Add(population.Chromosomes[rnd]);
                }
                // Crossover
                offspringChromosomeList = Crossover(offspringChromosomeList);
                foreach (Chromosome cms in offspringChromosomeList)
                {
                    // Mutate before adding
                    //Chromosome mutatedCms = Mutation(cms);
                    parentPopulation.AddChromosome(cms);
                }
            }

            return parentPopulation;
        }

        public static List<Chromosome> Crossover(List<Chromosome> chromosomes)
        {
            List<Chromosome> crossoverChromosomes = new List<Chromosome>();
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            int crossPosition = rand.Next(GA.GENE_SIZE - 1);
            Chromosome chromosome1 = new Chromosome();
            Chromosome chromosome2 = new Chromosome();
            for (int i = 0; i < crossPosition; i++)
            {
                chromosome1.AddGene(chromosomes[0].Genes[i]);
                chromosome2.AddGene(chromosomes[1].Genes[i]);
            }
            for (int i = crossPosition; i < GA.GENE_SIZE; i++)
            {
                chromosome1.AddGene(chromosomes[1].Genes[i]);
                chromosome2.AddGene(chromosomes[0].Genes[i]);
            }
            crossoverChromosomes.Add(chromosome1);
            crossoverChromosomes.Add(chromosome2);
            return crossoverChromosomes;
        }

        public static Chromosome Mutation(Chromosome chromosome)
        {
            int geneCount = GA.GENE_SIZE;
            for (int g = 0; g < geneCount; g++)
            {
                Gene gene = chromosome.Genes[g];

                string x = Encode(gene.X, Form1.materialLongest);
                StringBuilder xStrBuilder = new StringBuilder(x);
                for (int k = 0; k < x.Length; k++)
                {
                    Random rand = new Random(Guid.NewGuid().GetHashCode());
                    int percent = rand.Next(0, 100);
                    if (percent <= GA.MUTATION_RATE)
                    {
                        xStrBuilder[k] = (xStrBuilder[k] == '0' ? '1' : '0');
                    }
                }
                chromosome.Genes[g].X = Convert.ToInt32(xStrBuilder.ToString(), 2);

                string y = Encode(gene.Y, Form1.materialLongest);
                StringBuilder yStrBuilder = new StringBuilder(y);
                for (int k = 0; k < y.Length; k++)
                {
                    Random rand = new Random(Guid.NewGuid().GetHashCode());
                    int percent = rand.Next(0, 100);
                    if (percent <= GA.MUTATION_RATE)
                    {
                        yStrBuilder[k] = (yStrBuilder[k] == '0' ? '1' : '0');
                    }
                }
                chromosome.Genes[g].Y = Convert.ToInt32(yStrBuilder.ToString(), 2);

                string orientation = Convert.ToString(gene.Orientation, 2);
                StringBuilder oStrBuilder = new StringBuilder(orientation);
                {
                    Random rand = new Random(Guid.NewGuid().GetHashCode());
                    int percent = rand.Next(0, 100);
                    if (percent <= GA.MUTATION_RATE)
                    {
                        oStrBuilder[0] = (oStrBuilder[0] == '0' ? '1' : '0');
                    }
                }
                chromosome.Genes[g].Orientation = Convert.ToInt32(oStrBuilder.ToString(), 2);
            }
            return chromosome;
        }

        public static Population GetFirstHalfPopulation(Population population, int half = -99)
        {
            if (half == -99) half = (int)(population.Chromosomes.Count / 2);
            Population firstHalf = new Population();
            for (int i = 0; i < half; i++)
            {
                firstHalf.AddChromosome(population.Chromosomes[i]);
            }
            return firstHalf;
        }
        public static Population GetSecondHalfPopulation(Population population, int half = -99)
        {
            int popSize = population.Chromosomes.Count;
            if (half == -99) half = (int)(popSize / 2);
            Population secondHalf = new Population();
            for (int i = half; i < popSize; i++)
            {
                secondHalf.AddChromosome(population.Chromosomes[i]);
            }
            return secondHalf;
        }

        public static Population CombineFirstAndSecondPopulation(Population firstHalf, Population secondHalf)
        {
            Population population = firstHalf;
            foreach (var chromosome in secondHalf.Chromosomes)
            {
                population.AddChromosome(chromosome);
            }
            return population;
        }
    }
}
