using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcheryTool
{
    class Archer
    {
        int nScore;
        int nHits;
        int nTens;
        //TODO: add 2d array for the whole round to be able to show each arrow at end
        //dims based on round?

        public Archer()
        {
            nScore = 0;
            nHits = 0;
            nTens = 0;
        }

        public void AddToScore(int arrowScore)
        {
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
