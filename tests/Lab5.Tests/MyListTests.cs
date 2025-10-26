using NUnit.Framework;
using Lab5App;

namespace Lab5.Tests
{
    public class MyListTests
    {
        [Test]
        public void AddAndIndex()
        {
            var list = new MyList<int>();
            list.Add(5);
            list.Add(10);
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(5, list[0]);
            Assert.AreEqual(10, list[1]);
        }

        [Test]
        public void CollectionInitializer()
        {
            var list = new MyList<int> { 1, 2, 3 };
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(2, list[1]);
        }
    }
}
