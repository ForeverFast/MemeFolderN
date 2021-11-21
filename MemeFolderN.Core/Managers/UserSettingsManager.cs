using System;
using System.Configuration;
using System.IO;

namespace MemeFolderN.Common.Managers
{
    public class UserSettingsManager
    {
        public string RootFolderPath
        {
            get => ConfigurationManager.AppSettings["RootFolderPath"];
            set => ConfigurationManager.AppSettings["RootFolderPath"] = value;
        }

        public UserSettingsManager()
        {
            string path = "D:\\";
            if (Directory.Exists(path))
            {
                path += "\\MemeFolder";
                //if (Directory.Exists(path))
                //    Directory.Delete(path);
            }
            else
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\MemeFolder";
            }

            Directory.CreateDirectory(path);
            RootFolderPath = path;
        }
    }
}
