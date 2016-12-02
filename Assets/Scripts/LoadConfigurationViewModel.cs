using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Assets.Scripts.MetaData;
using Assets.Scripts.MetaData.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadConfigurationViewModel : MonoBehaviour
{
	private Configuration configuration;
	private ConfigFilesScrollList configFilesList;
	private Text errormessagesText;
	public System.Action<int> OnSelectionChanged;

	void Start ()
	{
		configFilesList = GameObject.Find (LoadConfigurationControls.GameController).GetComponent<ConfigFilesScrollList> ();
		errormessagesText = GameObject.Find (LoadConfigurationControls.ErrorMessagesText).GetComponent<Text> ();
		configuration = new Configuration ();
		configFilesList.OnSelectionChanged = ShowSelectedConfigIndex;
	}

	public void LoadSelectedConfig ()
	{
		LoadConfigurationFromConfig(configFilesList.SelectedConfig);
		SaveDataToPlayerPrefs(configuration);
        LoadCorrespondingScene(configFilesList.SelectedConfig.FileName);
	}

	private void ShowSelectedConfigIndex (int index)
	{
#if DEBUG
        Debug.Log ("Config index: " + index);
#endif
    }

	private void LoadConfigurationFromConfig (ConfigFile selectedConfig)
	{
		try {
#if DEBUG
            Debug.Log ("Selected Config Name: " + selectedConfig.FileName);
#endif
            if (DefaultConfigurations.List.Contains(selectedConfig.FileName)) {
				LoadDefaultConfig (selectedConfig.FileName);
			} else {
				configuration = FileManager.Load (selectedConfig.FileName);
			}
		} catch (Exception ex) {
			ShowErrorMessage (Messages.ConfigLoadingError, ex);
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
                configuration = DefaultConfigurations.GetDefaultConfig(configName);
                break;
		}
	}

	private void LoadScene (int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}

    //TODO: Possibly extract into HelperClass
	private void ShowErrorMessage (string message, Exception exception)
	{
		errormessagesText.text = message + "\n" + exception.Message;
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
}
