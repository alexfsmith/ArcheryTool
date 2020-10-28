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
    /// Interaction logic for ScoreEntry.xaml
    /// Window for user to log each end of arrow scores/positions
    /// </summary>
    public partial class ScoreEntry : Window
    {
        private List<Ellipse> targetGraphics;
        private UIRing<FletchedGraphic> lArrows;
        private RoundSelect rs;

        private ERound eRound;
        private EBowStyle eBowStyle;
        private int nArchers;
        private int nArrows;
        private Archer archer;
        private Ring<int> tempScore;
        private List<int> endScore;
        private int nEnd;
        private int nTotalEnds;
        private string[] validScores = { "M", "m", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };        //TODO: Add X for 10, and 11 for relevant imperial rounds

        public ScoreEntry(ERound round, EBowStyle bowStyle, int archers, RoundSelect rs)
        {
            InitializeComponent();

            this.rs = rs;
            eRound = round;
            eBowStyle = bowStyle;
            nArchers = archers;
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

        //Calculate which ring the arrow is inside
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

            for (int i = 0; i < radii.Length; i++)
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

        private void ClearArrowsFromTarget()
        {
            lArrows.Reset();
            UpdateTargetChildren();
        }

        //Check scores entered are not illegal (eg score > 10, letters)
        private bool Validate()
        {
            List<String> toCheck = new List<string>();                  //TODO: Generalise for multiple archers/different nArrows
            toCheck.Add(tbArch1Arr1.Text);
            toCheck.Add(tbArch1Arr2.Text);
            toCheck.Add(tbArch1Arr3.Text);

            bool bValid = false;
            foreach (string check in toCheck)
            {
                foreach (string s in validScores)
                {
                    if (check == s)
                    {
                        bValid = true;
                        break;
                    }
                }

                if (!bValid)
                {
                    MessageBox.Show("Invalid score. Valid scores are M, m, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10", "Invalid Score", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
                bValid = false;
            }

            return true;
        }

        //Handlers
        //Displays scores as entered on canvas, ready for edits if necessary
        private void BnNextArcher_Click(object sender, RoutedEventArgs e)
        {
            if (nArchers == 1)
            {
                endScore = new List<int>(nArrows);
                for (int i = 0; i < tempScore.GetSize(); i++)
                {
                    endScore.Add(tempScore.Current);
                    tempScore.MoveNext();
                }
                endScore.Sort();

                if (endScore[0] == 0)                                    //TODO: generalise for number of arrows and archers
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

        //Checks arrow scores and stores them, shows the completed score sheet when round is over
        private void BnOk_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                endScore.Clear();
                //take account of potential edits

                if (tbArch1Arr1.Text == "M" || tbArch1Arr1.Text == "m")
                    endScore.Add(0);
                if (tbArch1Arr2.Text == "M" || tbArch1Arr2.Text == "m")
                    endScore.Add(0);
                if (tbArch1Arr3.Text == "M" || tbArch1Arr3.Text == "m")
                    endScore.Add(0);

                if (int.TryParse(tbArch1Arr1.Text, out int arrow))      //should only fail if M/m as Vaidate checks inputs
                    endScore.Add(arrow);
                if (int.TryParse(tbArch1Arr2.Text, out arrow))
                    endScore.Add(arrow);
                if (int.TryParse(tbArch1Arr3.Text, out arrow))
                    endScore.Add(arrow);

                foreach (int score in endScore)
                {
                    archer.AddToScore(score);
                    if (score != 0)
                        archer.AddHit();
                    if (score == 10)
                        archer.AddTen();
                }

                archer.FinishEnd();

                ClearArrowsFromTarget();                //make it clear the score has been accepted
                EmptyScoreEntry();

                if (nEnd < nTotalEnds)
                {
                    nEnd++;
                    labelEnd.Content = "End " + nEnd + " of " + nTotalEnds;
                }
                else
                {
                    this.Visibility = Visibility.Hidden;
                    ScoreSheet scoreSheet = new ScoreSheet(archer, this);
                    scoreSheet.ShowDialog();
                }
            }
        }

        private void Target_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePos = e.GetPosition(target);
            FletchedGraphic arrow = new FletchedGraphic(mousePos);
            lArrows.Add(arrow);

            tempScore.Add(GetScore(mousePos));

            UpdateTargetChildren();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            rs.Close();
        }

        //Control setup
        //Hides/Shows score entry textboxes based on number of arrows for round
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

        //Resets content of score entry textboxes to show score entered
        private void EmptyScoreEntry()
        {
            tbArch1Arr1.Text = "";
            tbArch1Arr2.Text = "";
            tbArch1Arr3.Text = "";
            tbArch1Arr4.Text = "";
            tbArch1Arr5.Text = "";
            tbArch1Arr6.Text = "";

            tbArch2Arr1.Text = "";
            tbArch2Arr2.Text = "";
            tbArch2Arr3.Text = "";
            tbArch2Arr4.Text = "";
            tbArch2Arr5.Text = "";
            tbArch2Arr6.Text = "";

            tbArch3Arr1.Text = "";
            tbArch3Arr2.Text = "";
            tbArch3Arr3.Text = "";
            tbArch3Arr4.Text = "";
            tbArch3Arr5.Text = "";
            tbArch3Arr6.Text = "";

            tbArch4Arr1.Text = "";
            tbArch4Arr2.Text = "";
            tbArch4Arr3.Text = "";
            tbArch4Arr4.Text = "";
            tbArch4Arr5.Text = "";
            tbArch4Arr6.Text = "";
        }

        //Target graphics
        private void UpdateTargetChildren()
        {
            target.Children.Clear();
            AddTargetGraphic();

            if (lArrows.GetNumElements() > 0)
            {
                int head = lArrows.GetHead();
                lArrows.ResetHead();
                for (int i = 0; i < lArrows.GetSize(); i++)
                {
                    FletchedGraphic arrow = (FletchedGraphic)lArrows[i];
                    if (arrow != null)
                        target.Children.Add(arrow);      //ienumerator will go through the whole array even if elements are null
                    lArrows.MoveNext();
                }
                lArrows.SetHead(head);
            }
        }

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

        //Draw circles for a standard 5-zone target face
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

        //Draw circles for a standard 10-zone target face
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