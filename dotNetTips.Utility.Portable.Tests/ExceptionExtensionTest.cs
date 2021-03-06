// <copyright file="ExceptionExtensionTest.cs" company="dotNetTips.com">David McCarter - dotNetTips.com © 2017</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotNetTips.Utility.Portable.Extensions;

namespace dotNetTips.Utility.Portable.Extensions.Tests
{
    [TestClass]
    [PexClass(typeof(ExceptionExtension))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ExceptionExtensionTest
    {

        [PexMethod]
        public string GetAllMessages(Exception exception)
        {
            string result = ExceptionExtension.GetAllMessages(exception);
            return result;
            // TODO: add assertions to method ExceptionExtensionTest.GetAllMessages(Exception)
        }
    }
}
