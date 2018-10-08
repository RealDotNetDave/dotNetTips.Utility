using System.Diagnostics;
using dotNetTips.Utility.Standard;
using dotNetTips.Utility.Standard.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dotNetTips.Tips.Utility.Standard.Tests
{
    [TestClass]
    public class AppTest
    {
        [TestMethod]
        public void TestLoadingAppInfo()
        {
            var info = App.AssemblyInfo;

            Assert.IsTrue(info.FileVersion.HasItems());
            Assert.IsTrue(info.Version.HasItems());

        }

        [TestMethod]
        public void TestLoadingEnvironmentVariables()
        {
            var variables = App.GetEnvironmentVariables();

            Assert.IsNotNull(variables);

            Assert.IsTrue(variables.Count > 0);

            foreach (var variable in variables)
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
