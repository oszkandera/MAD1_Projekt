using Mad1_Projekt.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MAD1Project.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GraphGeneratorButton_Click(object sender, RoutedEventArgs e)
        {
            var graphGenerator = new WattsStrogatzModelGenerator();
            var graph = graphGenerator.Generate(1000, 0.5);
        }

        private void ClusteringButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DegreeDistributionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AverageDegreeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
