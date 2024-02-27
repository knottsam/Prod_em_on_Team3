using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prod_em_on_Team3.ProceduralGeneration
{
    public class ObstacleGenerationData
    {
        public int numberOfCrawlers;
        public int iterationMin;
        public int iterationMax;    

        public ObstacleGenerationData(int numberOfCrawlers, int iterationMin, int iterationMax)
        {
            this.numberOfCrawlers = numberOfCrawlers;
            this.iterationMin = iterationMin;
            this.iterationMax = iterationMax;
        }
    }
}
