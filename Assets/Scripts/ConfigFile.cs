using System;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class ConfigFile
{
	private string fileName;

	public string FileName { get { return fileName; } set { fileName = value; } }

	private string filePath { get { return filePath; } set { filePath = value; } }

	public string FilePath;
	private bool isInmutable;

	public bool IsInmutable { get { return isInmutable; } set { isInmutable = value; } }

	public ConfigFile (string fileName, string filePath, bool isInmutable)
	{
		this.FileName = fileName;
		this.FilePath = filePath;
		this.IsInmutable = isInmutable;
	}
}
