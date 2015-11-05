using System.Collections.Generic;
using System.Collections;
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
        [PexMethod]
        public int Count(IEnumerable source)
        {
            int result = CollectionExtensions.Count(source);
            return result;
            // TODO: add assertions to method CollectionExtensionsTest.Count(IEnumerable)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public string ToDelimitedString<T>(IEnumerable<T> source, string delimiter)
        {
            string result = CollectionExtensions.ToDelimitedString<T>(source, delimiter);
            return result;
            // TODO: add assertions to method CollectionExtensionsTest.ToDelimitedString(IEnumerable`1<!!0>, String)
        }
        [PexMethod]
        public bool IsValid(IEnumerable source)
        {
            bool result = CollectionExtensions.IsValid(source);
            return result;
            // TODO: add assertions to method CollectionExtensionsTest.IsValid(IEnumerable)
        }
        [PexMethod]
        public bool ContainsAny(string theString, string[] characters)
        {
            bool result = CollectionExtensions.ContainsAny(theString, characters);
            return result;
            // TODO: add assertions to method CollectionExtensionsTest.ContainsAny(String, String[])
        }
    }
}
