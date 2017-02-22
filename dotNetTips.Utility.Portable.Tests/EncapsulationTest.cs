using System.Collections;
// <copyright file="EncapsulationTest.cs" company="dotNetTips.com">Copyright Â©  2015</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotNetTips.Utility.Portable.OOP;

namespace dotNetTips.Utility.Portable.OOP.Tests
{
    [TestClass]
    [PexClass(typeof(Encapsulation))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class EncapsulationTest
    {

        [PexGenericArguments(typeof(ArgumentException))]
        [PexMethod]
        public void TryValidateParam<TException>(bool condition, string message)
            where TException : ArgumentException, new() {
            Encapsulation.TryValidateParam<TException>(condition, message);
            // TODO: add assertions to method EncapsulationTest.TryValidateParam(Boolean, String)
        }

        [PexMethod]
        public void TryValidateParam(
            IEnumerable collection,
            string paramName,
            string message
        ) {
            Encapsulation.TryValidateParam(collection, paramName, message);
            // TODO: add assertions to method EncapsulationTest.TryValidateParam(IEnumerable, String, String)
        }

        [PexMethod]
        public void TryValidateParam(
            Enum value,
            string paramName,
            string message
        ) {
            Encapsulation.TryValidateParam(value, paramName, message);
            // TODO: add assertions to method EncapsulationTest.TryValidateParam(Enum, String, String)
        }
    }
}
