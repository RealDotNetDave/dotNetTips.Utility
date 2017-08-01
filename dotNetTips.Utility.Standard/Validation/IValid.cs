using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetTips.Utility.Standard
{
    public interface IValid
    {
        /// <summary>
        /// Returns true if type is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        bool IsValid { get; }
    }
}
