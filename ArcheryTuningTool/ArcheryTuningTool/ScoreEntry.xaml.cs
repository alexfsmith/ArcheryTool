using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ArcheryTuningTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ScoreEntry : Window
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
        private int nArrows;
        private Ring<FletchedGraphic> lArrows;

        public ScoreEntry()
        {
            InitializeComponent();
            SetupTenZoneTargetGraphics();
            lArrows = new Ring<FletchedGraphic>(nArrows);
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

            AddTargetGraphic();
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
        }
        private bool Validate()
        {
            return false;
        }

        private void Target_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePos = e.GetPosition(target);
            FletchedGraphic arrow = new FletchedGraphic(mousePos);
            lArrows.Add(arrow);

            UpdateTargetChildren();
        }

        private void CbBowStyle_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void BnOk_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateTargetChildren()
        {
            target.Children.Clear();
            AddTargetGraphic();

            if (lArrows.GetSize() > 0)
            {
                lArrows.ResetHead();
                for (int i = 0; i < lArrows.GetNumElements(); i++)
                {
                    FletchedGraphic arrow = (FletchedGraphic)lArrows[i];
                    if (arrow != null)
                        target.Children.Add(arrow);      //ienumerator will go through the whole array even if elements are null
                    lArrows.MoveNext();
                }
            }
        }

        private void AddTargetGraphic()
        {
            foreach (Ellipse ellipse in targetGraphics)
            {
                target.Children.Add(ellipse);
            }
        }
    }
}