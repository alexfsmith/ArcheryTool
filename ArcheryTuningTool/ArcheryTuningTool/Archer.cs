using System;

namespace ArcheryTool
{
    /// <summary>
    /// Represents 1 archer shooting a round
    /// Stores all information needed to populate the score sheet
    /// </summary>
    public class Archer
    {
        private int nScore;
        private int nTotalHits;
        private int nTotalTens;
        private int nDozenHits;
        private int nDozenTens;
        private string[,] table;
        private int nEnd;
        private int nCol;
        private int nRow;

        public Archer()
        {
            nEnd = 1;
            nScore = 0;
            nTotalHits = 0;
            nTotalTens = 0;
            nDozenHits = 0;
            nDozenTens = 0;
            table = new string[18,5];
            nCol = 0;
            nRow = 0;
        }
        
        //Adds summary statistics after each (half) dozen
        public void FinishEnd()
        {
            if (nEnd % 2 == 0)        //every second end add a half dozen subtotal
            {
                int subtotal = int.Parse(table[nCol - 1, nRow]) + int.Parse(table[nCol - 2, nRow]) + int.Parse(table[nCol - 3, nRow]) + int.Parse(table[nCol - 4, nRow]) + int.Parse(table[nCol - 5, nRow]) + int.Parse(table[nCol - 6, nRow]);
                table[nCol, nRow] = subtotal.ToString();
                nCol++;
            }

            if(nEnd % 4 == 0)       //every fourth add a half dozen subtotal, dozen score, dozen hits, dozen tens, subtotal
            {
                int dozen = int.Parse(table[6, nRow]) + int.Parse(table[13, nRow]);
                table[nCol, nRow] = dozen.ToString();
                nCol++;
                table[nCol, nRow] = nDozenHits.ToString();
                nCol++;
                table[nCol, nRow] = nDozenTens.ToString();
                nCol++;
                table[nCol, nRow] = nScore.ToString();
                nDozenHits = 0;
                nDozenTens = 0;
                nCol = 0;
                nRow++;
            }

            nEnd++;
        }

        //Adds individual arrow values
        public void AddToScore(int arrowScore)
        {
            table[nCol, nRow] = arrowScore.ToString();
            nScore += arrowScore;
            nCol++;
        }

        public void AddHit()
        {
            nDozenHits++;
            nTotalHits++;
        }

        public void AddTen()
        {
            nDozenTens++;
            nTotalTens++;
        }

        public int GetTotalHits()
        {
            return nTotalHits;
        }

        public int GetTotalTens()
        {
            return nTotalTens;
        }

        public String[,] GetTable()
        {
            return table;
        }
    }
}
