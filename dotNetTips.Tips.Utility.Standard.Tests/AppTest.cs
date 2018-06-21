using System;
using System.Collections.Generic;
using System.Text;
using dotNetTips.Utility.Standard;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotNetTips.Utility.Standard.Extensions;

namespace dotNetTips.Tips.Utility.Standard.Tests
{
    [TestClass]
    public class AppTest
    {
        [TestMethod]
        public void TestLoadingAppInfo()
        {
            var info = App.AssemblyInfo;

            Assert.IsTrue(info.Company.IsValid());
            Assert.IsTrue(info.Copyright.IsValid());
            Assert.IsTrue(info.Culture.IsValid());
            Assert.IsTrue(info.Description.IsValid());

        }
    }
}
