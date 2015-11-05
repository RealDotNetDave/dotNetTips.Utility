using System.Collections.Generic;
// <copyright file="CollectionExtensionsTest.cs">Copyright ©  2014</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotNetTips.Utility.Portable.Extensions;

namespace dotNetTips.Utility.Portable.Extensions
{
    [TestClass]
    [PexClass(typeof(CollectionExtensions))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class CollectionExtensionsTest
    {
		[PexGenericArguments(typeof(int))]
		[PexMethod]
		public string ToDelimitedString<T>(IEnumerable<T> source, string delimiter)
		{
			string result = CollectionExtensions.ToDelimitedString<T>(source, delimiter);
			return result;
			// TODO: add assertions to method CollectionExtensionsTest.ToDelimitedString(IEnumerable`1<!!0>, String)
		}
	}
}
