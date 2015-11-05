// <copyright file="CollectionExtensionsTest.ContainsAny.g.cs">Copyright ©  2014</copyright>
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
//
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;
using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dotNetTips.Utility.Portable.Extensions
{
    public partial class CollectionExtensionsTest
    {
        [TestMethod]
        [PexGeneratedBy(typeof(CollectionExtensionsTest))]
        [PexRaisedException(typeof(NullReferenceException))]
        public void ContainsAnyThrowsNullReferenceException697()
        {
            bool b;
            b = this.ContainsAny((string)null, (string[])null);
        }

        [TestMethod]
        [PexGeneratedBy(typeof(CollectionExtensionsTest))]
        public void ContainsAny721()
        {
            bool b;
            string[] ss = new string[0];
            b = this.ContainsAny((string)null, ss);
            Assert.AreEqual<bool>(false, b);
        }

        [TestMethod]
        [PexGeneratedBy(typeof(CollectionExtensionsTest))]
        [PexRaisedException(typeof(NullReferenceException))]
        public void ContainsAnyThrowsNullReferenceException361()
        {
            bool b;
            string[] ss = new string[1];
            b = this.ContainsAny((string)null, ss);
        }

        [TestMethod]
        [PexGeneratedBy(typeof(CollectionExtensionsTest))]
        [PexRaisedException(typeof(NullReferenceException))]
        public void ContainsAnyThrowsNullReferenceException258()
        {
            bool b;
            string[] ss = new string[1];
            ss[0] = "";
            b = this.ContainsAny((string)null, ss);
        }

        [TestMethod]
        [PexGeneratedBy(typeof(CollectionExtensionsTest))]
        public void ContainsAny606()
        {
            bool b;
            string[] ss = new string[1];
            ss[0] = "";
            b = this.ContainsAny("", ss);
            Assert.AreEqual<bool>(true, b);
        }

        [TestMethod]
        [PexGeneratedBy(typeof(CollectionExtensionsTest))]
        public void ContainsAny993()
        {
            bool b;
            string[] ss = new string[1];
            ss[0] = "\0";
            b = this.ContainsAny("䀀 ", ss);
            Assert.AreEqual<bool>(false, b);
        }

        [TestMethod]
        [PexGeneratedBy(typeof(CollectionExtensionsTest))]
        public void ContainsAny495()
        {
            bool b;
            string[] ss = new string[1];
            ss[0] = "\0";
            b = this.ContainsAny("", ss);
            Assert.AreEqual<bool>(false, b);
        }

        [TestMethod]
        [PexGeneratedBy(typeof(CollectionExtensionsTest))]
        public void ContainsAny946()
        {
            bool b;
            string[] ss = new string[2];
            ss[0] = "\0";
            ss[1] = "";
            b = this.ContainsAny("", ss);
            Assert.AreEqual<bool>(true, b);
        }

        [TestMethod]
        [PexGeneratedBy(typeof(CollectionExtensionsTest))]
        public void ContainsAny572()
        {
            bool b;
            string[] ss = new string[2];
            ss[0] = "\0\0\0\0\0\0\0";
            ss[1] = "\0";
            b = this.ContainsAny(" က", ss);
            Assert.AreEqual<bool>(false, b);
        }

        [TestMethod]
        [PexGeneratedBy(typeof(CollectionExtensionsTest))]
        public void ContainsAny542()
        {
            bool b;
            string[] ss = new string[4];
            ss[0] = "\0";
            ss[1] = "\0";
            ss[2] = "\0";
            ss[3] = "\0";
            b = this.ContainsAny("\0\0\0\0\0", ss);
            Assert.AreEqual<bool>(false, b);
        }

        [TestMethod]
        [PexGeneratedBy(typeof(CollectionExtensionsTest))]
        public void ContainsAny966()
        {
            bool b;
            string[] ss = new string[3];
            ss[0] = "\0\0\0\0\0\0\0\0\0";
            ss[1] = "";
            ss[2] = "\0\0\0\0\0\0\0\0\0";
            b = this.ContainsAny(new string('\0', 9), ss);
            Assert.AreEqual<bool>(false, b);
        }
    }
}