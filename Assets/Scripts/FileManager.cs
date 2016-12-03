using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class FileManager
{

	public static void Save (Configuration configuration)
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/" + configuration.Name);
		bf.Serialize (file, configuration);
		file.Close ();
	}

	public static Configuration Load (string fileName)
	{
		if (File.Exists (Application.persistentDataPath + "/" + fileName)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/" + fileName, FileMode.Open);
			Configuration configuration = (Configuration)bf.Deserialize (file);
			file.Close ();
			return configuration;
		} else {
			return new Configuration ();
		}
	}

	public static void DeleteFile (string fileName)
	{
		if (File.Exists (Application.persistentDataPath + "/" + fileName)) {
			File.Delete (Application.persistentDataPath + "/" + fileName);
		} 
	}
}
