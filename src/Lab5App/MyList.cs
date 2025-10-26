using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab5App
{
    public class MyList<T> : IEnumerable<T>
    {
        private T[] items;
        private int count;

        public MyList(int capacity = 4)
        {
            if (capacity < 1) capacity = 4;
            items = new T[capacity];
            count = 0;
        }

        public int Count => count;

        public void Add(T item)
        {
            if (count >= items.Length)
                Resize(items.Length * 2);
            items[count++] = item;
            Logger.Log($"MyList: added item. New count = {count}");
        }

        private void Resize(int newSize)
        {
            var newArr = new T[newSize];
            Array.Copy(items, newArr, count);
            items = newArr;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count) throw new IndexOutOfRangeException();
                return items[index];
            }
            set
            {
                if (index < 0 || index >= count) throw new IndexOutOfRangeException();
                items[index] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
                yield return items[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
