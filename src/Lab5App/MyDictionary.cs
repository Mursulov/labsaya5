using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab5App
{
    public class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private TKey[] keys;
        private TValue[] values;
        private int count;

        public MyDictionary(int capacity = 8)
        {
            if (capacity < 1) capacity = 8;
            keys = new TKey[capacity];
            values = new TValue[capacity];
            count = 0;
        }

        public int Count => count;

        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key)) throw new ArgumentException("Key already exists");
            if (count >= keys.Length) Resize(keys.Length * 2);
            keys[count] = key;
            values[count] = value;
            count++;
            Logger.Log($"MyDictionary: added key='{key}'. Count={count}");
        }

        private void Resize(int newSize)
        {
            var newKeys = new TKey[newSize];
            var newValues = new TValue[newSize];
            Array.Copy(keys, newKeys, count);
            Array.Copy(values, newValues, count);
            keys = newKeys;
            values = newValues;
        }

        public TValue this[TKey key]
        {
            get
            {
                for (int i = 0; i < count; i++)
                    if (Equals(keys[i], key))
                        return values[i];
                throw new KeyNotFoundException();
            }
            set
            {
                for (int i = 0; i < count; i++)
                    if (Equals(keys[i], key))
                    {
                        values[i] = value;
                        return;
                    }
                Add(key, value);
            }
        }

        public bool ContainsKey(TKey key)
        {
            for (int i = 0; i < count; i++)
                if (Equals(keys[i], key))
                    return true;
            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
                yield return new KeyValuePair<TKey, TValue>(keys[i], values[i]);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
