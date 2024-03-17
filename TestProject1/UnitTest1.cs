using C__aaaaa;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            Class1 cls = new Class1(1);
            Assert.AreEqual(1, cls.getA());
            cls.setA(2);
            Assert.AreEqual(2, cls.getA());
        }
    }
}