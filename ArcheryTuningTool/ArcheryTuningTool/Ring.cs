using System.Collections;
using System.Collections.Generic;

namespace ArcheryTuningTool
{
    class Ring<T> : IEnumerable<T>, IEnumerator<T>
    {
        private int numItems;
        private int nHead;
        private T[] aRing;

        public T Current => throw new System.NotImplementedException();

        object IEnumerator.Current => throw new System.NotImplementedException();

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
    }
}