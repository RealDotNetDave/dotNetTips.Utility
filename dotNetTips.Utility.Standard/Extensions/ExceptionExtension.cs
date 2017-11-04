using System;

namespace dotNetTips.Utility.Standard.Extensions
{
    /// <summary>
    /// Class ExceptionExtension.
    /// </summary>
    public static class ExceptionExtension
    {
        #region Public Methods

        /// <summary>
        /// Traverses Exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex">The ex.</param>
        /// <returns>T.</returns>
        /// <exception cref="ArgumentNullException">ex - Exception cannot be null.</exception>
        public static T TraverseFor<T>(this Exception ex) where T : class
        {
            if (ex is null)
            {
                throw new ArgumentNullException(nameof(ex), "Exception cannot be null.");
            }

            if (ReferenceEquals(ex.GetType(), typeof(T)))
            {
                return ex as T;
            }

            return ex.InnerException.TraverseFor<T>();
        }

        #endregion Public Methods
    }
}