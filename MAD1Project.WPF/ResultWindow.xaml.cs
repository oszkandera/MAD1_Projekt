using Mad1_Projekt.Processor;
using MAD1Project.Core.Extensions;
using MAD1Project.WPF.Export;
using MAD1Project.WPF.Model;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace MAD1Project.WPF
{
    public partial class ResultWindow : Window
    {
        public List<DataPoint> DegreeDistributionPoints { get; set; }
        public List<ScatterPoint> ClusteringEffectDistribution { get; set; }

        private AnalysisExport _analysisExport = new AnalysisExport();

        public ResultWindow(Dictionary<int, HashSet<int>> graph, double parameterP)
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

            _analysisExport.ParameterP = parameterP;
            _analysisExport.GraphMean = graphMean;
            _analysisExport.ClusteringCoeficient = clusteringCoeficient;
            _analysisExport.AverageClusteringCieficient = averageClusteringCoeficient;
            _analysisExport.NodeCount = graph.Count;

            _analysisExport.DegreeDistributionGraph = new MemoryStream();
            var pngExporterDegreeDistributionGraph = new PngExporter { Width = 600, Height = 400, Background = OxyColors.White };
            pngExporterDegreeDistributionGraph.Export(DegreeDistributionGraph.ActualModel, _analysisExport.DegreeDistributionGraph);
            _analysisExport.DegreeDistributionGraph.Position = 0;


            _analysisExport.ClusteringEffectDistributionGraph = new MemoryStream();
            var pngExporterClusteringEffectDistributionGraph = new PngExporter { Width = 600, Height = 400, Background = OxyColors.White };
            pngExporterClusteringEffectDistributionGraph.Export(ClusteringEffectDistributionGraph.ActualModel, _analysisExport.ClusteringEffectDistributionGraph);
            _analysisExport.ClusteringEffectDistributionGraph.Position = 0;

            AverageClusteringCoeficient.Text = averageClusteringCoeficient.ToString();
            GraphMean.Text = graphMean.ToString();
        }

        private void ExportAnalysis_Click(object sender, RoutedEventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.ShowDialog();
                var selectedPath = folderBrowserDialog.SelectedPath;

                var fullPath = Path.Join(selectedPath, $"small_world_model_analysis_{DateTime.Now.ToString("yyyyMMddHHmmssffff")}.docx");

                var wordExporter = new WordExporter();
                wordExporter.Export(fullPath, _analysisExport);
            }
        }
    }
}
