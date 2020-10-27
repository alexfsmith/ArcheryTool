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
        private string[,] table;
        private int nEnd;
        private int nCol;
        private int nRow;

        public Archer()
        {
            nEnd = 1;
            nScore = 0;
            nHits = 0;
            nTens = 0;
            table = new string[18,5];
            nCol = 0;
            nRow = 0;
        }

        public String[,] GetTable()
        {
            return table;
        }
        
        public void FinishEnd()
        {
            if (nEnd % 2 == 0)        //every second end add a half dozen score, every fourth add a half dozen score, dozen score, hits, tens, total
            {
                int subtotal = int.Parse(table[nCol - 1, nRow]) + int.Parse(table[nCol - 2, nRow]) + int.Parse(table[nCol - 3, nRow]) + int.Parse(table[nCol - 4, nRow]) + int.Parse(table[nCol - 5, nRow]) + int.Parse(table[nCol - 6, nRow]);
                table[nCol, nRow] = subtotal.ToString();
                nCol++;
            }

            if(nEnd % 4 == 0)
            {
                int dozen = int.Parse(table[6, nRow]) + int.Parse(table[13, nRow]);
                table[nCol, nRow] = dozen.ToString();
                nCol++;
                table[nCol, nRow] = nHits.ToString();                                   //TODO: change to hits, tens per dozen with separate total at bottom
                nCol++;
                table[nCol, nRow] = nTens.ToString();
                nCol++;
                table[nCol, nRow] = nScore.ToString();
                nCol = 0;
                nRow++;
            }

            nEnd++;
        }

        public void AddToScore(int arrowScore)
        {
            table[nCol, nRow] = arrowScore.ToString();
            nScore += arrowScore;
            nCol++;
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
