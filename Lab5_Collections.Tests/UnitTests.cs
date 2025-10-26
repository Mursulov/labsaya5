using Xunit;
using Lab5_Collections;
using System.Linq;

namespace Lab5_Collections.Tests
{
    public class UnitTests
    {
        [Fact]
        public void MyMatrix_Indexer_Works()
        {
            var m = new MyMatrix(2, 2, 1, 5);
            m[0, 0] = 42;
            Assert.Equal(42, m[0, 0]);
        }

        [Fact]
        public void MyList_AddAndCount_Works()
        {
            var list = new MyList<int>();
            list.Add(1);
            list.Add(2);
            Assert.Equal(2, list.Count);
            Assert.Equal(1, list[0]);
        }

        [Fact]
        public void MyDictionary_AddAndGet_Works()
        {
            var dict = new MyDictionary<string, int>();
            dict.Add("A", 100);
            dict.Add("B", 200);
            Assert.Equal(100, dict["A"]);
            Assert.Equal(2, dict.Count);
        }

        [Fact]
        public void MyDictionary_Enumerator_Works()
        {
            var dict = new MyDictionary<int, string>();
            dict.Add(1, "one");
            dict.Add(2, "two");
            var keys = dict.Select(kv => kv.Key).ToList();
            Assert.Contains(1, keys);
            Assert.Contains(2, keys);
        }
    }
}
