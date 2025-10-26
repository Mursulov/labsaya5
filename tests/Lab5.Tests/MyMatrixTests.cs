using NUnit.Framework;
using Lab5App;

namespace Lab5.Tests
{
    public class MyMatrixTests
    {
        [Test]
        public void CreateAndIndexing()
        {
            var m = new MyMatrix(2, 3, 0, 5);
            Assert.AreEqual(2, m.Rows);
            Assert.AreEqual(3, m.Cols);
            int v = m[0,0];
            m[0,0] = v; // проверка set
        }

        [Test]
        public void ChangeSizeCopiesValues()
        {
            var m = new MyMatrix(2, 2, 1, 1);
            int a00 = m[0,0], a01 = m[0,1];
            m.ChangeSize(3, 4);
            Assert.AreEqual(3, m.Rows);
            Assert.AreEqual(4, m.Cols);
            Assert.AreEqual(a00, m[0,0]);
            Assert.AreEqual(a01, m[0,1]);
        }
    }
}
