using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dotNetTips.Utility.Standard.IO;
using dotNetTips.Utility.Standard.Win32;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dotNetTips.Tips.Utility.Standard.Tests.IO
{
    [TestClass]
    public class DirectoryHelperUnitTest
    {
        [TestMethod]
        public void LoadOneDriveFoldersTest()
        {
            var folders = DirectoryHelper.LoadOneDriveFolders();

            Assert.IsTrue(folders?.Count() > 0);
        }

        [TestMethod]
        public void LoadKeySubNames()
        {
            var names = RegistryHelper.GetCurrentUserRegistryKey(RegistryHelper.KeyCurrentUserOneDrive).GetSubKeyNames();


            Assert.IsTrue(names?.Count() > 0);
        }
    }
}
