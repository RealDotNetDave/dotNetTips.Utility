// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 06-26-2017
//
// Last Modified By : David McCarter
// Last Modified On : 06-10-2018
// ***********************************************************************
// <copyright file="Config.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.IO;
using System.Xml.Serialization;
using dotNetTips.Utility.Standard.Xml;

namespace dotNetTips.Utility.Standard
{
    /// <summary>
    /// Class Config.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Config<T> where T : class, new()
    {

        /// <summary>
        /// Prevents a default instance of the <see cref="Config{T}" /> class from being created.
        /// </summary>
        private Config() : this(ConfigStorageLocation.User)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Config{T}" /> class.
        /// </summary>
        /// <param name="storageLocation">The storage location.</param>
        protected Config(ConfigStorageLocation storageLocation)
        {
            var localAppData = string.Empty;

            localAppData = storageLocation == ConfigStorageLocation.User ? System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) : Directory.GetCurrentDirectory();

            var fileName = $"{App.AssemblyInfo.Product}.config";

            var folder = Path.Combine(localAppData, App.AssemblyInfo.Company);

            this.ConfigFileName = Path.Combine(folder, fileName);
        }

        /// <summary>
        /// Gets or sets the name of the configuration file.
        /// </summary>
        /// <value>The name of the configuration file.</value>
        [XmlIgnore]
        public string ConfigFileName { get; private set; }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool Load()
        {
            if (File.Exists(this.ConfigFileName))
            {
                Instance = XmlHelper.DeserializeFromXmlFile<T>(this.ConfigFileName);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool Save()
        {
            if (File.Exists(this.ConfigFileName))
            {
                File.Delete(this.ConfigFileName);
            }

            XmlHelper.SerializeToXmlFile(Instance, this.ConfigFileName);

            return true;
        }

        /// <summary>
        /// Returns instance for the object.
        /// </summary>
        /// <value>The instance.</value>
        [XmlIgnore]
        public static T Instance { get; private set; } = TypeHelper.Create<T>();
    }
}
