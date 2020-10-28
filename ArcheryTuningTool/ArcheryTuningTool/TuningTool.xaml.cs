using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ArcheryTool
{
    /// <summary>
    /// Interaction logic for TuningTool.xaml
    /// </summary>
    public partial class TuningTool : Window
    {
        private enum EBowStyle
        {
            Recurve,
            Compound,
            Barebow,
            Longbow,
            Other
        };

        private List<Ellipse> targetGraphics;
        private EBowStyle eBowStyle;
        private bool bRightHanded;
        private int nFletched;
        private UIRing<FletchedGraphic> lFletched;
        private int nBareshaft;
        private UIRing<BareshaftGraphic> lBareshaft;
        private int nPoundage;
        private int nDrawLength;
        private string sSpine;
        private double dArrowLength;
        private int nBowLength;         //advanced only
        private int nStrands;           //advanced only
        private int nPointWeight;       //advanced only
        private string sStringMaterial; //advanced only

        public TuningTool()
        {
            InitializeComponent();
            SetupTenZoneTargetGraphics();
            SetupComboBoxes();
            lFletched = new UIRing<FletchedGraphic>(nFletched);
            lBareshaft = new UIRing<BareshaftGraphic>(nBareshaft);
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

        private bool Validate()
        {
            int outInt;
            double outDouble;

            if (!int.TryParse(tbPoundage.Text, out outInt)) return false;
            if (!int.TryParse(tbDrawLength.Text, out outInt)) return false;
            if (!double.TryParse(tbArrowLength.Text, out outDouble)) return false;

            return true;
        }

        //Handlers
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

        private void CbHands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbHands.SelectedItem.ToString() == "Right-handed")
                bRightHanded = true;
            else
                bRightHanded = false;
        }

        private void CbFletched_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFletched.SelectedIndex == 0)
                nFletched = 3;
            else
                nFletched = 6;
            if (lFletched != null)
                lFletched.SetNumElements(nFletched);
        }

        private void CbBareshaft_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (cbBareshaft.SelectedIndex == 0)
                nBareshaft = 1;
            else
                nBareshaft = 2;
            if (lBareshaft != null)
                lBareshaft.SetNumElements(nBareshaft);
        }

        private void BnOk_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                nPoundage = int.Parse(tbPoundage.Text);         //TODO: set focus to the incorrect field
                nDrawLength = int.Parse(tbDrawLength.Text);
                sSpine = tbSpine.Text;
                dArrowLength = double.Parse(tbArrowLength.Text);
            }
            else
            {
                MessageBox.Show("Please check input and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Target_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePos = e.GetPosition(target);

            if (Keyboard.Modifiers == ModifierKeys.Control) //bareshaft
            {
                BareshaftGraphic bareshaft = new BareshaftGraphic(mousePos);
                lBareshaft.Add(bareshaft);
            }
            else
            {
                FletchedGraphic fletched = new FletchedGraphic(mousePos);
                lFletched.Add(fletched);
            }

            UpdateTargetChildren();
        }

        //Target graphics
        private void UpdateTargetChildren()
        {
            target.Children.Clear();
            AddTargetGraphic();

            if (lBareshaft.GetNumElements() > 0)
            {
                int head = lBareshaft.GetHead();
                lBareshaft.SetHead(0);
                for (int i = 0; i < lBareshaft.GetSize(); i++)
                {
                    BareshaftGraphic bareshaft = (BareshaftGraphic)lBareshaft[i];
                    if (bareshaft != null)
                        target.Children.Add(bareshaft);                             //ienumerator will go through the whole array even if elements are null
                    lBareshaft.MoveNext();
                }
                lBareshaft.SetHead(head);
            }

            if (lFletched.GetNumElements() > 0)
            {
                int head = lFletched.GetHead();
                lFletched.SetHead(0);
                for (int i = 0; i < lFletched.GetSize(); i++)
                {
                    FletchedGraphic fletched = (FletchedGraphic)lFletched[i];
                    if (fletched != null)
                        target.Children.Add(fletched);      
                    lFletched.MoveNext();
                }
                lFletched.SetHead(head);
            }
        }

        private void AddTargetGraphic()
        {
            foreach (Ellipse ellipse in targetGraphics)
            {
                target.Children.Add(ellipse);
            }
        }

        private void SetupTenZoneTargetGraphics()
        {
            targetGraphics = new List<Ellipse>();

            Ellipse white1 = new Ellipse();
            white1.Width = 400;
            white1.Height = 400;
            white1.Margin = new Thickness(0);
            white1.Fill = Brushes.White;
            white1.Stroke = Brushes.Black;
            white1.StrokeThickness = 1;
            white1.Opacity = 100;
            white1.Stretch = Stretch.UniformToFill;

            Ellipse white2 = new Ellipse();
            white2.Width = 360;
            white2.Height = 360;
            white2.Margin = new Thickness(20);
            white2.Fill = Brushes.White;
            white2.Stroke = Brushes.Black;
            white2.StrokeThickness = 1;
            white2.Opacity = 100;
            white2.Stretch = Stretch.UniformToFill;

            Ellipse black1 = new Ellipse();
            black1.Width = 320;
            black1.Height = 320;
            black1.Margin = new Thickness(40);
            black1.Fill = Brushes.Black;
            black1.Stroke = Brushes.White;
            black1.StrokeThickness = 1;
            black1.Opacity = 100;
            black1.Stretch = Stretch.UniformToFill;

            Ellipse black2 = new Ellipse();
            black2.Width = 280;
            black2.Height = 280;
            black2.Margin = new Thickness(60);
            black2.Fill = Brushes.Black;
            black2.Stroke = Brushes.White;
            black2.StrokeThickness = 1;
            black2.Opacity = 100;
            black2.Stretch = Stretch.UniformToFill;

            Ellipse blue1 = new Ellipse();
            blue1.Width = 240;
            blue1.Height = 240;
            blue1.Margin = new Thickness(80);
            blue1.Fill = Brushes.Aqua;
            blue1.Stroke = Brushes.Black;
            blue1.StrokeThickness = 1;
            blue1.Opacity = 100;
            blue1.Stretch = Stretch.UniformToFill;

            Ellipse blue2 = new Ellipse();
            blue2.Width = 200;
            blue2.Height = 200;
            blue2.Margin = new Thickness(100);
            blue2.Fill = Brushes.Aqua;
            blue2.Stroke = Brushes.Black;
            blue2.StrokeThickness = 1;
            blue2.Opacity = 100;
            blue2.Stretch = Stretch.UniformToFill;

            Ellipse red1 = new Ellipse();
            red1.Width = 160;
            red1.Height = 160;
            red1.Margin = new Thickness(120);
            red1.Fill = Brushes.Red;
            red1.Stroke = Brushes.Black;
            red1.StrokeThickness = 1;
            red1.Opacity = 100;
            red1.Stretch = Stretch.UniformToFill;

            Ellipse red2 = new Ellipse();
            red2.Width = 120;
            red2.Height = 120;
            red2.Margin = new Thickness(140);
            red2.Fill = Brushes.Red;
            red2.Stroke = Brushes.Black;
            red2.StrokeThickness = 1;
            red2.Opacity = 100;
            red2.Stretch = Stretch.UniformToFill;

            Ellipse gold1 = new Ellipse();
            gold1.Width = 80;
            gold1.Height = 80;
            gold1.Margin = new Thickness(160);
            gold1.Fill = Brushes.Yellow;
            gold1.Stroke = Brushes.Black;
            gold1.StrokeThickness = 1;
            gold1.Opacity = 100;
            gold1.Stretch = Stretch.UniformToFill;

            Ellipse gold2 = new Ellipse();
            gold2.Width = 40;
            gold2.Height = 40;
            gold2.Margin = new Thickness(180);
            gold2.Fill = Brushes.Yellow;
            gold2.Stroke = Brushes.Black;
            gold2.StrokeThickness = 1;
            gold2.Opacity = 100;
            gold2.Stretch = Stretch.UniformToFill;

            Ellipse spider = new Ellipse();
            spider.Width = 1;
            spider.Height = 1;
            spider.Margin = new Thickness(200);
            spider.Fill = Brushes.Black;
            spider.Stroke = Brushes.Black;
            spider.StrokeThickness = 1;
            spider.Opacity = 100;
            spider.Stretch = Stretch.UniformToFill;

            targetGraphics.Add(white1);
            targetGraphics.Add(white2);
            targetGraphics.Add(black1);
            targetGraphics.Add(black2);
            targetGraphics.Add(blue1);
            targetGraphics.Add(blue2);
            targetGraphics.Add(red1);
            targetGraphics.Add(red2);
            targetGraphics.Add(gold1);
            targetGraphics.Add(gold2);
            targetGraphics.Add(spider);
            AddTargetGraphic();
        }
    }
}
