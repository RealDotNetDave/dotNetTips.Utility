// <copyright file="TypeHelperTest.cs" company="dotNetTips.com">Copyright Â©  2015</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotNetTips.Utility.Portable;

namespace dotNetTips.Utility.Portable.Tests
{
    [TestClass]
    [PexClass(typeof(TypeHelper))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class TypeHelperTest
    {

        [PexGenericArguments(typeof(object))]
        [PexMethod]
        public T Create<T>()
            where T : class
        {
            T result = TypeHelper.Create<T>();
            return result;
            // TODO: add assertions to method TypeHelperTest.Create()
        }
    }
}
