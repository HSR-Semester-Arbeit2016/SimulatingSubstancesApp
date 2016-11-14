using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;


public class LoadCustomConfigurationViewModel : MonoBehaviour
{
	private string applicationPath;

	private string ApplicationPath { get { return applicationPath; } set { applicationPath = value; } }

	void Start ()
	{
		ApplicationPath = Application.persistentDataPath;
	}


	public void RefreshList ()
	{
		DropDownList dropDownConfigFilesList = GameObject.Find ("CustomConfigsDropDownList").GetComponent<DropDownList> ();
		dropDownConfigFilesList.Clear ();
		string[] fileEntries = Directory.GetFiles (ApplicationPath);
		foreach (string fileName in fileEntries) {
			DropDownListItem listItem = new DropDownListItem (Path.GetFileName (fileName), fileName);
			dropDownConfigFilesList.AddItem (listItem);
		}
	}
}
