// <copyright file="ArgumentIsReadOnlyExceptionTest.cs" company="dotNetTips.com">Copyright Â©  2015</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotNetTips.Utility.Portable;

namespace dotNetTips.Utility.Portable.Tests
{
    [TestClass]
    [PexClass(typeof(ArgumentIsReadOnlyException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ArgumentIsReadOnlyExceptionTest
    {

        [PexMethod]
        public ArgumentIsReadOnlyException Constructor01(string message, Exception innerException)
        {
            ArgumentIsReadOnlyException target = new ArgumentIsReadOnlyException(message, innerException);
            return target;
            // TODO: add assertions to method ArgumentIsReadOnlyExceptionTest.Constructor01(String, Exception)
        }
    }
}
