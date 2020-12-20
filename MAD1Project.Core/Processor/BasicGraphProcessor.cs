using System;
using System.Collections.Generic;
using System.Linq;

namespace Mad1_Projekt.Processor
{
    public class BasicGraphProcessor
    {
        public double GetAverageOfDegree(Dictionary<int, HashSet<int>> graph)
        {
            var sumOfDegree = graph.Sum(x => x.Value.Count());
            return (double)sumOfDegree / graph.Count;
        }

        public List<Tuple<int, double>> GetDegreeDistribution(Dictionary<int, HashSet<int>> graph)
        {
            var degrees = graph.Select(x => new Tuple<int, int>(x.Key, x.Value.Count));
            var groupOfDegrees = degrees.GroupBy(x => x.Item2).ToList();

            return groupOfDegrees.Select(x => new Tuple<int, double>(x.Key, x.Count())).OrderBy(x => x.Item1).ToList();
        }
    }
}
