using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_WinApp
{
    class Individual
    {
        int fitness;
        List<int> chromosome;

        public int Fitness { get; set; }
        public List<int> Chromosome { get; set; }

        public Individual()
        {
            Fitness = 0;
            Chromosome = new List<int>();
        }
    }
}
