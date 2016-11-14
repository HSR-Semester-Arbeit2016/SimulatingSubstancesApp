using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveConfiguration
{

	public static void Save (ConfigurationDTO configuration)
	{
		Debug.Log ("SaveConfiguration save called");
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/" + configuration.Name);
		bf.Serialize (file, configuration);
		file.Close ();

		// TODO exception handling
	}

	public static ConfigurationDTO Load (string fileName)
	{
		//TODO complete this exception handling
		if (File.Exists (Application.persistentDataPath + "/" + fileName)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/" + fileName, FileMode.Open);
			ConfigurationDTO configuration = (ConfigurationDTO)bf.Deserialize (file);
			file.Close ();
			return configuration;
		} else {
			return new ConfigurationDTO ();
		}
	}
}
