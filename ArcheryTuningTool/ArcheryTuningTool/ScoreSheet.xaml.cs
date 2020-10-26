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

        public ScoreSheet(DataTable data, ScoreEntry se)
        {
            InitializeComponent();
            this.data = data;
            this.se = se;

            SetupGrid();

        }

        private void SetupGrid()
        {
            if (data != null) 
            {
                foreach (DataColumn col in data.Columns)
                {
                    gridScore.Columns.Add(
                        new DataGridTextColumn
                        {
                            Header = col.ColumnName,
                            Binding = new Binding(string.Format("[{0}]", col.ColumnName))
                        });
                }

                gridScore.DataContext = data;
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
}
