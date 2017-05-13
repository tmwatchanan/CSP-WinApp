using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_WinApp
{
    static class GA
    {
        public const int MAX_GENERATION = 3000;
        public const int POPULATION_SIZE = 150;
        //public const double ELITISM_RATE = 0.3; // % of population size
        //public const int ELITISM_SIZE = (int)(ELITISM_RATE * POPULATION_SIZE);
        public const int MUTATION_RATE = 2; // 2% from 100%
        public static int GENE_SIZE = 0;
        public static int BINARY_SIZE = 0;
        public static int LAST_GENERATION = 0;
        public static int MIN_FITNESS = Int32.MaxValue;
        public static int STOP_GENERATION = 0;
        public static int STILL_SAME_MAX = 100;
        public static int StillMinCount = 0;

        public static string Encode(int value, int len)
        {
            //Convert.ToString(gene.X, 2)
            return (len > 1 ? Encode(value >> 1, len - 1) : null) + "01"[value & 1];
        }

        public static Population BinaryTournament(Population population)
        {
            Population offspringPopulation = new Population();
            int chromosomeCount = population.Chromosomes.Count;
            for (int c = 0; c < (chromosomeCount / 2); c++)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                int[] random = new int[2] { rand.Next(0, chromosomeCount - 1), rand.Next(0, chromosomeCount - 1) };
                List<Chromosome> offspringChromosomeList = new List<Chromosome>();
                foreach (int rnd in random)
                {
                    offspringChromosomeList.Add(new Chromosome(population.Chromosomes[rnd]));
                }
                // Crossover
                List<Chromosome> xoverChromosomeList = Crossover(offspringChromosomeList);
                foreach (Chromosome cms in xoverChromosomeList)
                {
                    // Mutate before adding
                    Chromosome mutatedCms = Mutation(cms);
                    offspringPopulation.AddChromosome(mutatedCms);
                    //offspringPopulation.AddChromosome(cms);
                }
            }
            offspringPopulation.ResetFitness();
            return offspringPopulation;
        }

        public static List<Chromosome> Crossover(List<Chromosome> chromosomes)
        {
            Chromosome chromosome1 = chromosomes[0];
            Chromosome chromosome2 = chromosomes[1];
            int longest = Form1.materialLongest;
            string materialLongestBinary = Convert.ToString(Form1.materialLongest, 2);
            int binaryLength = materialLongestBinary.Length;
            for (int g = 0; g < GA.GENE_SIZE; g++)
            {
                Gene gene1 = chromosome1.Genes[g];
                Gene gene2 = chromosome2.Genes[g];
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                int crossPosition = rand.Next(1, binaryLength - 1);
                string x1 = Encode(gene1.X, binaryLength);
                string x2 = Encode(gene2.X, binaryLength);
                string xNew1 = "";
                string xNew2 = "";
                for (int k = 0; k < binaryLength; k++)
                {
                    if (k < crossPosition)
                    {
                        xNew1 += x1[k];
                        xNew2 += x2[k];
                    }
                    else
                    {
                        xNew1 += x2[k];
                        xNew2 += x1[k];
                    }
                }
                chromosome1.Genes[g].X = Convert.ToInt32(xNew1, 2);
                chromosome2.Genes[g].X = Convert.ToInt32(xNew2, 2);

                rand = new Random(Guid.NewGuid().GetHashCode());
                crossPosition = rand.Next(0, binaryLength - 1);
                string y1 = Encode(gene1.Y, binaryLength);
                string y2 = Encode(gene2.Y, binaryLength);
                string yNew1 = "";
                string yNew2 = "";
                for (int k = 0; k < binaryLength; k++)
                {
                    if (k < crossPosition)
                    {
                        yNew1 += y1[k];
                        yNew2 += y2[k];
                    }
                    else
                    {
                        yNew1 += y2[k];
                        yNew2 += y1[k];
                    }
                }
                chromosome1.Genes[g].Y = Convert.ToInt32(yNew1, 2);
                chromosome2.Genes[g].Y = Convert.ToInt32(yNew2, 2);
            }
            List<Chromosome> crossoverChromosomes = new List<Chromosome>();
            crossoverChromosomes.Add(chromosome1);
            crossoverChromosomes.Add(chromosome2);
            return crossoverChromosomes;
        }

        public static Chromosome Mutation(Chromosome chromosome)
        {
            int geneCount = GA.GENE_SIZE;
            int longest = Form1.materialLongest;
            string materialLongestBinary = Convert.ToString(Form1.materialLongest, 2);
            int binaryLength = materialLongestBinary.Length;
            for (int g = 0; g < geneCount; g++)
            {
                Gene gene = chromosome.Genes[g];
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                string x = Encode(gene.X, binaryLength);
                StringBuilder xStrBuilder = new StringBuilder(x);
                for (int k = 0; k < xStrBuilder.Length; k++)
                {
                    int percent = rand.Next(0, 100);
                    if (percent <= GA.MUTATION_RATE)
                    {
                        xStrBuilder[k] = (xStrBuilder[k] == '0' ? '1' : '0');
                    }
                }
                chromosome.Genes[g].X = Convert.ToInt32(xStrBuilder.ToString(), 2);

                string y = Encode(gene.Y, binaryLength);
                StringBuilder yStrBuilder = new StringBuilder(y);
                for (int k = 0; k < yStrBuilder.Length; k++)
                {
                    int percent = rand.Next(0, 100);
                    if (percent <= GA.MUTATION_RATE)
                    {
                        yStrBuilder[k] = (yStrBuilder[k] == '0' ? '1' : '0');
                    }
                }
                chromosome.Genes[g].Y = Convert.ToInt32(yStrBuilder.ToString(), 2);

                int orientation = gene.Orientation;
                {
                    int percent = rand.Next(0, 100);
                    if (percent <= GA.MUTATION_RATE)
                    {
                        orientation = (orientation == 0 ? 1 : 0);
                    }
                }
                chromosome.Genes[g].Orientation = orientation;
            }
            return chromosome;
        }

        public static Population CombineFirstAndSecondPopulation(Population firstHalf, Population secondHalf)
        {
            Population combinedPopulation = new Population();
            for (int i = 0; i < firstHalf.Chromosomes.Count; i++)
            {
                combinedPopulation.AddChromosome(firstHalf.Chromosomes[i]);
            }
            for (int j = 0; j < secondHalf.Chromosomes.Count; j++)
            {
                combinedPopulation.AddChromosome(secondHalf.Chromosomes[j]);
            }
            for (int k = 0; k < combinedPopulation.Chromosomes.Count; k++)
            {
                combinedPopulation.Chromosomes[k].Fitness = 0;
            }
            return combinedPopulation;
        }
    }
}
