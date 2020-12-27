using Mad1_Projekt.Processor;
using MAD1Project.Core.Extensions;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace MAD1Project.WPF
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public List<DataPoint> DegreeDistributionPoints { get; set; }
        public List<ScatterPoint> ClusteringEffectDistribution { get; set; }

        public ResultWindow(Dictionary<int, HashSet<int>> graph)
        {
            var basicGraphProcessor = new BasicGraphProcessor();
            var graphClusteringProcessor = new GraphClusteringProcessor();

            var graphMean = basicGraphProcessor.GetAverageOfDegree(graph);
            var degreeDistribution = basicGraphProcessor.GetDegreeDistribution(graph);

            var clusteringCoeficient = graphClusteringProcessor.GetClusteringCoeficient(graph);
            var clusteringEffect = graphClusteringProcessor.GetClusteringEffect(graph, clusteringCoeficient);
            var averageClusteringCoeficient = graphClusteringProcessor.GetAverageClusteringCoeficient(clusteringCoeficient); //tranzitivita

            DegreeDistributionPoints = degreeDistribution.ToDataPointList();
            ClusteringEffectDistribution = clusteringEffect.ToScatterPoint();

            this.DataContext = this;
            InitializeComponent();  

            //TODO_OnSz: Dodelat export

            var stream = new FileStream(@"C:\\Users\\Ondra\\Desktop\\test.png", FileMode.Create);
            var pngExporter = new PngExporter { Width = 600, Height = 400, Background = OxyColors.White };
            pngExporter.Export(Graph.ActualModel, stream);

            AverageClusteringCoeficient.Text = averageClusteringCoeficient.ToString();
            GraphMean.Text = graphMean.ToString();
        }
    }
}
