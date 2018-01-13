// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 06-26-2017
//
// Last Modified By : David McCarter
// Last Modified On : 09-16-2017
// ***********************************************************************
// <copyright file="Config.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.IO;
using System.Xml.Serialization;
using dotNetTips.Utility.Standard.Extensions;
using dotNetTips.Utility.Standard.Xml;

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
        /// The instance
        /// </summary>
        private T _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="Config{T}" /> class.
        /// </summary>
        protected Config()
        {
            var localAppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);

            var fileName = $"{App.AssemblyInfo.Product}.config";

            var folder = Path.Combine(localAppData, App.AssemblyInfo.Company);

            this.ConfigFileName = Path.Combine(folder, fileName);
        }

        /// <summary>
        /// Gets or sets the name of the configuration file.
        /// </summary>
        /// <value>The name of the configuration file.</value>
        [XmlIgnore]
        public string ConfigFileName { get; protected set; }

        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <returns>T.</returns>
        private T GetInstance()
        {
            if (this._instance is null)
            {
                this._instance = TypeHelper.Create<T>();
            }

            return this._instance;
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool Load()
        {
            if (File.Exists(this.ConfigFileName))
            {
                this._instance = XmlHelper.DeserializeFromXmlFile<T>(this.ConfigFileName);

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

            XmlHelper.SerializeToXmlFile(this.GetInstance(), this.ConfigFileName);

            return true;
        }

        /// <summary>
        /// Returns instance for the object.
        /// </summary>
        /// <returns>T.</returns>
        public T Instance()
        {
            return this.GetInstance();
        }
    }
}
