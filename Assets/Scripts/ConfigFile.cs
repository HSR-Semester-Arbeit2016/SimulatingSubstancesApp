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
    public bool IsImmutable { get; set; }

	public ConfigFile (string fileName, string filePath, bool isImmutable)
	{
		FileName = fileName;
		FilePath = filePath;
		IsImmutable = isImmutable;
	}
}
