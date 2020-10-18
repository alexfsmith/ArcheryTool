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
        private Ring<ArrowGraphic> lFletched;
        private int nBareshaft;
        private Ring<ArrowGraphic> lBareshaft;
        private int nPoundage;
        private int nDrawLength;
        private string sSpine;
        private double dArrowLength;
        private int nBowLength;         //advanced
        private int nStrands;           //advanced
        private int nPointWeight;       //advanced
        private string sStringMaterial; //advanced

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
            cbBowStyle.SelectedIndex = 0;

            cbHands.Items.Add("Right-handed");      //should not be visible for compound? maybe have a shoot-through option?
            cbHands.Items.Add("Left-handed");
            cbHands.SelectedIndex = 0;

            cbFletched.Items.Add(3);
            cbFletched.Items.Add(6);
            cbFletched.SelectedIndex = 0;

            cbBareshaft.Items.Add(1);
            cbBareshaft.Items.Add(2);
            cbBareshaft.SelectedIndex = 0;
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
            if (Validate())
            {
                nPoundage = int.Parse(tbPoundage.Text);
                nDrawLength = int.Parse(tbDrawLength.Text);
                sSpine = tbSpine.Text;
                dArrowLength = double.Parse(tbArrowLength.Text);
            }
            else
            {
                MessageBox.Show("Please check input and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private bool Validate()
        {
            int outInt;
            double outDouble;

            if (!int.TryParse(tbPoundage.Text, out outInt)) return false;
            if (!int.TryParse(tbDrawLength.Text, out outInt)) return false;
            if (!double.TryParse(tbArrowLength.Text, out outDouble)) return false;

            return true;
        }

        private void Target_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePos = e.GetPosition(target);

            if (Keyboard.Modifiers == ModifierKeys.Control) //bareshaft
            {
                Ellipse bareshaft = new Ellipse();
                bareshaft.Width = 10;
                bareshaft.Height = 10;
                bareshaft.Fill = Brushes.White;
                bareshaft.Stroke = Brushes.Black;
                bareshaft.StrokeThickness = 1;
                bareshaft.VerticalAlignment = VerticalAlignment.Top;
                bareshaft.HorizontalAlignment = HorizontalAlignment.Left;
                bareshaft.Margin = new Thickness(mousePos.X, mousePos.Y, 0, 0);
                target.Children.Add(bareshaft);
            }
            else
            {
                ArrowGraphic fletched = new ArrowGraphic();
                fletched.Width = 10;
                fletched.Height = 10;
                fletched.Fill = Brushes.White;
                fletched.Stroke = Brushes.Black;
                fletched.StrokeThickness = 1;
                fletched.VerticalAlignment = VerticalAlignment.Top;
                fletched.HorizontalAlignment = HorizontalAlignment.Left;
                fletched.Margin = new Thickness(mousePos.X, mousePos.Y, 0, 0);
                target.Children.Add(fletched);
            }
        }

        private void CbFletched_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFletched.SelectedIndex == 0)
                nFletched = 3;
            else
                nFletched = 6;
        }

        private void CbBareshaft_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (cbBareshaft.SelectedIndex == 0)
                nBareshaft = 1;
            else
                nBareshaft = 2;
        }
    }
}
