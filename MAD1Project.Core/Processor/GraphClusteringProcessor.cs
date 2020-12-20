using System;
using System.Collections.Generic;
using System.Linq;

namespace Mad1_Projekt.Processor
{
    public class GraphClusteringProcessor
    {
        public List<Tuple<int, decimal>> GetClusteringCoeficient(Dictionary<int, HashSet<int>> data)
        {
            var clusteringCoeficients = new List<Tuple<int, decimal>>();
            foreach (var node in data)
            {
                var nodeDegree = node.Value.Count;
                var numberOfConnectionsBetweenNeighboards = GetNumberOfConnectionsBetweenNeighboards(node, data);
                var clusteringCoeficient = nodeDegree <= 1 ? 0.0m : (decimal)(numberOfConnectionsBetweenNeighboards) / (nodeDegree * (nodeDegree - 1));
                clusteringCoeficients.Add(new Tuple<int, decimal>(node.Key, clusteringCoeficient));
            }

            return clusteringCoeficients;
        }

        public double GetAverageClusteringCoeficient(List<Tuple<int, decimal>> clusteringCoeficients)
        {
            return Convert.ToDouble(Enumerable.Sum(clusteringCoeficients.Select(x => x.Item2)) / clusteringCoeficients.Count);
        }

        public List<Tuple<int, decimal>> GetClusteringEffect(Dictionary<int, HashSet<int>> data, List<Tuple<int, decimal>> clusteringCoeficients)
        {
            var dataGroupedByDegree = data.GroupBy(x => x.Value.Count);

            var clusteringEffect = new List<Tuple<int, decimal>>();

            foreach (var degreeGroup in dataGroupedByDegree)
            {
                var nodesInDegree = degreeGroup.Select(x => x.Key);

                clusteringEffect.Add(new Tuple<int, decimal>(degreeGroup.Key,
                    Enumerable.Sum(clusteringCoeficients.Where(x => nodesInDegree.Contains(x.Item1)).Select(x => x.Item2)) / nodesInDegree.Count()));
            }

            return clusteringEffect;
        }


        private int GetNumberOfConnectionsBetweenNeighboards(KeyValuePair<int, HashSet<int>> node, Dictionary<int, HashSet<int>> data)
        {
            var numberOfConnectionsBetweenNeighboards = 0;
            foreach (var neighboardId in node.Value)
            {
                var neigboardsOfNeighboard = data[neighboardId];

                foreach (var neighboardOfNeighboard in neigboardsOfNeighboard)
                {
                    if (neighboardOfNeighboard == node.Key) continue;

                    if (node.Value.Contains(neighboardOfNeighboard)) numberOfConnectionsBetweenNeighboards++;
                }
            }

            return numberOfConnectionsBetweenNeighboards;
        }
    }
}
