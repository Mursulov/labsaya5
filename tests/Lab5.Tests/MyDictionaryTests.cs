using NUnit.Framework;
using Lab5App;
using System.Collections.Generic;

namespace Lab5.Tests
{
    public class MyDictionaryTests
    {
        [Test]
        public void AddAndIndexer()
        {
            var d = new MyDictionary<string,int>();
            d.Add("a", 1);
            d.Add("b", 2);
            Assert.AreEqual(2, d.Count);
            Assert.AreEqual(1, d["a"]);
            Assert.IsTrue(d.ContainsKey("b"));
        }

        [Test]
        public void ForeachEnumerates()
        {
            var d = new MyDictionary<string,int>();
            d.Add("x", 100);
            var list = new List<string>();
            foreach (var kv in d)
                list.Add(kv.Key);
            Assert.Contains("x", list);
        }
    }
}
