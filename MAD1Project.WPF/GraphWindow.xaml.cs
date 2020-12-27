using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Wpf;

namespace MAD1Project.WPF
{
    /// <summary>
    /// Interaction logic for GraphWindow.xaml
    /// </summary>
    /// 


    public partial class GraphWindow : Window
    {
        public string GraphTitle { get; set; }
        public List<DataPoint> Points { get; set; }
        public GraphWindow(List<DataPoint> data)
        {
            this.DataContext = this;
            GraphTitle = "Test";
            Points = data;
            InitializeComponent();
        }
    }
}
