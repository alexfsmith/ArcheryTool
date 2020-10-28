using System.Windows;

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
            //TODO
            /* Use clustering to get the fletched group position 
             * relative to the bareshaft (group)
             * Display potential solution/s to user (eg 1/4 turn on 
             * tension button, raise nocking  point)
             */
            TuningTool tuning = new TuningTool();
            tuning.ShowDialog();
        }

        private void BnHistory_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            /*  Have a folder of config files
             *  Get a summary of each (date, total score, etc)
             *  If selected, display a scoresheet of the round
             */
        }

        private void BnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
