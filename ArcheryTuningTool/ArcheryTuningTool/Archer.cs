using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArcheryTool
{
    class Archer
    {
        private int nScore;
        private int nHits;
        private int nTens;
        private DataTable table;
        private Tuple<int, int> currentCell;
        private int nEnd;
        private DataRow currentRow;

        public Archer()
        {
            nEnd = 1;
            nScore = 0;
            nHits = 0;
            nTens = 0;
            table = new DataTable();
            currentCell = new Tuple<int, int>(0, 0);
            SetupTable();
        }

        private void SetupTable()
        {
            for (int i = 0; i < 18; i++)
            {
                table.Columns.Add(new DataColumn("", System.Type.GetType("System.Int32")));
            }
            currentRow = table.NewRow();
        }
        
        public DataTable GetData()
        {
            return table;
        }

        public void FinishEnd()
        {
            int col = currentCell.Item1;
            int row = currentCell.Item2;
            DataRow dataRow = currentRow;

            if (nEnd % 2 == 0)        //every second end add a half dozen score, every fourth add a half dozen score, dozen score, hits, tens, total
            {
                dataRow[col] = (int)dataRow[col - 6] + (int)dataRow[col - 5] + (int)dataRow[col - 4] + (int)dataRow[col - 3] + (int)dataRow[col - 2] + (int)dataRow[col - 1];
                col++;
            }

            if(nEnd % 4 == 0)
            {
                dataRow[col] = (int)dataRow[6] + (int)dataRow[13];
                col++;
                dataRow[col] = nHits;
                col++;
                dataRow[col] = nTens;
                col++;
                dataRow[col] = nScore;
                currentRow = table.NewRow();
                col = 0;
                row++;
            }

            currentCell = new Tuple<int, int>(col, row);
            nEnd++;
        }

        public void AddToScore(int arrowScore)
        {
            int col = currentCell.Item1;
            int row = currentCell.Item2;
            DataRow dataRow = currentRow;
            dataRow[col] = arrowScore;
            currentCell = new Tuple<int, int>(col + 1, row);
            nScore += arrowScore;
        }

        public void AddHit()
        {
            nHits ++;
        }

        public void AddTen()
        {
            nTens ++;
        }

        public int GetScore()
        {
            return nScore;
        }

        public int GetHits()
        {
            return nHits;
        }

        public int GetTens()
        {
            return nTens;
        }

    }
}
