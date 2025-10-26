using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Lab5_Collections
{
    internal class Program
    {
        private static readonly string LogFile = "log.txt";

        static void Main()
        {
           

    
            Log(" MyMatrix");
            var matrix = new MyMatrix(3, 4, 1, 9);
            matrix.Show();
            matrix.ChangeSize(4, 5);
            matrix.ShowPartialy(1, 3, 1, 3);

            // === MyList<T> ===
            Log("Creating MyList<int>...");
            var myList = new MyList<int> { 5, 10, 15, 20 };
            Log($"MyList Count: {myList.Count}");
            foreach (var item in myList)
                Log($"MyList item: {item}");

            // === MyDictionary<TKey,TValue> ===
            Log("Creating MyDictionary<string,int>...");
            var dict = new MyDictionary<string, int>();
            dict.Add("A", 10);
            dict.Add("B", 20);
            dict.Add("C", 30);

            foreach (var kv in dict)
                Log($"Key={kv.Key}, Value={kv.Value}");

            Log($"Dictionary coun {dict.Count}");
            Log(" Lab5 ");

        
        }

        static void Log(string message)
        {
            var line = $"[{DateTime.Now:HH:mm:ss}] {message}";
            Console.WriteLine(line);
            File.AppendAllText(LogFile, line + Environment.NewLine);
        }
    }

    public class MyMatrix
    {
        private int[,] _matrix;
        private readonly Random _rnd = new();
        private readonly int _min, _max;

        public int Rows => _matrix.GetLength(0);
        public int Cols => _matrix.GetLength(1);

        public MyMatrix(int rows, int cols, int minValue, int maxValue)
        {
            _min = minValue;
            _max = maxValue;
            _matrix = new int[rows, cols];
            Fill();
        }

        public void Fill()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    _matrix[i, j] = _rnd.Next(_min, _max + 1);
        }

        public void ChangeSize(int newRows, int newCols)
        {
            int[,] newMatrix = new int[newRows, newCols];
            for (int i = 0; i < Math.Min(Rows, newRows); i++)
                for (int j = 0; j < Math.Min(Cols, newCols); j++)
                    newMatrix[i, j] = _matrix[i, j];

            _matrix = newMatrix;

            for (int i = 0; i < newRows; i++)
                for (int j = 0; j < newCols; j++)
                    if (_matrix[i, j] == 0)
                        _matrix[i, j] = _rnd.Next(_min, _max + 1);
        }

        public void Show()
        {
            Console.WriteLine("matrix:");
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                    Console.Write($"{_matrix[i, j],3} ");
                Console.WriteLine();
            }
        }

        public void ShowPartialy(int rowStart, int rowEnd, int colStart, int colEnd)
        {
            Console.WriteLine($"matrix fragment [{rowStart}-{rowEnd}, {colStart}-{colEnd}]:");
            for (int i = rowStart; i <= rowEnd && i < Rows; i++)
            {
                for (int j = colStart; j <= colEnd && j < Cols; j++)
                    Console.Write($"{_matrix[i, j],3} ");
                Console.WriteLine();
            }
        }

        public int this[int i, int j]
        {
            get => _matrix[i, j];
            set => _matrix[i, j] = value;
        }
    }

    public class MyList<T> : IEnumerable<T>
    {
        private T[] _items;
        private int _count;

        public int Count => _count;

        public MyList() => _items = new T[4];

        public void Add(T item)
        {
            if (_count == _items.Length)
                Array.Resize(ref _items, _items.Length * 2);
            _items[_count++] = item;
        }

        public T this[int index]
        {
            get => (index >= 0 && index < _count)
                ? _items[index]
                : throw new IndexOutOfRangeException();
            set
            {
                if (index >= 0 && index < _count)
                    _items[index] = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
                yield return _items[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private TKey[] _keys;
        private TValue[] _values;
        private int _count;

        public int Count => _count;

        public MyDictionary()
        {
            _keys = new TKey[4];
            _values = new TValue[4];
        }

        public void Add(TKey key, TValue value)
        {
            if (_count == _keys.Length)
            {
                Array.Resize(ref _keys, _keys.Length * 2);
                Array.Resize(ref _values, _values.Length * 2);
            }

            _keys[_count] = key;
            _values[_count] = value;
            _count++;
        }

        public TValue this[TKey key]
        {
            get
            {
                for (int i = 0; i < _count; i++)
                    if (EqualityComparer<TKey>.Default.Equals(_keys[i], key))
                        return _values[i];
                throw new KeyNotFoundException();
            }
            set
            {
                for (int i = 0; i < _count; i++)
                    if (EqualityComparer<TKey>.Default.Equals(_keys[i], key))
                    {
                        _values[i] = value;
                        return;
                    }
                Add(key, value);
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
                yield return new KeyValuePair<TKey, TValue>(_keys[i], _values[i]);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
