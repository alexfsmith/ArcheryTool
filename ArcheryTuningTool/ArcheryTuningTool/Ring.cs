using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ArcheryTool
{
    class Ring<T> : IEnumerable<UIElement>, IEnumerator<UIElement> where T:UIElement
    {
        private int nElements;
        private int nHead = -1;      //list has no items in it, so head is outside range of nElements
        private UIElement[] ring;

        public Ring(int nElements)
        {
            this.nElements = nElements;
            ring = new T[nElements];
        }

        public UIElement Current => ring[nHead];

        public int GetSize()
        {
            int size = 0;
            for(int i = 0; i < ring.Length; i++)
            {
                if (ring[i] != null)
                    size++;
            }

            return size;
        }

        public void Add(UIElement element)
        {
            MoveNext();
            ring[nHead] = element;
        }

        public UIElement this[int nIndex]
        {
            get { return ring[nIndex]; }
        }


        object IEnumerator.Current => ring[nHead];

        public void Dispose()
        {
        
        }

        public IEnumerator<UIElement> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            nHead++;
            if (nHead >= nElements - 1)      //0 indexed to match array, resets to 0 instead of ticking over to nElements - 1
                nHead = 0;
            return true;
        }

        public void ResetHead()
        {
            nHead = -1;
        }

        public void Reset()
        {
            ResetHead();
            ring = new UIElement[nElements];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public int GetNumElements()
        {
            return nElements;
        }

        public void SetNumElements(int newNElements)
        {
            if (newNElements > nElements)
            {
                nElements = newNElements;
            }
            else
            {
                UIElement[] temp = new UIElement[nElements];
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