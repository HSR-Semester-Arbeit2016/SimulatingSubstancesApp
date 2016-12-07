using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts
{
    public class FileManager
    {
        public static void Save(Configuration configuration)
        {
            var bf = new BinaryFormatter();
            var file = File.Create(Application.persistentDataPath + "/" + configuration.Name);
            bf.Serialize(file, configuration);
            file.Close();
        }

        public static Configuration Load(string fileName)
        {
            if (File.Exists(Application.persistentDataPath + "/" + fileName))
            {
                var bf = new BinaryFormatter();
                var file = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);
                var configuration = (Configuration) bf.Deserialize(file);
                file.Close();
                return configuration;
            }
            return new Configuration();
        }

        public static void DeleteFile(string fileName)
        {
            if (File.Exists(Application.persistentDataPath + "/" + fileName))
            {
                File.Delete(Application.persistentDataPath + "/" + fileName);
            }
        }
    }
}