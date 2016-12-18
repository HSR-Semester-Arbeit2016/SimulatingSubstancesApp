using System;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.Helpers;
using Assets.Scripts.MetaData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	/// <summary>
	/// Manages the Main Scene view, principally its only GUI component, a custom scrollable list with selectable items.
	/// Same as in the ConfigFilesScrollList, this class creates the list, fills it with button prefabs and updates it. 
	/// Its difference to ConfigFilesScrollList is the implementation, using a SimpleObjectPool helper for prefab instantiation.
	/// This class also manages user's input by implementing the methods called in the OnButtonClick method of the SampleButton class.
	/// See the ConfigFilesScrollList, SimpleObjectPool and SampleButton classes.
	/// </summary>
	public class MainSceneViewModel : MonoBehaviour
	{
		/// <summary>
		/// Creates prefab objects
		/// </summary>
		public SimpleObjectPool buttonObjectPool;
		private List<ConfigFile> configsList;
		private Configuration configuration;
		/// <summary>
		/// Reference to the panel containing the list elements (buttons prefabs).
		/// This reference must be set by dragging & dropping of the ContentPanel in the Unity Editor to the MainSceneViewModel script.
		/// </summary>
		public Transform contentPanel;

		private void Start ()
		{
			configsList = new List<ConfigFile> ();
			configuration = new Configuration ();
			RefreshDisplay ();
		}

		private void RefreshDisplay ()
		{
			FillListWithDefaultConfigs ();
			FillListWithSavedConfigFiles ();
			RemoveButtons ();
			AddButtons ();
		}

		private void RemoveButtons ()
		{
			while (contentPanel.childCount > 0) {
				var toRemove = transform.GetChild (0).gameObject;
				buttonObjectPool.ReturnObject (toRemove);
			}
		}

		private void AddButtons ()
		{
			foreach (var config in configsList) {
				var newButton = buttonObjectPool.GetObject (); //Obtain a new button prefab instance
				newButton.transform.SetParent (contentPanel);  // Set the contentPanel as prefab's parent 
				var sampleButton = newButton.GetComponent<SampleButton> (); 
				sampleButton.Setup (config, this);
			}
		}

		private void FillListWithDefaultConfigs ()
		{
			foreach (var fileName in DefaultConfigurations.List) {
				var newConfigFile = new ConfigFile (fileName);
				configsList.Add (newConfigFile);
			}
		}

		private void FillListWithSavedConfigFiles ()
		{
			var fileEntries = Directory.GetFiles (Application.persistentDataPath);
			foreach (var fileName in fileEntries) {
				var newConfigFile = new ConfigFile (Path.GetFileName (fileName));
				configsList.Add (newConfigFile);
			}
		}

		/// <summary>
		/// This method is called in the OnButtonClick method of the SampleButton class each time the user selects a list item
		/// </summary>
		/// <param name="configurationFile">Configuration file.</param>
		public void LoadSelectedConfiguration (ConfigFile configurationFile)
		{
			LoadConfigurationFromConfig (configurationFile);
			SaveDataToPlayerPrefs (configuration);
			LoadCorrespondingScene (configurationFile.FileName);
		}


		private void LoadConfigurationFromConfig (ConfigFile selectedConfig)
		{
			try { 	
				if (DefaultConfigurations.List.Contains (selectedConfig.FileName)) {
					LoadDefaultConfig (selectedConfig.FileName);
				} else {
					configuration = FileManager.Load (selectedConfig.FileName);
				}
			} catch (Exception ex) {
				#if DEBUG
				Debug.Log ("Error at loading configuration" + ex.Message);
				#endif
			}
		}
	    /// <summary>
	    /// Saves the data to player prefs for data sharing between scenes
	    /// </summary>
	    /// <param name="selectedConfig">Selected config.</param>
		private void SaveDataToPlayerPrefs (Configuration selectedConfig)
		{  
			PlayerPrefs.SetString (PlayerPreferences.ConfigurationName, configuration.Name);
			PlayerPrefs.SetFloat (PlayerPreferences.BlurLevel, configuration.BlurLevel);
			PlayerPrefs.SetFloat (PlayerPreferences.TunnelLevel, configuration.TunnelLevel);
			PlayerPrefs.SetInt (PlayerPreferences.DelayLevel, configuration.Delay);
			PlayerPrefs.SetInt (PlayerPreferences.MotionBlur, configuration.MotionBlur);
			PlayerPrefs.SetInt (PlayerPreferences.RedColorDistortion, configuration.RedColor);
			PlayerPrefs.SetInt (PlayerPreferences.Randomization, configuration.Randomization);
		}

		/// <summary>
		/// Called if the selected item in the list is a default configuration
		/// </summary>
		/// <param name="configName">Config name.</param>
		private void LoadDefaultConfig (string configName)
		{
			switch (configName) {
			case ConfigurationNames.CreateNew:
			case ConfigurationNames.DeleteExisting:
				break;
			default:
				configuration = ConfigurationHelper.GetDefaultConfig (configName);
				break;
			}
		}

		private void LoadCorrespondingScene (string selectedConfig)
		{
			switch (selectedConfig) {
			case ConfigurationNames.Sober:
			case ConfigurationNames.SlightlyDrunk:
			case ConfigurationNames.Drunk:
			case ConfigurationNames.VeryDrunk:
				LoadScene (2);
				break;
			case ConfigurationNames.CreateNew:
				LoadScene (1);
				break;
			case ConfigurationNames.DeleteExisting:
				LoadScene (3);
				break;
			default:
				LoadScene (2);
				break;
			}
		}


		private void LoadScene (int sceneIndex)
		{
			SceneManager.LoadScene (sceneIndex);
		}
	}
}