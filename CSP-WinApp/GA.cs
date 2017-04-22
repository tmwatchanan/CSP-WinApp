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
        public const int POPULATION_SIZE = 50;
        public const double ELITISM_RATE = 0.5; // 50% of population size
        public const int ELITISM_SIZE = (int)(ELITISM_RATE * POPULATION_SIZE);
        public const int MUTATION_RATE = 20; // 20% from 100%
        public static int GENE_SIZE = 0;

        public static string Encode(int value, int len)
        {
            //Convert.ToString(gene.X, 2)
            return (len > 1 ? Encode(value >> 1, len - 1) : null) + "01"[value & 1];
        }

        public static void Crossover(Population population, int binaryLength)
        {
            for (int g = 0; g < GA.GENE_SIZE; g++)
            {
                for (int i = 0; i < population.GetSize(); i++)
                {
                    for (int j = i; j < population.GetSize(); j++)
                    {
                        Gene gene1 = population.Chromosomes[i].Genes[g];
                        Gene gene2 = population.Chromosomes[j].Genes[g];

                        string binary1 = "";
                        string binary2 = "";
                        for (int p = 1; p <= 2; p++)
                        {
                            if (p == 1)
                            {
                                binary1 = Encode(gene1.X, binaryLength);
                                binary2 = Encode(gene2.X, binaryLength);
                            }
                            else if (p == 2)
                            {
                                binary1 = Encode(gene1.Y, binaryLength);
                                binary2 = Encode(gene2.Y, binaryLength);
                            }
                            //else if (p == 3)
                            //{
                            //    binary1 = Encode(gene1.Orientation, binaryLength);
                            //    binary2 = Encode(gene2.Orientation, binaryLength);
                            //}
                            Random rand = new Random(Guid.NewGuid().GetHashCode());
                            int crossPosition = rand.Next(GA.GENE_SIZE);
                            string firstHalf = binary1.Substring(crossPosition, binary1.Length);
                            string secondHalf = binary2.Substring(crossPosition, binary2.Length);
                            binary1 = binary1.Substring(0, crossPosition) + secondHalf;
                            binary2 = binary2.Substring(0, crossPosition) + firstHalf;
                        }
                    }
                }
            }
        }

        public static void Mutation(Population population)
        {
            for (int i = 0; i < population.GetSize(); i++)
            {
                for (int g = 0; g < GA.GENE_SIZE; g++)
                {
                    Gene gene = population.Chromosomes[i].Genes[g];

                    string x = Convert.ToString(gene.X, 2);
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
                    population.Chromosomes[i].Genes[g].X = Convert.ToInt32(xStrBuilder.ToString());

                    string y = Convert.ToString(gene.Y, 2);
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
                    population.Chromosomes[i].Genes[g].Y = Convert.ToInt32(yStrBuilder.ToString());

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
                    population.Chromosomes[i].Genes[g].Orientation = Convert.ToInt32(oStrBuilder.ToString());
                }
            }
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
    }
}
