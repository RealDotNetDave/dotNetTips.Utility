// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 03-06-2017
//
// Last Modified By : David McCarter
// Last Modified On : 03-06-2017
// ***********************************************************************
// <copyright file="Config.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using dotNetTips.Utility.Standard.Extensions;
using dotNetTips.Utility.Standard.IO;
using System.IO;

namespace dotNetTips.Utility.Standard
{
    /// <summary>
    /// Class Config.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="dotNetTips.Utility.Standard.ISingleton{T}" />
    public class Config<T> : ISingleton<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Config{T}"/> class.
        /// </summary>
        protected Config()
        {
            var fileName = $"{App.Info().AssemblyProduct}.config";
            var folder = Path.Combine(DirectoryHelper.GetFolderPath(SpecialFolder.LocalApplicationData), App.Info().Company);

            this.ConfigFileName = Path.Combine(folder, fileName);
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool Save()
        {
            if (File.Exists(ConfigFileName))
            {
                File.Delete(ConfigFileName);
            }

            _instance.ToJsonFile(ConfigFileName);

            return true;
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool Load()
        {
            if (File.Exists(ConfigFileName))
            {
                _instance = ObjectExtensions.FromJsonFile<T>(ConfigFileName);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets or sets the name of the configuration file.
        /// </summary>
        /// <value>The name of the configuration file.</value>
        public string ConfigFileName { get; protected set; }

        /// <summary>
        /// The instance
        /// </summary>
        private T _instance;

        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <returns>T.</returns>
        public T Instance()
        {
            if (_instance == null)
            {
                _instance = TypeHelper.Create<T>();
            }

            return _instance;
        }
    }
}
