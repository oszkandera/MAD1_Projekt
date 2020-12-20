using System;
using System.Collections.Generic;

namespace Mad1_Projekt.Generator
{
    public class WattsStrogatzModelGenerator
    {
        public Dictionary<int, HashSet<int>> Generate(int N, double p)
        {
            var graph = CreateStructureForGraph(N);
            AddRingConnections(graph);
            CreateRewiredConnections(graph, p);
            return graph;
        }

        private void AddRingConnections(Dictionary<int, HashSet<int>> graph)
        {
            var N = graph.Count;

            for (int i = 0; i < N; i++)
            {
                var neighbor1 = i + 1 >= N ? 0 : i + 1;
                var neighbor2 = i + 2 >= N ? i == N - 2 ? 0 : 1 : i + 2;

                AddBidirectConnection(graph, i, neighbor1);
                AddBidirectConnection(graph, i, neighbor2);
            }
        }  

        private void AddBidirectConnection(Dictionary<int, HashSet<int>> graph, int node1, int node2)
        {
            graph[node1].Add(node2);
            graph[node2].Add(node1);
        }

        private void RemoveBidirectConnection(Dictionary<int, HashSet<int>> graph, int node1, int node2)
        {
            graph[node1].Remove(node2);
            graph[node2].Remove(node1);
        }

        private void CreateRewiredConnections(Dictionary<int, HashSet<int>> graph, double p)
        {
            var N = graph.Count;
            var randomGenerator = new Random();
            for (int i = 0; i < N; i++)
            {
                var neighbors = graph[i];
                for(int y = 0; y < neighbors.Count; y++)
                {
                    var generatedProbability = randomGenerator.NextDouble();
                    if(p >= generatedProbability)
                    {
                        var nodeAlreadyExists = false;
                        int randomGeneratedNode;
                        do
                        {
                            randomGeneratedNode = randomGenerator.Next(0, N - 1);
                            if (graph[y].Contains(randomGeneratedNode))
                            {
                                nodeAlreadyExists = true;
                            }
                            else
                            {
                                nodeAlreadyExists = false;
                            }
                        } 
                        while (randomGeneratedNode == y || randomGeneratedNode == i || nodeAlreadyExists);

                        RemoveBidirectConnection(graph, i, y);
                        
                        AddBidirectConnection(graph, y, randomGeneratedNode);
                    }
                }
            }
        }

        private Dictionary<int, HashSet<int>> CreateStructureForGraph(int N)
        {
            var graph = new Dictionary<int, HashSet<int>>();

            for (int i = 0; i < N; i++)
            {
                graph.Add(i, new HashSet<int>());
            }

            return graph;
        }
    }
}
