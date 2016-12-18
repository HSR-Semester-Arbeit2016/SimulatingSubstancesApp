using System;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.MetaData;
using UnityEngine;

namespace Assets.Scripts
{
	/// <summary>
	/// In Unity there are no scrollable lists with selectable elements. This class provides that functionality by creating, 
	/// filling and updating the <see cref="ConfigFile"/>list used in the <see cref="DeleteConfigViewModel"/>
	/// </summary>
	public class ConfigFilesScrollList : MonoBehaviour
	{
		private List<ConfigFile> configFilesList;
		/// <summary>
		/// As its name indicates, this panel contains all the listButtons or list elements. 
		/// This reference must be set by dragging & dropping of the ContentPanel in the Unity Editor
		/// </summary>
		public Transform contentPanel;
		/// <summary>
		/// This is a reference to the ListItemButton prefab. 
		/// The reference must be set in the Unity editor by dragging & dropping the ListItemButton prefab.
		/// </summary>
		public GameObject listButton;
		public Action<int> OnSelectionChanged;

		public ConfigFile SelectedConfig { get; private set; }

		public ConfigFile this [int index] {
			get { return configFilesList [index]; }
		}

		private void Start ()
		{
			configFilesList = new List<ConfigFile> ();
			FillListWithSavedConfigFiles ();
			FillListInGui ();
		}

		public void RemoveSelectedConfig ()
		{
			configFilesList.Remove (SelectedConfig);
			ClearListInGui ();
			FillListInGui ();
		}


		private void FillListWithSavedConfigFiles ()
		{
			var fileEntries = Directory.GetFiles (Application.persistentDataPath);
			foreach (var fileName in fileEntries) {
				var newConfigFile = new ConfigFile (Path.GetFileName (fileName));
				configFilesList.Add (newConfigFile);
			}
		}

		/// <summary>
		/// For each element in the configFilesList this method instantiates a listButton prefab, assigns it the file's name, 
		/// registers a listener with the element's index and finally adds the new prefab to the ContentPanel in order to show it in the GUI 
		/// </summary>
		private void FillListInGui ()
		{
			foreach (var file in configFilesList) {
				var newButton = Instantiate (listButton);
				var button = newButton.GetComponent<ListButton> ();
				button.nameLabel.text = file.FileName;
				var index = configFilesList.IndexOf (file);
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
}