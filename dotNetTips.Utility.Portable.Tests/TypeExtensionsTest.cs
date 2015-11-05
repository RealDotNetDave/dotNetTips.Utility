// <copyright file="TypeExtensionsTest.cs">Copyright ©  2014</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotNetTips.Utility.Portable.Extensions;

namespace dotNetTips.Utility.Portable.Extensions
{
    [TestClass]
    [PexClass(typeof(TypeExtensions))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class TypeExtensionsTest
    {
		[PexGenericArguments(typeof(IComparable))]
		[PexMethod]
		public T Max<T>(T obj1, T obj2)
			where T : IComparable
		{
			T result = TypeExtensions.Max<T>(obj1, obj2);
			return result;
			// TODO: add assertions to method TypeExtensionsTest.Max(!!0, !!0)
		}
	}
}
