// <copyright file="ObjectExtensionsTest.cs">Copyright ©  2014</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotNetTips.Utility.Portable.Extensions;

namespace dotNetTips.Utility.Portable.Extensions
{
    [TestClass]
    [PexClass(typeof(ObjectExtensions))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ObjectExtensionsTest
    {
        [PexMethod]
        public void TryDispose(IDisposable obj, bool throwException)
        {
            ObjectExtensions.TryDispose(obj, throwException);
            // TODO: add assertions to method ObjectExtensionsTest.TryDispose(IDisposable, Boolean)
        }
    }
}
