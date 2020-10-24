using System.Collections.Generic;
using System.Windows;
using System.Windows.Shapes;

namespace ArcheryTool
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

        private void BnScoring_Click(object sender, RoutedEventArgs e)
        {
            RoundSelect round = new RoundSelect(this);
            this.Visibility = Visibility.Hidden;
            round.ShowDialog();
        }

        private void BnTuning_Click(object sender, RoutedEventArgs e)
        {
            TuningTool tuning = new TuningTool();
            tuning.ShowDialog();
        }

        private void BnHistory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
