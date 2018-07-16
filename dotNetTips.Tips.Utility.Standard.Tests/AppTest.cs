using System;
using System.Collections.Generic;
using System.Text;
using dotNetTips.Utility.Standard;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotNetTips.Utility.Standard.Extensions;
using System.Diagnostics;

namespace dotNetTips.Tips.Utility.Standard.Tests
{
    [TestClass]
    public class AppTest
    {
        [TestMethod]
        public void TestLoadingAppInfo()
        {
            var info = App.AssemblyInfo;

            Assert.IsTrue(info.FileVersion.IsValid());
            Assert.IsTrue(info.Version.IsValid());

        }

        [TestMethod]
        public void TestLoadingEnvironmentVariables()
        {
            var variables = App.GetEnvironmentVariables();

            Assert.IsNotNull(variables);

            Assert.IsTrue(variables.Count > 0);

            foreach(var variable in variables)
            {
                Debug.WriteLine($"{variable.Key}:{variable.Value}");
            }
        }

        [TestMethod]
        public void TestLoadingEnvironmentVaiable()
        {
            var variables = App.GetEnvironmentVariables();

            Assert.IsTrue(variables.ContainsKey(EnvironmentKeys.Path.ToString()));
        }
    }
}
