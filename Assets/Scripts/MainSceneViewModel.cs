using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Assets.Scripts.Helpers;
using Assets.Scripts.MetaData;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneViewModel : MonoBehaviour
{
	private List<ConfigFile> configsList;
	public Transform contentPanel;
	public SimpleObjectPool buttonObjectPool;
	private Configuration configuration;
	private int count = 0;

	void Start ()
	{
		configsList = new  List<ConfigFile> ();
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
			GameObject toRemove = transform.GetChild (0).gameObject;
			buttonObjectPool.ReturnObject (toRemove);
		}
	}

	private void AddButtons ()
	{
		foreach (var config in configsList) {
			GameObject newButton = buttonObjectPool.GetObject ();
			newButton.transform.SetParent (contentPanel);
			SampleButton sampleButton = newButton.GetComponent<SampleButton> ();
			sampleButton.Setup (config, this);
		}
	}

	private void FillListWithDefaultConfigs ()
	{
		foreach (var fileName in DefaultConfigurations.List) {
			ConfigFile newConfigFile = new ConfigFile (fileName, "");
			configsList.Add (newConfigFile);
		}
	}

	private void FillListWithSavedConfigFiles ()
	{
		string[] fileEntries = Directory.GetFiles (Application.persistentDataPath);
		foreach (string fileName in fileEntries) {
			ConfigFile newConfigFile = new ConfigFile (Path.GetFileName (fileName), fileName);
			configsList.Add (newConfigFile);
		}
	}

	public void LoadSelectedConfiguration (ConfigFile configurationFile)
	{ //TODO use this to navigate to scene or to load configuration
		
		//	RemoveItem (item, this);
		//RefreshDisplay ();
		Debug.Log ("TryTransferItemToOtherShop called with item: " + configurationFile.FileName + " count: " + count.ToString ());
		count++;
		LoadConfigurationFromConfig (configurationFile);
		SaveDataToPlayerPrefs (configuration);
		LoadCorrespondingScene (configurationFile.FileName);
	}


	private void LoadConfigurationFromConfig (ConfigFile selectedConfig)
	{
		try {
			#if DEBUG
			Debug.Log ("Selected Config Name: " + selectedConfig.FileName);
			#endif
			if (DefaultConfigurations.List.Contains (selectedConfig.FileName)) {
				LoadDefaultConfig (selectedConfig.FileName);
			} else {
				configuration = FileManager.Load (selectedConfig.FileName);
			}
		} catch (Exception ex) {
			#if DEBUG
			Debug.Log ("Error at loading configuration");
			#endif
		}
	}

	private void SaveDataToPlayerPrefs (Configuration selectedConfig)
	{
		#if DEBUG
		Debug.Log ("SaveDataToPlayerPrefs in LoadConfigurationViewModelCalled");
		#endif
		PlayerPrefs.SetString (PlayerPreferences.ConfigurationName, configuration.Name);
		PlayerPrefs.SetFloat (PlayerPreferences.BlurLevel, configuration.BlurLevel);
		PlayerPrefs.SetFloat (PlayerPreferences.TunnelLevel, configuration.TunnelLevel);
		PlayerPrefs.SetInt (PlayerPreferences.DelayLevel, configuration.Delay);
		PlayerPrefs.SetInt (PlayerPreferences.MotionBlur, configuration.MotionBlur);
		PlayerPrefs.SetInt (PlayerPreferences.RedColorDistortion, configuration.RedColor);    
		PlayerPrefs.SetInt (PlayerPreferences.Randomization, configuration.Randomization);
	}


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