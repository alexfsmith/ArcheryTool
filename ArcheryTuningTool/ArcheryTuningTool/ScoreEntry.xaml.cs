using ArcheryTuningTool;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using static ArcheryTool.RoundSelect;

namespace ArcheryTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ScoreEntry : Window
    {
        private ERound eRound;
        private EBowStyle eBowStyle;
        private int nArchers;
        private bool bSighters;
        private RoundSelect rs;
        private List<Ellipse> targetGraphics;
        private int nArrows;
        private UIRing<FletchedGraphic> lArrows;
        private Archer archer;
        private Ring<int> tempScore;
        private List<int> endScore;
        private int nEnd;
        private int nTotalEnds;

        public ScoreEntry(ERound round, EBowStyle bowStyle, int archers, bool sighters, RoundSelect rs)
        {
            InitializeComponent();

            this.rs = rs;
            eRound = round;
            eBowStyle = bowStyle;
            nArchers = archers;
            bSighters = sighters;
            if (round == ERound.Fita18 || round == ERound.Portsmouth)
            {
                nArrows = 3;
                nTotalEnds = 20;
            }
            else
            {
                nArrows = 6;
                if (round == ERound.WA1440)
                    nTotalEnds = 24;
                else
                    nTotalEnds = 12;
            }
            nEnd = 1;
            labelEnd.Content = "End " + nEnd + " of " + nTotalEnds;
            lArrows = new UIRing<FletchedGraphic>(nArrows);

            if (nArchers == 1)
                bnNextArcher.Content = "Score";

            archer = new Archer();       //TODO: add more depending on nArchers
            tempScore = new Ring<int>(nArrows);

            SetupTargetGraphics();
            SetupScoreEntryBoxes();
        }

        
        private bool Validate()
        {
            //TODO
            return true;
        }

        private void Target_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePos = e.GetPosition(target);
            FletchedGraphic arrow = new FletchedGraphic(mousePos);
            lArrows.Add(arrow);

            tempScore.Add(GetScore(mousePos));

            UpdateTargetChildren();
        }

        private int GetScore(Point point)
        {
            int[] radii = new int[10];

            radii[9] = 200;
            radii[8] = 180;
            radii[7] = 160;
            radii[6] = 140;
            radii[5] = 120;
            radii[4] = 100;
            radii[3] = 80;
            radii[2] = 60;
            radii[1] = 40;
            radii[0] = 20;

            Point centre = new Point(210, 210);

            for (int i =0; i< radii.Length; i++)
            {
                double xdiff = point.X - centre.X;
                double ydiff = point.Y - centre.Y;
                double dist = Math.Sqrt(xdiff * xdiff + ydiff * ydiff);
                if (dist <= radii[i])
                {
                    return 10 - i;
                }
            }
            
            return 0;       //miss
        }

        private void BnNextArcher_Click(object sender, RoutedEventArgs e)
        {
            if(nArchers==1)
            {
                endScore = new List<int>(nArrows);
                for(int i=0; i < tempScore.GetNumElements(); i++)
                {
                    endScore.Add(tempScore.Current);
                    tempScore.MoveNext();
                }
                endScore.Sort();

                if (endScore[0] == 0)                                    //TODO: generalise
                    tbArch1Arr1.Text = "M";
                else
                    tbArch1Arr1.Text = endScore[0].ToString();
                if (endScore[1] == 0)
                    tbArch1Arr2.Text = "M";
                else
                    tbArch1Arr2.Text = endScore[1].ToString();
                if (endScore[2] == 0)
                    tbArch1Arr3.Text = "M";
                else
                    tbArch1Arr3.Text = endScore[2].ToString();

            }
        }

        private void BnEdit_Click(object sender, RoutedEventArgs e)
        {
            tbArch1Arr1.IsEnabled = true;
            tbArch1Arr2.IsEnabled = true;
            tbArch1Arr3.IsEnabled = true;
        }

        private void BnOk_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;
            endScore.Clear();
            //take account of edits

            if (tbArch1Arr1.Text == "M")
                endScore.Add(0);
            if (tbArch1Arr2.Text == "M")
                endScore.Add(0);
            if (tbArch1Arr3.Text == "M")
                endScore.Add(0);

            int arrow;
            if(int.TryParse(tbArch1Arr1.Text, out arrow))
                endScore.Add(arrow);
            if (int.TryParse(tbArch1Arr2.Text, out arrow))
                endScore.Add(arrow);
            if (int.TryParse(tbArch1Arr3.Text, out arrow))
                endScore.Add(arrow);

            foreach(int score in endScore)
            {
                archer.AddToScore(score);
                if (score != 0)
                    archer.AddHit();
                if (score == 10)
                    archer.AddTen();
            }

            archer.FinishEnd();

            if (nEnd < nTotalEnds)
            {
                nEnd++;
                labelEnd.Content = "End " + nEnd + " of " + nTotalEnds;
            }
            else
            {
                this.Visibility = Visibility.Hidden;
                ScoreSheet scoreSheet = new ScoreSheet(archer.GetTable(), this);
                scoreSheet.ShowDialog();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            rs.Close();
        }

        private void SetupScoreEntryBoxes()
        {
            if(eRound == ERound.Fita18 || eRound == ERound.Portsmouth)
            {
                labelArr4.Visibility = Visibility.Hidden;
                labelArr5.Visibility = Visibility.Hidden;
                labelArr6.Visibility = Visibility.Hidden;

                tbArch1Arr4.Visibility = Visibility.Hidden;
                tbArch1Arr5.Visibility = Visibility.Hidden;
                tbArch1Arr6.Visibility = Visibility.Hidden;

                tbArch2Arr4.Visibility = Visibility.Hidden;
                tbArch2Arr5.Visibility = Visibility.Hidden;
                tbArch2Arr6.Visibility = Visibility.Hidden;

                tbArch3Arr4.Visibility = Visibility.Hidden;
                tbArch3Arr5.Visibility = Visibility.Hidden;
                tbArch3Arr6.Visibility = Visibility.Hidden;

                tbArch4Arr4.Visibility = Visibility.Hidden;
                tbArch4Arr5.Visibility = Visibility.Hidden;
                tbArch4Arr6.Visibility = Visibility.Hidden;

                this.Width = 900;
            }
            else
            {
                labelArr4.Visibility = Visibility.Visible;
                labelArr5.Visibility = Visibility.Visible;
                labelArr6.Visibility = Visibility.Visible;

                tbArch1Arr4.Visibility = Visibility.Visible;
                tbArch1Arr5.Visibility = Visibility.Visible;
                tbArch1Arr6.Visibility = Visibility.Visible;

                tbArch2Arr4.Visibility = Visibility.Visible;
                tbArch2Arr5.Visibility = Visibility.Visible;
                tbArch2Arr6.Visibility = Visibility.Visible;

                tbArch3Arr4.Visibility = Visibility.Visible;
                tbArch3Arr5.Visibility = Visibility.Visible;
                tbArch3Arr6.Visibility = Visibility.Visible;

                tbArch4Arr4.Visibility = Visibility.Visible;
                tbArch4Arr5.Visibility = Visibility.Visible;
                tbArch4Arr6.Visibility = Visibility.Visible;

                this.Width = 1100;
            }
        }

        private void UpdateTargetChildren()
        {
            target.Children.Clear();
            AddTargetGraphic();

            if (lArrows.GetSize() > 0)
            {
                int head = lArrows.GetHead();
                lArrows.ResetHead();
                for (int i = 0; i < lArrows.GetNumElements(); i++)
                {
                    FletchedGraphic arrow = (FletchedGraphic)lArrows[i];
                    if (arrow != null)
                        target.Children.Add(arrow);      //ienumerator will go through the whole array even if elements are null
                    lArrows.MoveNext();
                }
                lArrows.SetHead(head);
            }
        }

        //Target Graphics
        private void AddTargetGraphic()
        {
            foreach (Ellipse ellipse in targetGraphics)
            {
                target.Children.Add(ellipse);
            }
        }

        private void SetupTargetGraphics()
        {
            if (eRound == ERound.Fita18 && eBowStyle == EBowStyle.Compound)
                SetupFiveZoneGraphics();
            else
                SetupTenZoneGraphics();
        }

        private void SetupFiveZoneGraphics()
        {
            targetGraphics = new List<Ellipse>();

            Ellipse blue1 = new Ellipse();
            blue1.Width = 400;
            blue1.Height = 400;
            blue1.Margin = new Thickness(10);
            blue1.Fill = Brushes.Aqua;
            blue1.Stroke = Brushes.Black;
            blue1.StrokeThickness = 1;

            Ellipse blue2 = new Ellipse();
            blue2.Width = 330;
            blue2.Height = 330;
            blue2.Margin = new Thickness(45);
            blue2.Fill = Brushes.Aqua;
            blue2.Stroke = Brushes.Black;
            blue2.StrokeThickness = 1;

            Ellipse red1 = new Ellipse();
            red1.Width = 260;
            red1.Height = 260;
            red1.Margin = new Thickness(80);
            red1.Fill = Brushes.Red;
            red1.Stroke = Brushes.Black;
            red1.StrokeThickness = 1;

            Ellipse red2 = new Ellipse();
            red2.Width = 190;
            red2.Height = 190;
            red2.Margin = new Thickness(115);
            red2.Fill = Brushes.Red;
            red2.Stroke = Brushes.Black;
            red2.StrokeThickness = 1;

            Ellipse gold1 = new Ellipse();
            gold1.Width = 120;
            gold1.Height = 120;
            gold1.Margin = new Thickness(150);
            gold1.Fill = Brushes.Yellow;
            gold1.Stroke = Brushes.Black;
            gold1.StrokeThickness = 1;

            Ellipse gold2 = new Ellipse();
            gold2.Width = 50;
            gold2.Height = 50;
            gold2.Margin = new Thickness(185);
            gold2.Fill = Brushes.Yellow;
            gold2.Stroke = Brushes.Black;
            gold2.StrokeThickness = 1;

            Ellipse spider = new Ellipse();
            spider.Width = 1;
            spider.Height = 1;
            spider.Margin = new Thickness(210);
            spider.Fill = Brushes.Black;
            spider.Stroke = Brushes.Black;
            spider.StrokeThickness = 1;

            targetGraphics.Add(blue1);
            targetGraphics.Add(blue2);
            targetGraphics.Add(red1);
            targetGraphics.Add(red2);
            targetGraphics.Add(gold1);
            targetGraphics.Add(gold2);
            targetGraphics.Add(spider);

            AddTargetGraphic();
        }

        private void SetupTenZoneGraphics()
        {
            targetGraphics = new List<Ellipse>();

            Ellipse white1 = new Ellipse();
            white1.Width = 400;
            white1.Height = 400;
            white1.Margin = new Thickness(10);
            white1.Fill = Brushes.White;
            white1.Stroke = Brushes.Black;
            white1.StrokeThickness = 1;

            Ellipse white2 = new Ellipse();
            white2.Width = 360;
            white2.Height = 360;
            white2.Margin = new Thickness(30);
            white2.Fill = Brushes.White;
            white2.Stroke = Brushes.Black;
            white2.StrokeThickness = 1;

            Ellipse black1 = new Ellipse();
            black1.Width = 320;
            black1.Height = 320;
            black1.Margin = new Thickness(50);
            black1.Fill = Brushes.Black;
            black1.Stroke = Brushes.White;
            black1.StrokeThickness = 1;

            Ellipse black2 = new Ellipse();
            black2.Width = 280;
            black2.Height = 280;
            black2.Margin = new Thickness(70);
            black2.Fill = Brushes.Black;
            black2.Stroke = Brushes.White;
            black2.StrokeThickness = 1;

            Ellipse blue1 = new Ellipse();
            blue1.Width = 240;
            blue1.Height = 240;
            blue1.Margin = new Thickness(90);
            blue1.Fill = Brushes.Aqua;
            blue1.Stroke = Brushes.Black;
            blue1.StrokeThickness = 1;

            Ellipse blue2 = new Ellipse();
            blue2.Width = 200;
            blue2.Height = 200;
            blue2.Margin = new Thickness(110);
            blue2.Fill = Brushes.Aqua;
            blue2.Stroke = Brushes.Black;
            blue2.StrokeThickness = 1;

            Ellipse red1 = new Ellipse();
            red1.Width = 160;
            red1.Height = 160;
            red1.Margin = new Thickness(130);
            red1.Fill = Brushes.Red;
            red1.Stroke = Brushes.Black;
            red1.StrokeThickness = 1;

            Ellipse red2 = new Ellipse();
            red2.Width = 120;
            red2.Height = 120;
            red2.Margin = new Thickness(150);
            red2.Fill = Brushes.Red;
            red2.Stroke = Brushes.Black;
            red2.StrokeThickness = 1;

            Ellipse gold1 = new Ellipse();
            gold1.Width = 80;
            gold1.Height = 80;
            gold1.Margin = new Thickness(170);
            gold1.Fill = Brushes.Yellow;
            gold1.Stroke = Brushes.Black;
            gold1.StrokeThickness = 1;

            Ellipse gold2 = new Ellipse();
            gold2.Width = 40;
            gold2.Height = 40;
            gold2.Margin = new Thickness(190);
            gold2.Fill = Brushes.Yellow;
            gold2.Stroke = Brushes.Black;
            gold2.StrokeThickness = 1;

            Ellipse spider = new Ellipse();
            spider.Width = 1;
            spider.Height = 1;
            spider.Margin = new Thickness(210);
            spider.Fill = Brushes.Black;
            spider.Stroke = Brushes.Black;
            spider.StrokeThickness = 1;

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