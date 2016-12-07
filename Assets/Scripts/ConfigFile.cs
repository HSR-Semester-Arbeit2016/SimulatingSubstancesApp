using System;

namespace Assets.Scripts
{
    [Serializable]
    public class ConfigFile
    {
        public ConfigFile(string fileName, string filePath)
        {
            FileName = fileName;
            FilePath = filePath;
        }

        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}