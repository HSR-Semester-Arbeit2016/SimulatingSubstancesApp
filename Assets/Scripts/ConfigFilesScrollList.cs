using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Assets.Scripts.MetaData;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConfigFilesScrollList : MonoBehaviour
{
	public GameObject listButton;
	public Transform contentPanel;
    private List<ConfigFile> configFilesList;
    public System.Action<int> OnSelectionChanged;
	public ConfigFile SelectedConfig { get; private set; }

	void Start ()
	{
		configFilesList = new  List<ConfigFile> ();
		FillListWithDefaultConfigs ();
		FillListWithSavedConfigFiles ();	
		FillListInGui ();
	}


	public ConfigFile this [int index] {
		get { return configFilesList [index]; }
	}

	public void RemoveSelectedConfig ()
	{
		foreach (var config in DefaultConfigurations.List) {
			if (config.Equals (SelectedConfig.FileName)) {
				throw new TriedToDeleteDefaultConfigException (Messages.ConfigDeletingDefaultError);
			} 
		}
		configFilesList.Remove (SelectedConfig);
		ClearListInGui ();
		FillListInGui ();
	}

	private void FillListWithDefaultConfigs ()
	{
		foreach (var fileName in DefaultConfigurations.List) {
			ConfigFile newConfigFile = new ConfigFile (fileName, "", true);
			configFilesList.Add (newConfigFile);
		}
	}

	private void FillListWithSavedConfigFiles ()
	{
		string[] fileEntries = Directory.GetFiles (Application.persistentDataPath);
		foreach (string fileName in fileEntries) {
			ConfigFile newConfigFile = new ConfigFile (Path.GetFileName (fileName), fileName, false);
			configFilesList.Add (newConfigFile);
		}
	}

	private void FillListInGui ()
	{
		foreach (var file in configFilesList) {
			GameObject newButton = Instantiate (listButton);
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

	private void OnButtonClicked (int index)
	{
		SelectedConfig = configFilesList [index];
		OnSelectionChanged (index);
	}
}
