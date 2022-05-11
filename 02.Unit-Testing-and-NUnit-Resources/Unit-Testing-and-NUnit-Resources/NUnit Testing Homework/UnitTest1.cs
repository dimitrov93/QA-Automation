using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace NUnit_Testing_Homework
{
    public class Tests
    {

        [Test]
        [Timeout(1000)]
        public void Testing_Collection_1Millionitems()
        {
            const int itemsCount = 1000000;
            var nums = new Collections.Collection<int>();
            var newNums = Enumerable.Range(1, itemsCount).ToArray();
            
            nums.AddRange(newNums);
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);

            for (int i = itemsCount - 1; i >= 0; i--)
            {
                nums.RemoveAt(i);
            }
            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }

        [Test]
        public void Testing_Collection_Add()
        {
            var nums = new Collections.Collection<int>();
            nums.Add(1);
            nums.Add(2);
            Assert.That(nums.ToString(), Is.EqualTo("[1, 2]"));
        }

        [Test]
        public void Testing_Collection_AddRange()
        {
            var nums = new Collections.Collection<int>();
            nums.AddRange(Enumerable.Range(1, 10).ToArray());
            Assert.That(nums.Count, Is.EqualTo(10));
        }

        [Test]
        public void Testing_Collection_AddRangeWithGrow()
        {
            var nums = new Collections.Collection<int>();
            int oldCapacity = nums.Capacity;

            var newNums = Enumerable.Range(1000, 2000).ToArray();
            nums.AddRange(newNums);

            string exp = "[" + string.Join(",", newNums) + "]";

            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));

        }

        [Test]
        public void Testing_Clear()
        {
            var nums = new Collections.Collection<int>();
            nums.Add(1);
            nums.Add(2);
            Assert.That(nums.Count, Is.EqualTo(2));
            nums.Clear();
            Assert.That(nums.Count, Is.EqualTo(0));

        }

        [Test]
        public void Testing_EmptyConstructur()
        {
            var nums = new Collections.Collection<int>();
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Testing_Constructur_With_One_Item()
        {
            var nums = new Collections.Collection<int>(5);
            Assert.That(nums.ToString(), Is.EqualTo("[5]"));
        }

        [Test]
        public void Testing_Constructur_With_Multiple_Item()
        {
            var nums = new Collections.Collection<int>(5,10,15);
            Assert.That(nums.ToString(), Is.EqualTo("[5, 10, 15]"));
        }

        [Test]
        public void Testing_CountAndCapacity()
        {
            var nums = new Collections.Collection<int>(1,2);
            Assert.That(nums.Count == 2);
            Assert.That(nums.Capacity == 16);
            for (int i = 3; i < 20; i++)
            {
                nums.Add(i);
            }
            Assert.That(nums.Count == 19);
            Assert.That(nums.Capacity == 32);
        }

        [Test]
        public void Testing_ExchangeFirstLast()
        {
            var nums = new Collections.Collection<int>(2, 4, 6);
            Assert.That(nums[0] == 2);
            Assert.That(nums[2] == 6);
            nums.Exchange(0, 2);
            Assert.That(nums[0], Is.EqualTo(6));
            Assert.That(nums[2], Is.EqualTo(2));
        }

        [Test]
        public void Testing_ExchangeInvalidIndex()
        {
            var nums = new Collections.Collection<int>(2, 4, 6);
            Assert.That(nums[0] == 2);
            Assert.That(nums[2] == 6);
            Assert.That(() => nums.Exchange(0, 3), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Testing_ExchangeMiddle()
        {
            var nums = new Collections.Collection<int>(2, 4, 6);
            nums.Exchange(nums.Count / 2, 0);
            Assert.That(nums[1], Is.EqualTo(2) );

        }

        [Test]
        public void Testing_Collection_Get_ByIndex()
        {
            var names = new Collections.Collection<string>("Peter", "Maria");
            var first = names[0];
            var second = names[1];
            
            Assert.That(first, Is.EqualTo("Peter"));
            Assert.That(second, Is.EqualTo("Maria"));
        }

        [Test]
        public void Testing_Collection_Get_InvalidIndex()
        {
            var names = new Collections.Collection<string>("Peter", "Maria");

            Assert.That(() => { var name = names[-1]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[2]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[500]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(),Is.EqualTo("[Peter, Maria]"));
        }

        [Test]
        public void Testing_Collection_Nested_Collection()
        {
            var names = new Collections.Collection<string>("Peter", "Maria");
            var nums = new Collections.Collection<int>(10,20);
            var date = new Collections.Collection<DateTime>();
            var nested = new Collections.Collection<object>(names,nums,date);
            string nestedToString = nested.ToString();  

            Assert.That(nestedToString, Is.EqualTo("[[Peter, Maria], [10, 20], []]"));
        }



        [TestCase("Peter", 0 , "Peter")]
        [TestCase("Peter,Maria", 1 , "Maria")]
        [TestCase("Peter,Maria,Gosho", 2 , "Gosho")]
        public void Testing_Collection_Valid_Index(string data, int index, string expectedValue)
        {
            var items = new Collections.Collection<string>(data.Split(","));
            var item = items[index];
            Assert.That(item, Is.EqualTo(expectedValue));
        }


        [TestCase("", 0)]
        [TestCase("Peter", -1)]
        [TestCase("Peter", 1)]
        [TestCase("Peter,Maria,Gosho", -1)]
        [TestCase("Peter,Maria,Gosho", 3)]
        [TestCase("Peter,Maria,Gosho", 150)]
        public void Testing_Collection_Valid_Index(string data, int index)
        {
            var items = new Collections.Collection<string>(data.Split(",", StringSplitOptions.RemoveEmptyEntries));
            Assert.That(() => items[index], Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}