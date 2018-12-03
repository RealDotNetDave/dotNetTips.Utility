// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Extensions.Tests
// Author           : David McCarter
// Created          : 09-28-2018
//
// Last Modified By : David McCarter
// Last Modified On : 09-28-2018
// ***********************************************************************
// <copyright file="CollectionExtensionsTest.cs" company="dotNetTips.Utility.Standard.Extensions.Tests">
//     Copyright (c) dotNetTips.com - McCarter Consulting. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
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
            Assert.IsTrue(people.FastAny(p => p.Age == 100));

            //Test Finding Age not in collection
            Assert.IsFalse(people.FastAny(p => p.Age == 100000));
        }

        /// <summary>
        /// Tests the fast count.
        /// </summary>
        [TestMethod]
        public void TestFastCount()
        {
            var people = CreatePersonCollection(1000);

            //Test Finding Age of 100
            Assert.IsTrue(people.FastCount(p => p.Age == 100) > 0);

            //Test Finding Age not in collection
            Assert.IsTrue(people.FastCount(p => p.Age == 100000) == 0);
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
                    Age = i,
                    FirstName = $"FirstName{i}",
                    LastName = $"LastName{i}"
                };

                people.Add(person);
            }

            return people;
        }
    }
}
