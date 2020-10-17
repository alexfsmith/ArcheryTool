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

namespace ArcheryTuningTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum EBowStyle {
            Recurve,
            Compound,
            Barebow,
            Longbow,
            Other
        };

        private EBowStyle eBowStyle;
        private bool bRightHanded;
        private int nFletched;
        private int nBareshaft;

        public MainWindow()
        {
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

            cbHands.Items.Add("Right-handed");      //should not be visible for compound? maybe have a shootthrough option?
            cbHands.Items.Add("Left-handed");
        }

        //Message handlers for controls + canvas
        private void CbBowStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(cbBowStyle.SelectedItem)
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

        private void CbHands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbHands.SelectedItem.ToString() == "Right-handed")
                bRightHanded = true;
            else
                bRightHanded = false;
        }

        private void BnOk_Click(object sender, RoutedEventArgs e)
        {
            nFletched = int.Parse(tbFletched.Text);
            nBareshaft = int.Parse(tbBareshaft.Text);
        }
    }
}
