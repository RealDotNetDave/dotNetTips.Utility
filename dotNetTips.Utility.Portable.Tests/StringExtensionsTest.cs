// <copyright file="StringExtensionsTest.cs">Copyright ©  2014</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotNetTips.Utility.Portable.Extensions;

namespace dotNetTips.Utility.Portable.Extensions
{
    [TestClass]
    [PexClass(typeof(StringExtensions))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class StringExtensionsTest
    {
		[PexMethod]
		public string Repeat(string source, int count)
		{
			string result = StringExtensions.Repeat(source, count);
			return result;
			// TODO: add assertions to method StringExtensionsTest.Repeat(String, Int32)
		}
		[PexMethod]
		public string Prepend01(
			string s,
			int count,
			string value
		)
		{
			string result = StringExtensions.Prepend(s, count, value);
			return result;
			// TODO: add assertions to method StringExtensionsTest.Prepend01(String, Int32, String)
		}
	}
}
