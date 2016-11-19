using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConfigFilesScrollList : MonoBehaviour
{
	public GameObject listButton;
	private List<ConfigFile> fileList;
	public Transform contentPanel;

	private ConfigFile selectedFile;

	public ConfigFile SelectedFile { get; private set; }

	private string applicationPath;

	private string ApplicationPath { get { return applicationPath; } set { applicationPath = value; } }

	void Start ()
	{
		fileList = new  List<ConfigFile> ();
		ApplicationPath = Application.persistentDataPath;
		PopulateList ();	
	}

	void PopulateList ()
	{
		string[] fileEntries = Directory.GetFiles (ApplicationPath);
		foreach (string fileName in fileEntries) {
			ConfigFile newConfigFile = new ConfigFile (Path.GetFileName (fileName), fileName);
			this.fileList.Add (newConfigFile);
			GameObject newButton = Instantiate (listButton) as GameObject;
			ListButton button = newButton.GetComponent<ListButton> ();
			button.nameLabel.text = newConfigFile.FileName;
			int index = fileList.IndexOf (newConfigFile);
			button.button.onClick.AddListener (() => {
				OnButtonClicked (index);
			});
			newButton.transform.SetParent (contentPanel);
		}


		/*
		foreach (var file in fileList) {
			GameObject newButton = Instantiate (listButton) as GameObject;
			ListButton button = newButton.GetComponent<ListButton> ();
			button.nameLabel.text = file.FileName;
			int index = fileList.IndexOf (file);
			button.button.onClick.AddListener (() => {
				OnButtonClicked (index);
			});
			newButton.transform.SetParent (contentPanel);

		}
		*/
	}

	public void OnButtonClicked (int index)
	{
		Debug.Log ("File index: " + index.ToString ());
		SelectedFile = fileList [index];
		Debug.Log ("File name: " + SelectedFile.FileName);
	}



}
