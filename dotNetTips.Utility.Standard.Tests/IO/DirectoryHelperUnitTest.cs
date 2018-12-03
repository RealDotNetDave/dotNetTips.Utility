// ***********************************************************************
// Assembly         : dotNetTips.Tips.Utility.Standard.Tests
// Author           : David McCarter
// Created          : 06-15-2018
//
// Last Modified By : David McCarter
// Last Modified On : 07-05-2018
// ***********************************************************************
// <copyright file="DirectoryHelperUnitTest.cs" company="McCarter Consulting">
//     David McCarter - dotNetTips.com
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dotNetTips.Utility.Standard.IO;
using dotNetTips.Utility.Standard.Win32;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dotNetTips.Tips.Utility.Standard.Tests.IO
{
    /// <summary>
    /// Class DirectoryHelperUnitTest.
    /// </summary>
    [TestClass]
    public class DirectoryHelperUnitTest
    {
        /// <summary>
        /// Loads the one drive folders test.
        /// </summary>
        [TestMethod]
        public void LoadOneDriveFoldersTest()
        {
            var folders = DirectoryHelper.LoadOneDriveFolders();

            Assert.IsTrue(folders!= null && folders.Count() > 0);
        }

        /// <summary>
        /// Loads the key sub names.
        /// </summary>
        [TestMethod]
        public void LoadKeySubNames()
        {
            var names = RegistryHelper.GetCurrentUserRegistryKey(RegistryHelper.KeyCurrentUserOneDrive).GetSubKeyNames();


            Assert.IsTrue(names?.Count() > 0);
        }
    }
}
