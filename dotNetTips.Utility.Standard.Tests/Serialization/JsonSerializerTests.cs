using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using dotNetTips.Utility.Standard.Tester;
using dotNetTips.Utility.Standard.Tester.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dotNetTips.Tips.Utility.Standard.Tests.Serialization
{
    [TestClass]
    public  class JsonSerializerTests
    {
        [TestMethod]
        public void TestSerializingPersonToJson()
        {
            var person = RandomData.GeneratePerson<PersonProper>();

            var json = dotNetTips.Utility.Standard.Serialization.JsonSerializer.Serialize(person);

            Debug.WriteLine(json);

            Assert.IsTrue(string.IsNullOrEmpty(json) == false);
        }
    }
}
