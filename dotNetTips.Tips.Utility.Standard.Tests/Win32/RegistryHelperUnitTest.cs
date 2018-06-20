using System;
using System.Collections.Generic;
using System.Text;
using dotNetTips.Utility.Standard.Win32;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dotNetTips.Tips.Utility.Standard.Tests.Win32
{
    [TestClass]
    public class RegistryHelperUnitTest
    {
        [TestMethod]
        public void LookupUserOneDriveKey()
        {
            var key = RegistryHelper.GetCurrentUserRegistryKey(RegistryHelper.KeyCurrentUserOneDrive);

            Assert.IsNotNull(key);
        }
    }
}
