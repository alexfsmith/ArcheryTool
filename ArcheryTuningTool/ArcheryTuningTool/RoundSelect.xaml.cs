using System.Windows;
using System.Windows.Controls;

namespace ArcheryTuningTool
{
    /// <summary>
    /// Interaction logic for RoundSelect.xaml
    /// </summary>
    public partial class RoundSelect : Window
    {
        private enum EBowStyle
        {
            Recurve,
            Compound,
            Barebow,
            Longbow,
            Other
        };

        private EBowStyle eBowStyle;
        private int nArrows;

        public RoundSelect()
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
            cbBowStyle.SelectedIndex = 0;

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

        private bool Validate()
        {
            return false;
        }

    }
}