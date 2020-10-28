using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace ArcheryTool
{
    class Ring<T> : IEnumerable<T>, IEnumerator<T>
    {
        protected int nSize;
        protected int nHead = -1;      //list has no items in it, so head is outside range of nElements
        protected T[] ring;

        public Ring(int nSize)
        {
            this.nSize = nSize;
            ring = new T[nSize];
        }

        public virtual void Add(T element)
        {
            MoveNext();
            ring[nHead] = element;
        }

        public bool MoveNext()
        {
            nHead++;
            if (nHead > nSize - 1)      //0 indexed to match array, resets to 0 instead of ticking over to nElements - 1
                nHead = 0;
            return true;
        }

        public void SetHead(int element)
        {
            nHead = element;
        }

        public void ResetHead()
        {
            nHead = -1;
        }

        public void Reset()         //Fully reset the ring
        {
            ResetHead();
            ring = new T[nSize];
        }

        //Getters
        public int GetHead()
        {
            return nHead;
        }

        public int GetNumElements()
        {
            int elements = 0;
            for (int i = 0; i < ring.Length; i++)
            {
                if (ring[i] != null)
                    elements++;
            }

            return elements;
        }

        public int GetSize()
        {
            return nSize;
        }

        public T Current => ring[nHead];

        object IEnumerator.Current => ring[nHead];

        public T this[int nIndex]
        {
            get { return ring[nIndex]; }
        }
        
        //Interface required methods
        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public void Dispose()
        {

        }       
    }

    class UIRing<T> : Ring<T> where T : UIElement
    {
        public UIRing(int nSize) : base(nSize)
        {
            this.nSize = nSize;
            ring = new T[nSize];
        }

        public override void Add(T element)
        {
            MoveNext();
            ring[nHead] = (T)element;
        }

        public void SetNumElements(int newSize)         //For changing between nArrows values, mainly in Tuning
        {
            if (newSize > nSize)
            {
                nSize = newSize;
            }
            else
            {
                T[] temp = new T[nSize];
                for(int i = 0; i < nSize; i++)
                {
                    temp[i] = Current;
                    MoveNext();
                }
                nSize = newSize;
                ring = new T[nSize];
            }
        }
    }
}