using System;
using System.Collections.Generic;
using System.Text;
using dotNetTips.Utility.Standard.Extensions;
using dotNetTips.Utility.Standard.OOP;
using System.IO;
using dotNetTips.Utility.Standard.IO;

namespace dotNetTips.Utility.Standard
{
    public abstract class Config
    {

        protected Config(string configFileName)
        {
            this.ConfigFileName = configFileName;
        }

        
        public void Save()
        {


            if (File.Exists(ConfigFileName))
            {
                File.Delete(ConfigFileName);

                this.ToJsonFile(ConfigFileName);
            }
        }

        public void Load()
        {
            if (File.Exists(ConfigFileName) == false)
            {
                ObjectExtensions.FromJsonFile<Config>(ConfigFileName, this);
            }
        }

        public string ConfigFileName { get; private set; }

        private bool ConfigFileNameCheck()
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(string.IsNullOrEmpty(ConfigFileName) == false);
        }
    }
}
