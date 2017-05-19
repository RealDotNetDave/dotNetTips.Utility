using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// <copyright file="DateTimeExtensionsTest.Intersects.g.cs" company="dotNetTips.com">David McCarter - dotNetTips.com © 2017</copyright>
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace dotNetTips.Utility.Portable.Extensions.Tests
{
    public partial class DateTimeExtensionsTest
    {

[TestMethod]
[PexGeneratedBy(typeof(DateTimeExtensionsTest))]
public void Intersects786()
{
    bool b;
    b = this.Intersects(default(DateTime), default(DateTime), 
                        default(DateTime), default(DateTime));
    Assert.AreEqual<bool>(true, b);
}

[TestMethod]
[PexGeneratedBy(typeof(DateTimeExtensionsTest))]
public void Intersects968()
{
    bool b;
    b = this.Intersects
            (new DateTime(4611686018427387903L & 1L, (DateTimeKind)(1uL >> 62)), 
             default(DateTime), default(DateTime), default(DateTime));
    Assert.AreEqual<bool>(false, b);
}
    }
}
