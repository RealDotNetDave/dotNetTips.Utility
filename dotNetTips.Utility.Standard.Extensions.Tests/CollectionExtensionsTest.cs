// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Extensions.Tests
// Author           : David McCarter
// Created          : 09-28-2018
//
// Last Modified By : David McCarter
// Last Modified On : 03-03-2019
// ***********************************************************************
// <copyright file="CollectionExtensionsTest.cs" company="dotNetTips.Utility.Standard.Extensions.Tests">
//     Copyright (c) dotNetTips.com - McCarter Consulting. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Diagnostics;
using dotNetTips.Utility.Standard.Tester.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dotNetTips.Utility.Standard.Extensions.Tests
{
    /// <summary>
    /// Class CollectionExtensionsTest.
    /// </summary>
    [TestClass]
    public class CollectionExtensionsTest
    {
        /// <summary>
        /// Tests the fast any.
        /// </summary>
        [TestMethod]
        public void TestFastAny()
        {
            var people = CreatePersonCollection(1000);

            //Test Finding Age of 100
            Assert.IsTrue(people.FastAny(p => p.Age >= 1));

            //Test Finding Age not in collection
            Assert.IsFalse(people.FastAny(p => p.Age > 100));
        }

        /// <summary>
        /// Tests the fast count.
        /// </summary>
        [TestMethod]
        public void TestFastCount()
        {
            var people = CreatePersonCollection(1000);

            //Test Finding Age of 100
            Assert.IsTrue(people.FastCount(p => p.Age >= 1) > 0);

            //Test Finding Age not in collection
            Assert.IsTrue(people.FastCount(p => p.Age > 100) == 0);
        }

        /// <summary>
        /// Creates the person collection.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>List&lt;Person&gt;.</returns>
        private static List<Person> CreatePersonCollection(int number)
        {
            var people = new List<Person>(number);

            for (int i = 0; i < number; i++)
            {
                var person = new Person
                {
                    Age = RandomData.Integer(1, 95),
                    FirstName = RandomData.Word(5, 50),
                    LastName = RandomData.Word(2, 50)
                };

                people.Add(person);

                Debug.WriteLine(person.Age);
            }

            return people;
        }
    }
}
