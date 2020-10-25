using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ArcheryTool
{
    class Ring<T> : IEnumerable<T>, IEnumerator<T>
    {
        protected int nElements;
        protected int nHead = -1;      //list has no items in it, so head is outside range of nElements
        protected T[] ring;

        public Ring(int nElements)
        {
            this.nElements = nElements;
            ring = new T[nElements];
        }

        public T Current => ring[nHead];

        public int GetHead()
        {
            return nHead;
        }

        public int GetSize()
        {
            int size = 0;
            for (int i = 0; i < ring.Length; i++)
            {
                if (ring[i] != null)
                    size++;
            }

            return size;
        }

        public virtual void Add(T element)
        {
            MoveNext();
            ring[nHead] = element;
        }

        public T this[int nIndex]
        {
            get { return ring[nIndex]; }
        }


        object IEnumerator.Current => ring[nHead];

        public void Dispose()
        {

        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            nHead++;
            if (nHead > nElements - 1)      //0 indexed to match array, resets to 0 instead of ticking over to nElements - 1
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

        public void Reset()
        {
            ResetHead();
            ring = new T[nElements];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public int GetNumElements()
        {
            return nElements;
        }
    }

    class UIRing<T> : Ring<T> where T : UIElement
    {
        public UIRing(int nElements) : base(nElements)
        {
            this.nElements = nElements;
            ring = new T[nElements];
        }

        public override void Add(T element)
        {
            MoveNext();
            ring[nHead] = (T)element;
        }

        public void SetNumElements(int newNElements)
        {
            if (newNElements > nElements)
            {
                nElements = newNElements;
            }
            else
            {
                T[] temp = new T[nElements];
                for(int i = 0; i < nElements; i++)
                {
                    temp[i] = Current;
                    MoveNext();
                }
                nElements = newNElements;
                ring = new T[nElements];
            }
        }
    }
}