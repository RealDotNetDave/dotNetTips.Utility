// <copyright file="IdentityReferenceEnumeratorFactory.cs">Copyright ©  2014</copyright>

using System;
using Microsoft.Pex.Framework;

namespace System.Security.Principal
{
    /// <summary>A factory for System.Security.Principal.IdentityReferenceEnumerator instances</summary>
    public static partial class IdentityReferenceEnumeratorFactory
    {
        /// <summary>A factory for System.Security.Principal.IdentityReferenceEnumerator instances</summary>
        [PexFactoryMethod(typeof(GC), "System.Security.Principal.IdentityReferenceEnumerator")]
        public static IDisposable Create()
        {
            throw new NotImplementedException();

            // TODO: Edit factory method of IdentityReferenceEnumerator
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
