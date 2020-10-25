using System.Windows;
using System.Windows.Controls;

namespace ArcheryTool
{
    /// <summary>
    /// Interaction logic for RoundSelect.xaml
    /// </summary>
    public partial class RoundSelect : Window
    {
        public enum EBowStyle
        {
            Recurve,
            Compound,
            Barebow,
            Longbow,
            Other
        };

        public enum ERound
        {
            Fita18,
            Portsmouth,
            WA1440,
            WA720
        };

        private EBowStyle eBowStyle;
        private ERound eRound;
        private int nArchers;
        private bool bSighters;
        private MainWindow mw;

        public RoundSelect(MainWindow mw)
        {
            this.mw = mw;
            InitializeComponent();
            SetupComboBoxes();
        }

        //Setup for combo boxes to add options
        protected void SetupComboBoxes()
        {
            cbBowStyle.Items.Add("Olympic Recurve");
            cbBowStyle.Items.Add("Compound");
            cbBowStyle.Items.Add("Barebow Recurve");
            cbBowStyle.Items.Add("Longbow");
            cbBowStyle.Items.Add("Other");
            cbBowStyle.SelectedIndex = 0;

            cbIndoor.Items.Add("Indoor");
            cbIndoor.Items.Add("Outdoor");
            cbIndoor.SelectedIndex = 0;

            SetupRoundCombo(true);

            cbNoArchers.Items.Add(1);
            cbNoArchers.Items.Add(2);
            cbNoArchers.Items.Add(3);
            cbNoArchers.Items.Add(4);
            cbNoArchers.SelectedIndex = 0;
            cbNoArchers.IsEnabled = false;      //until functionality there for multiple
        }

        protected void SetupRoundCombo(bool bIndoors)
        {
            cbRound.Items.Clear();
            if (bIndoors)
            {
                cbRound.Items.Add("FITA 18");
                cbRound.Items.Add("Portsmouth");
                //cbRound.Items.Add("Worcester");
                //cbRound.Items.Add("Vegas");
            }
            else
            {
                cbRound.Items.Add("WA1440");
                cbRound.Items.Add("WA720");
                //cbRound.Items.Add("York");
                //cbRound.Items.Add("St George");
            }
            cbRound.SelectedIndex = 0;
        }

        //Message handlers for controls + canvas
        private void CbBowStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbBowStyle.SelectedItem)
            {
                case "Olympic Recurve":
                    eBowStyle = EBowStyle.Recurve;
                    break;
                case "Compound":
                    eBowStyle = EBowStyle.Compound;
                    break;
                case "Barebow Recurve":
                    eBowStyle = EBowStyle.Barebow;
                    break;
                case "Longbow":
                    eBowStyle = EBowStyle.Longbow;
                    break;
                default:
                    eBowStyle = EBowStyle.Other;
                    break;
            }
        }

        private void CbIndoor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool bIndoors;
            if (cbIndoor.SelectedItem.ToString() == "Indoor")
                bIndoors = true;
            else
                bIndoors = false;
            SetupRoundCombo(bIndoors);
        }

        private void CbRound_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(cbRound.SelectedItem)
            {
                case "FITA 18":
                    eRound = ERound.Fita18;
                    break;
                case "Portsmouth":
                    eRound = ERound.Portsmouth;
                    break;
                case "WA1440":
                    eRound = ERound.WA1440;
                    break;
                case "WA720":
                    eRound = ERound.WA720;
                    break;
            }
        }

        private void CbNoArchers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nArchers = (int)cbNoArchers.SelectedItem;
        }

        private void ChkSighters_Unchecked(object sender, RoutedEventArgs e)
        {
            bSighters = !bSighters;
        }

        private void BnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            ScoreEntry score = new ScoreEntry(eRound, eBowStyle, nArchers, bSighters, this);
            score.ShowDialog();
        }

        private void BnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mw.Visibility = Visibility.Visible;
        }
    }
}