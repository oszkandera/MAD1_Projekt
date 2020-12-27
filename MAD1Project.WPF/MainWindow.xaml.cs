using Mad1_Projekt.Exporter;
using Mad1_Projekt.Generator;
using Mad1_Projekt.Processor;
using MAD1Project.Core.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace MAD1Project.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Dictionary<int, HashSet<int>> _graph;

        public object GetTimestampDateTime { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            GraphGeneratorButton.IsEnabled = false;
            ClusteringButton.IsEnabled = false;
            GraphExport.IsEnabled = false;
        }

        private void GraphGeneratorButton_Click(object sender, RoutedEventArgs e)
        {
            var nodeNumber = Convert.ToInt32(NodeCountInput.Text);
            var pParameter = Convert.ToDouble(ParameterPInput.Text);

            var graphGenerator = new WattsStrogatzModelGenerator();
            _graph = graphGenerator.Generate(nodeNumber, pParameter);


            ClusteringButton.IsEnabled = true;
            GraphExport.IsEnabled = true;

            MessageBox.Show("Graf úspěšně vytvořen");
        }

        private void ClusteringButton_Click(object sender, RoutedEventArgs e)
        {
            var resultWindow = new ResultWindow(_graph);

            resultWindow.Show();

        }

        private void DegreeDistributionButton_Click(object sender, RoutedEventArgs e)
        {
            var basicGraphProcessor = new BasicGraphProcessor();
            var degreeDistribution = basicGraphProcessor.GetDegreeDistribution(_graph);
            var dataPoints = degreeDistribution.ToDataPointList();

            var graphWindow = new GraphWindow(dataPoints);

            graphWindow.Show();
        }

        private void GraphExport_Click(object sender, RoutedEventArgs e)
        {

            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.ShowDialog();
                var selectedPath = folderBrowserDialog.SelectedPath;

                var fullPath = Path.Join(selectedPath, $"small_world_model_{DateTime.Now.ToString("yyyyMMddHHmmssffff")}.csv");

                using(var stream = new FileStream(fullPath, FileMode.Create))
                {
                    var graphToCsvExporter = new GraphToCsvExporter();
                    graphToCsvExporter.Export(stream, _graph);
                }
            }
        }

        private void GenerateGraphInputCheckEvent(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(String.IsNullOrWhiteSpace(ParameterPInput.Text) || String.IsNullOrWhiteSpace(NodeCountInput.Text))
            {
                GraphGeneratorButton.IsEnabled = false;
                return;
            }

            if(!Double.TryParse(ParameterPInput.Text, out double parameterP))
            {
                GraphGeneratorButton.IsEnabled = false;
                return;
            }

            if (!Int32.TryParse(NodeCountInput.Text, out int nodeCount))
            {
                GraphGeneratorButton.IsEnabled = false;
                return;
            }

            GraphGeneratorButton.IsEnabled = true;
        }
    }
}
