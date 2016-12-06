using System;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class ConfigFile
{
    public string FilePath { get; set; }
    public string FileName { get; set; }

	public ConfigFile (string fileName, string filePath)
	{
		FileName = fileName;
		FilePath = filePath;
	}
}
