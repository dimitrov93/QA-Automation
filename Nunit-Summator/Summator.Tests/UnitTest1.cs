using NUnit.Framework;
using System;
using System.Linq;

namespace Summator.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        private Summator summer;

        [OneTimeSetUp]
        public void SetUp()
        {
            summer = new Summator();
            System.Console.WriteLine("Start" + DateTime.Now);
        }

        [Test]
        public void Test_Sum_Two_Possitive_Numbers()
        {
            int result = summer.Sum(new int[] { 2, 4 });
            Assert.That(result == 6);
        }

        [Test]
        public void Test_Sum_Two_Negative_Numbers()
        {
            int result = summer.Sum(new int[] { -2, -4 });
            Assert.That(result, Is.EqualTo(-6) );
            Assert.AreEqual(-6, result);
            Assert.That(-6,Is.InRange(-7, -5));
            Assert.That("Yes", Does.Contain("Yes"));
            Assert.That(Enumerable.Range(1, 100), Has.Member(5));
        }
        [Test]
        public void Test_Sum_Pos_and_Neg_Numbers()
        {
            int result = summer.Sum(new int[] { -4, 4 });
            Assert.That(result == 0);

        }

        [Test]
        public void Test_Average_Summator()
        {
            int result = summer.Avarage(new int[] { 1, 2, 3 });
            Assert.That(result == 2);
        }

        [Test]
        public void Test_Equal()
        {
            int result = summer.Sum(new int[] { 1, 2, 3 });
            Assert.AreEqual(6, result);
        }

        [Test]
        [Category("Critical")]
        public void Test_Equal2()
        {
            double result = summer.Avarage2(new double[] { 1.5, 1.5 });
            Assert.AreEqual(1.5, result);

        }

        [OneTimeTearDown]
        public void TestCleanUp()
        {
            summer = null;
            System.Console.WriteLine("Tear Down: " + DateTime.Now);

        }
    }
}