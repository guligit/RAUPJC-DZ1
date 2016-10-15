using System;

namespace List
{
    public interface IIntegerList
    {
        /// <summary >
        /// Adds an item to the collection .
        /// </ summary >
        void Add(int item);

        /// <summary >
        /// Removes the first occurrence of an item from the collection .
        /// If the item was not found , method does nothing .
        /// </ summary >
        bool Remove(int item);

        /// <summary >
        /// Removes the item at the given index in the collection .
        /// </ summary >
        bool RemoveAt(int index);

        /// <summary >
        /// Returns the item at the given index in the collection .
        /// </ summary >
        int GetElement(int index);

        /// <summary >
        /// Returns the index of the item in the collection .
        /// If item is not found in the collection , method returns -1.
        /// </ summary >
        int IndexOf(int item);

        /// <summary >
        /// Readonly property . Gets the number of items contained in the collection.
        /// </ summary >
        int Count { get; }

        /// <summary >
        /// Removes all items from the collection .
        /// </ summary >
        void Clear();

        /// <summary >
        /// Determines whether the collection contains a specific value .
        /// </ summary >
        bool Contains(int item);
    }


    public class IntegerList : IIntegerList
    {
        private int[] _internalStorage;
        private int _count = 0;

        public int Count
        {
            get
            {
                return _count;
            }
        }


        public static void Main(String[] args)
        {
            IIntegerList listOfIntegers = new IntegerList();
            ListExample(listOfIntegers);
        }


        public IntegerList(int initialSize) : base()
        {
            if (initialSize <= 0)
            {
                throw new ArgumentException("Initial size has to be grater than zero.");
            }
            else
            {
                _internalStorage = new int[initialSize];
            }

        }

        public IntegerList() 
            : this(4)
         {
         }

        // ... IIntegerList implementation ...
        public void Add(int X)
        {
            int arrayLength = _internalStorage.Length;
            if (_count >= arrayLength)
            { 
                int[] temp = new int[arrayLength * 2];
                Array.Copy(_internalStorage, temp, arrayLength);
                _internalStorage = new int[arrayLength * 2];
                _internalStorage = temp;
            }

            _internalStorage[_count++] = X;
        }

        public bool Remove(int item)
        {
            for (int i = 0; i < _count; ++i)
            {
                if (_internalStorage[i] == item)
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
            _internalStorage[i] = 0;
            _count--;
            return true;
        }

        public int GetElement(int index)
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

        public int IndexOf(int item)
        {
           for (int i = 0; i < _count; ++i)
            {
                if (_internalStorage[i] == item)
                {
                    return i;
                }
            }

            return -1;
        }

        public void Clear()
        {
           for(int i = 0; i < _count; ++i)
            {
                _internalStorage[i] = 0;
            }
            _count = 0;
        }

        public bool Contains(int item)
        {
            for (int i = 0; i < _count; ++i)
            {
                if (_internalStorage[i] == item)
                {
                    return true;
                }
            }
            return false;
        }

        public static void ListExample(IIntegerList listOfIntegers)
        {
            listOfIntegers.Add(1); // [1]
            listOfIntegers.Add(2); // [1 ,2]
            listOfIntegers.Add(3); // [1 ,2 ,3]
            listOfIntegers.Add(4); // [1 ,2 ,3 ,4]
            listOfIntegers.Add(5); // [1 ,2 ,3 ,4 ,5]
            listOfIntegers.RemoveAt(0); // [2 ,3 ,4 ,5]
            listOfIntegers.Remove(5); 
            Console.WriteLine ( listOfIntegers.Count ) ; // 3
            Console.WriteLine ( listOfIntegers.Remove (100) ) ; // false
            Console.WriteLine ( listOfIntegers.RemoveAt (5) ) ; // false
            listOfIntegers.Clear () ; // []
            Console.WriteLine ( listOfIntegers.Count ) ; // 0
            Console.ReadLine();
        }

    }

}
