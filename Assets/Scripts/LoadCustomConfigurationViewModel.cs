using System.Collections;
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


	public void UpdateList ()
	{
		DropDownList list = GameObject.Find ("CustomConfigsDropDownList").GetComponent<DropDownList> ();
		string[] fileEntries = Directory.GetFiles (ApplicationPath);
		foreach (string fileName in fileEntries) {
			DropDownListItem listItem = new DropDownListItem (Path.GetFileName (fileName), fileName);
			list.AddItem (listItem);
			// TODO mejorar esto. la lista n0 se llena cuando la escena se carga
		}


	}
}
