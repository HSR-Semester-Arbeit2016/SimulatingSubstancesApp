using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Assets.Scripts.MetaData;
using Assets.Scripts.MetaData.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeleteConfigViewModel : MonoBehaviour
{

	public string ApplicationPath { get; set; }
    public ConfigFilesScrollList ConfigFilesScrollList { get; set; }
    public Action<int> OnSelectionChanged;
    private Configuration configuration;
    private Text messageText;

	void Start ()
	{
		ApplicationPath = Application.persistentDataPath;
		ConfigFilesScrollList = GameObject.Find (DeleteConfigurationControls.GameController).GetComponent<ConfigFilesScrollList> ();
		messageText = GameObject.Find (DeleteConfigurationControls.ErrorMessagesText).GetComponent<Text> ();
		configuration = new Configuration ();
        ConfigFilesScrollList.OnSelectionChanged = ShowSelectedConfig;
	}


	private void ShowSelectedConfig (int index)
	{
#if DEBUG
        Debug.Log ("ShowSelectedConfig called with index: " + index);
#endif
        try {
			ResetConfig ("");
			ConfigFile selectedConfig = ConfigFilesScrollList[index];
			if (DefaultConfigurations.List.Contains (selectedConfig.FileName)) {
				LoadDefaultConfig (selectedConfig.FileName);
#if DEBUG
                Debug.Log ("ShowSelectedConfig called with file name: " + selectedConfig.FileName);
#endif
			    messageText.text = configuration.ToString();
			} else {
				configuration = FileManager.Load (selectedConfig.FileName);
                messageText.text = configuration.ToString();
            }
		} catch (Exception ex) {
			ShowErrorMessage (Messages.ConfigLoadingError, ex);
		}
	}

	public void Delete ()
	{
		try {
			ResetConfig ("");
			ConfigFile selectedConfig = ConfigFilesScrollList.SelectedConfig;
            ConfigFilesScrollList.RemoveSelectedConfig ();
			FileManager.DeleteFile (selectedConfig.FileName);
		} catch (TriedToDeleteDefaultConfigException ex) {
			ShowErrorMessage (Messages.ConfigDeletingError, ex);
		} catch (Exception ex) {
            //TODO: Make sure the custom-exception is actually needed and not just a different MessageText
			ShowErrorMessage (Messages.ConfigDeletingError, ex);
		}
	}

    //TODO: Possibly extract into HelperClass
	private void ShowErrorMessage (string message, Exception ex)
	{
		messageText.text = message + "\n" + ex.Message;
	}
    //TODO: Extract into ConfigurationFactory
	private void ResetConfig (string value)
	{
		configuration.Name = value;
		configuration.BlurLevel = 0;
		configuration.TunnelLevel = 0;
		configuration.Delay = 0;
		configuration.MotionBlur = 0;
		configuration.RedColor = 0; 
		configuration.Randomization = 0;
	}

    //TODO: Possibly extract into HelperClass
	private void LoadDefaultConfig (string configName)
	{
		switch (configName.ToLower()) {
		case ConfigurationNames.CreateNew:
		case ConfigurationNames.DeleteExisting:
			ResetConfig (configName);
			break;
		default:
			configuration = DefaultConfigurations.GetDefaultConfig(configName);
			break;
		}
    }
}