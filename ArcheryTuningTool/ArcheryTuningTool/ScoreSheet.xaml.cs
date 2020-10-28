using ArcheryTool;
using System.Windows;

namespace ArcheryTuningTool
{
    /// <summary>
    /// Interaction logic for ScoreSheet.xaml
    /// </summary>
    public partial class ScoreSheet : Window
    {
        private string[,] data;
        private ScoreEntry se;
        private int totHits;
        private int totTens;

        public ScoreSheet(Archer archer, ScoreEntry se)
        {
            InitializeComponent();

            totHits = archer.GetTotalHits();
            totTens = archer.GetTotalTens();
            this.data = archer.GetTable();
            this.se = se;

            Replace0WithM();

            FillGrid();
        }

        private void Replace0WithM()
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    if (data[i, j] == "0")
                        data[i, j] = "M";
                }
            }
        }

        private void FillGrid()                 //TODO: Finish work in main branch for using a DataTable/DataGrid so other round formats can be used
        {
            score1_1.Content = data[0, 0];
            score1_2.Content = data[1, 0];
            score1_3.Content = data[2, 0];
            score1_4.Content = data[3, 0];
            score1_5.Content = data[4, 0];
            score1_6.Content = data[5, 0];
            score1_7.Content = data[6, 0];
            score1_8.Content = data[7, 0];
            score1_9.Content = data[8, 0];
            score1_10.Content = data[9, 0];
            score1_11.Content = data[10, 0];
            score1_12.Content = data[11, 0];
            score1_13.Content = data[12, 0];
            score1_14.Content = data[13, 0];
            dozen1.Content = data[14, 0];
            hits1.Content = data[15, 0];
            tens1.Content = data[16, 0];
            total1.Content = data[17, 0];

            score2_1.Content = data[0, 1];
            score2_2.Content = data[1, 1];
            score2_3.Content = data[2, 1];
            score2_4.Content = data[3, 1];
            score2_5.Content = data[4, 1];
            score2_6.Content = data[5, 1];
            score2_7.Content = data[6, 1];
            score2_8.Content = data[7, 1];
            score2_9.Content = data[8, 1];
            score2_10.Content = data[9, 1];
            score2_11.Content = data[10, 1];
            score2_12.Content = data[11, 1];
            score2_13.Content = data[12, 1];
            score2_14.Content = data[13, 1];
            dozen2.Content = data[14, 1];
            hits2.Content = data[15, 1];
            tens2.Content = data[16, 1];
            total2.Content = data[17, 1];

            score3_1.Content = data[0, 2];
            score3_2.Content = data[1, 2];
            score3_3.Content = data[2, 2];
            score3_4.Content = data[3, 2];
            score3_5.Content = data[4, 2];
            score3_6.Content = data[5, 2];
            score3_7.Content = data[6, 2];
            score3_8.Content = data[7, 2];
            score3_9.Content = data[8, 2];
            score3_10.Content = data[9, 2];
            score3_11.Content = data[10, 2];
            score3_12.Content = data[11, 2];
            score3_13.Content = data[12, 2];
            score3_14.Content = data[13, 2];
            dozen3.Content = data[14, 2];
            hits3.Content = data[15, 2];
            tens3.Content = data[16, 2];
            total3.Content = data[17, 2];

            score4_1.Content = data[0, 3];
            score4_2.Content = data[1, 3];
            score4_3.Content = data[2, 3];
            score4_4.Content = data[3, 3];
            score4_5.Content = data[4, 3];
            score4_6.Content = data[5, 3];
            score4_7.Content = data[6, 3];
            score4_8.Content = data[7, 3];
            score4_9.Content = data[8, 3];
            score4_10.Content = data[9, 3];
            score4_11.Content = data[10, 3];
            score4_12.Content = data[11, 3];
            score4_13.Content = data[12, 3];
            score4_14.Content = data[13, 3];
            dozen4.Content = data[14, 3];
            hits4.Content = data[15, 3];
            tens4.Content = data[16, 3];
            total4.Content = data[17, 3];

            score5_1.Content = data[0, 4];
            score5_2.Content = data[1, 4];
            score5_3.Content = data[2, 4];
            score5_4.Content = data[3, 4];
            score5_5.Content = data[4, 4];
            score5_6.Content = data[5, 4];
            score5_7.Content = data[6, 4];
            score5_8.Content = data[7, 4];
            score5_9.Content = data[8, 4];
            score5_10.Content = data[9, 4];
            score5_11.Content = data[10, 4];
            score5_12.Content = data[11, 4];
            score5_13.Content = data[12, 4];
            score5_14.Content = data[13, 4];
            dozen5.Content = data[14, 4];
            hits5.Content = data[15, 4];
            tens5.Content = data[16, 4];
            total5.Content = data[17, 4];

            totalHits.Content = totHits.ToString();
            totalTens.Content = totTens.ToString();
            total.Content = data[17, 4];

        }

        //Handlers
        private void BnSave_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            //Save as csv for use in History 
             
        }

        private void BnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            se.Close();
        }
    }
}
