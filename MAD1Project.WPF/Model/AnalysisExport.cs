using System;
using System.Collections.Generic;
using System.IO;

namespace MAD1Project.WPF.Model
{
    public class AnalysisExport
    {
        public int NodeCount { get; set; }
        public double ParameterP { get; set; }
        public double GraphMean { get; set; }
        public List<Tuple<int, decimal>> ClusteringCoeficient { get; set; }
        public double AverageClusteringCieficient { get; set; }
        public Stream DegreeDistributionGraph { get; set; }
        public Stream ClusteringEffectDistributionGraph { get; set; }
    }
}
