using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConfigFilesScrollList : MonoBehaviour
{
	public GameObject listButton;
	public Transform contentPanel;
	public System.Action<int> OnSelectionChanged;
	private int index;

	private int Index { get { return index; } set { this.index = value; } }

	private List<ConfigFile> configFilesList;

	private List<ConfigFile> ConfigFilesList { get { return configFilesList; } set { configFilesList = value; } }

	private readonly static List<string> defaultConfigurations = new List<string> (new string[] {
		"Sober",
		"Slightly Drunk",
		"Drunk",
		"Very Drunk",
		"Create",
		"Delete"
	});
		
	private readonly ReadOnlyCollection<string> readOnlyConfigurations = 
		new ReadOnlyCollection<string> (defaultConfigurations);

	public ConfigFile SelectedConfig { get; private set; }

	private string applicationPath;

	private string ApplicationPath { get { return applicationPath; } set { applicationPath = value; } }

	void Start ()
	{
		ConfigFilesList = new  List<ConfigFile> ();
		ApplicationPath = Application.persistentDataPath;
		this.FillListWithDefaultConfigs ();
		this.FillListWithSavedConfigFiles ();	
		this.FillListInGui ();
	}


	public ConfigFile this [int index] {
		get { return configFilesList [index]; }
	}



	public void RemoveSelectedConfig ()
	{
		foreach (var config in readOnlyConfigurations) {
			if (config.Equals (SelectedConfig.FileName)) {
				throw new TriedToDeleteDefaultConfigException ("The configuration you tried to delete\n is a default configuration!");
			} 
		}
		ConfigFilesList.Remove (SelectedConfig);
		this.ClearListInGui ();
		this.FillListInGui ();
	}


	private void FillListWithDefaultConfigs ()
	{
		foreach (var fileName in readOnlyConfigurations) {
			ConfigFile newConfigFile = new ConfigFile (fileName, "", true);
			configFilesList.Add (newConfigFile);
		}
	}

	private void FillListWithSavedConfigFiles ()
	{
		string[] fileEntries = Directory.GetFiles (ApplicationPath);
		foreach (string fileName in fileEntries) {
			ConfigFile newConfigFile = new ConfigFile (Path.GetFileName (fileName), fileName, false);
			this.configFilesList.Add (newConfigFile);
		}
	}

	private void FillListInGui ()
	{
		foreach (var file in ConfigFilesList) {
			GameObject newButton = Instantiate (listButton) as GameObject;
			ListButton button = newButton.GetComponent<ListButton> ();
			button.nameLabel.text = file.FileName;
			int index = configFilesList.IndexOf (file);
			button.button.onClick.AddListener (() => {
				OnButtonClicked (index);
			});
			newButton.transform.SetParent (contentPanel);
		}
	}

	private void ClearListInGui ()
	{
		contentPanel.DetachChildren ();
	}

	private void  OnButtonClicked (int index)
	{
		SelectedConfig = configFilesList [index];
		OnSelectionChanged (index);
		Index = index;
	}
}
