using ArcheryTool;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace ArcheryTuningTool
{
    /// <summary>
    /// Interaction logic for ScoreSheet.xaml
    /// </summary>
    public partial class ScoreSheet : Window
    {
        private DataTable data;
        private ScoreEntry se;
        private List<ScoreSheetRow> scoreSheetRows;

        public ScoreSheet(DataTable data, ScoreEntry se)
        {
            InitializeComponent();
            this.data = data;
            this.se = se;

            scoreSheetRows = new List<ScoreSheetRow>();
            scoreSheetRows.Add(new ScoreSheetRow(data.Rows[0]));
            scoreSheetRows.Add(new ScoreSheetRow(data.Rows[1]));
            scoreSheetRows.Add(new ScoreSheetRow(data.Rows[2]));
            scoreSheetRows.Add(new ScoreSheetRow(data.Rows[3]));
            scoreSheetRows.Add(new ScoreSheetRow(data.Rows[4]));

            SetupGrid(); 

        }

        private void SetupGrid()
        {
            if (data != null) 
            {
                gridScore.DataContext = data;

                int count = 0;
                foreach (DataColumn col in data.Columns)
                {
                    if (count < 14)
                    {
                        gridScore.Columns.Add(
                            new DataGridTextColumn
                            {
                                Header = col.ColumnName,
                                //Binding = new Binding(string.Format("[{0}]", col.ColumnName)),           //TODO: make this display the datam give it column names
                                Width = 28
                            });
                    }
                    else if(count == 14)
                    {
                        gridScore.Columns.Add(
                            new DataGridTextColumn
                            {
                                Header = col.ColumnName,
                                //Binding = new Binding(string.Format("[{0}]", col.ColumnName)),
                                Width = 50
                            });
                    }
                    else
                    {
                        gridScore.Columns.Add(
                            new DataGridTextColumn
                            {
                                Header = col.ColumnName,
                                //Binding = new Binding(string.Format("[{0}]", col.ColumnName)),
                                Width = 40
                            });
                    }
                    count++;
                }

                gridScore.ItemsSource = scoreSheetRows;

                /*
                for(int i = 0; i < 5; i++)
                {
                    //DataGridRow row;
                    gridScore.Items.Add(data.Rows[i]);

                    //row["Column1"] = data.Rows[i][0];
                    //row["Column2"] = data.Rows[i][1];
                    //row["Column3"] = data.Rows[i][2];
                    //row["Column4"] = data.Rows[i][3];
                    //row["Column5"] = data.Rows[i][4];
                    //row["Column6"] = data.Rows[i][5];
                    //row["Column7"] = data.Rows[i][6];
                    //row["Column8"] = data.Rows[i][7];
                    //row["Column9"] = data.Rows[i][8];
                    //row["Column10"] = data.Rows[i][9];
                    //row["Column11"] = data.Rows[i][10];
                    //row["Column12"] = data.Rows[i][11];
                    //row["Column13"] = data.Rows[i][12];
                    //row["Dozen"] = data.Rows[i][13];
                    //row["Hits"] = data.Rows[i][14];
                    //row["Tens"] = data.Rows[i][15];
                    //row["Total"] = data.Rows[i][16];

                    //data.Rows.Add(row);
                }*/

                /*Binding binding = new Binding();

                binding.Source = data;
                gridScore.SetBinding(DataGrid.ItemsSourceProperty, binding);

                for (int i = 0; i < 5; i++)
                {
                    gridScore.Items.Add(new DataRow());

                }*/

            }


            /*for (int i = 0; i < nCols; i++)
            {
                DataGridTextColumn textColumn = new DataGridTextColumn();

                if (i == 14)
                {
                    textColumn.Header = "Hits";
                    textColumn.Binding = new Binding("Hits");
                }
                if (i == 15)
                {
                    textColumn.Header = "Tens";
                    textColumn.Binding = new Binding("Tens");
                }
                if (i == 16)
                {
                    textColumn.Header = "Total";
                    textColumn.Binding = new Binding("Total");
                }
                
                gridScore.Columns.Add(textColumn);
            }*/
        }

        private void CreateRows()
        {

        }

        private void BnSave_Click(object sender, RoutedEventArgs e)
        {
            //TODO
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

    public class ScoreSheetRow
    {
        private object[] row;
        private int arr1;
        private int arr2;
        private int arr3;
        private int arr4;
        private int arr5;
        private int arr6;
        private int sub1;
        private int arr7;
        private int arr8;
        private int arr9;
        private int arr10;
        private int arr11;
        private int arr12;
        private int sub2;
        private int dozen;
        private int hits;
        private int tens;
        private int total;

        public ScoreSheetRow(DataRow row)
        {
            this.row = row.ItemArray;
            Setup();
        }

        private void Setup()
        {
            arr1 = (int)row[0];
            arr2 = (int)row[1];
            arr3 = (int)row[2];
            arr4 = (int)row[3];
            arr5 = (int)row[4];
            arr6 = (int)row[5];
            sub1 = (int)row[6];
            arr7 = (int)row[7];
            arr8 = (int)row[8];
            arr9 = (int)row[9];
            arr10 = (int)row[10];
            arr11 = (int)row[11];
            arr12 = (int)row[12];
            sub2 = (int)row[13];
            dozen = (int)row[14];
            hits = (int)row[15];
            tens = (int)row[16];
            total = (int)row[17];
        }
    }

}
