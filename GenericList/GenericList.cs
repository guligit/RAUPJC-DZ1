using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericList
{
    public interface IGenericList<X> : IEnumerable<X>
    {
        void Add(X item);
        bool Remove(X item);
        bool RemoveAt(int index);
        X GetElement(int index);
        int IndexOf(X item);
        int Count { get; }
        void Clear();
        bool Contains(X item);
    }

    public class GenericList<X> : IGenericList<X>
    {
        private int _count;
        private X[] _internalStorage;

      
        public GenericList(int initialSize = 4) 
        {
            if (initialSize <= 0)
            {
                throw new ArgumentException("Initial size has to be grater than zero.");
            }
            else
            {
                _internalStorage = new X[initialSize];
            }

        }

        public int Count
        {
            get
            {
                return _count; 
            }
        }

        public void Add(X item)
        {
            int arrayLength = _internalStorage.Length;
            if (_count >= arrayLength)
            {
                X[] temp = new X[arrayLength * 2];
                Array.Copy(_internalStorage, temp, arrayLength);
                _internalStorage = new X[arrayLength * 2];
                _internalStorage = temp;
            }

            _internalStorage[_count++] = item;
        }

        public void Clear()
        {
            Array.Clear(_internalStorage, 0, _count);
            _count = 0;
        }

        public bool Contains(X item)
        {
            for (int i = 0; i < _count; ++i)
            {
                if (_internalStorage[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public X GetElement(int index)
        {
            if (index >= 0 && index <= _count)
            {
                return _internalStorage[index];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public IEnumerator<X> GetEnumerator()
        {
            return new GenericListEnumerator<X>(this);
        }

        public int IndexOf(X item)
        {
            for (int i = 0; i < _count; ++i)
            {
                if (_internalStorage[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Remove(X item)
        {
            for (int i = 0; i < _count; ++i)
            {
                if (_internalStorage[i].Equals(item))
                {
                    RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public bool RemoveAt(int index)
        {
            if (index > _count)
            {
                return false;
            }
            int i;
            for (i = index; i < _count - 1; ++i)
            {
                _internalStorage[i] = _internalStorage[i + 1];
            }

            Array.Clear(_internalStorage, i, 1);
            _count--;
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public class GenericListEnumerator<T> : IEnumerator<T>
        {
            private int currentIndex;
            private GenericList<T> _collection;
            private T curT;

            public GenericListEnumerator (GenericList<T> collection) 
            {
                _collection = collection;
                currentIndex = -1;
                curT = default(T);
            }

            public T Current
            {
                get
                {
                    return curT;
                }
            }

            T IEnumerator<T>.Current
            {
                get
                {
                    return Current;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                if (++currentIndex >= _collection.Count)
                {
                    return false;
                }
                else
                {
                    curT = _collection.GetElement(currentIndex);
                    return true;
                }
            }

            public void Reset()
            {
                currentIndex = -1;
            }
        }
    }
}
