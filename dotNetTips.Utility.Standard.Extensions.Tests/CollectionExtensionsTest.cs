using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dotNetTips.Utility.Standard.Extensions.Tests
{
    [TestClass]
    public class CollectionExtensionsTest
    {
        [TestMethod]
        public void TestFastAny()
        {
            var people = CreatePersonCollection(1000);

            //Test Finding Age of 100
            Assert.IsTrue(people.FastAny(p => p.Age == 100));

            //Test Finding Age not in collection
            Assert.IsFalse(people.FastAny(p => p.Age == 100000));
        }

        [TestMethod]
        public void TestFastCount()
        {
            var people = CreatePersonCollection(1000);

            //Test Finding Age of 100
            Assert.IsTrue(people.FastCount(p => p.Age == 100) > 0);

            //Test Finding Age not in collection
            Assert.IsTrue(people.FastCount(p => p.Age == 100000) == 0);
        }


        private static List<Person> CreatePersonCollection(int number)
        {
            var people = new List<Person>(number);

            for (int i = 0; i < number; i++)
            {
                var person = new Person
                {
                    Age = i,
                    FirstName = $"FirstName{i}",
                    LastName = $"LastName{i}"
                };

                people.Add(person);
            }

            return people;
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
