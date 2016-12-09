using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadConfigurationViewModel : MonoBehaviour
{
	private string applicationPath;

	private string ApplicationPath { get { return applicationPath; } set { applicationPath = value; } }

	private Configuration configuration;

	private ConfigFilesScrollList configFilesList;

	private ConfigFilesScrollList ConfigFilesList { get { return configFilesList; } set { configFilesList = value; } }

	private Text errormessagesText;

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

	public System.Action<int> OnSelectionChanged;

	void Start ()
	{
		ApplicationPath = Application.persistentDataPath;
		ConfigFilesList = GameObject.Find ("GameController").GetComponent<ConfigFilesScrollList> ();
		errormessagesText = GameObject.Find ("ErrorMessagesText").GetComponent<Text> ();
		configuration = new Configuration ();
		ConfigFilesList.OnSelectionChanged = ShowSelectedConfigIndex;
	}

	public void LoadSelectedConfig ()
	{
		this.LoadConfigurationDTOfromConfig ();
		this.SaveDataToPlayerPrefs ();
		this.LoadCorrespondingScene ();
	}

	private void ShowSelectedConfigIndex (int index)
	{
		Debug.Log ("Config index: " + index.ToString ());
	}

	private void LoadConfigurationDTOfromConfig ()
	{
		try {
			ConfigFile selectedConfig = ConfigFilesList.SelectedConfig;
			Debug.Log ("Selected Config Name: " + selectedConfig.FileName);
			if (readOnlyConfigurations.Contains (selectedConfig.FileName)) {
				this.LoadDefaultConfig (selectedConfig.FileName);
			} else {
				configuration = FileManager.Load (selectedConfig.FileName);
			}
		} catch (Exception ex) {
			this.ShowErrorMessage ("Error at loading configuration:", ex);
			Debug.Log ("Error at loading configuration");
		}
	}


	private void SaveDataToPlayerPrefs ()
	{
		Debug.Log ("SaveDataToPlayerPrefs in LoadConfigurationViewModelCalled");
		PlayerPrefs.SetString ("ConfigurationName", this.configuration.Name);
		PlayerPrefs.SetFloat ("BlurLevel", this.configuration.BlurLevel);
		PlayerPrefs.SetFloat ("TunnelLevel", this.configuration.TunnelLevel);
		PlayerPrefs.SetInt ("DelayLevel", this.configuration.Delay);
		PlayerPrefs.SetInt ("MotionBlur", this.configuration.MotionBlur);
		PlayerPrefs.SetInt ("RedColorDistortion", this.configuration.RedColor);    
		PlayerPrefs.SetInt ("RandomEffects", this.configuration.Randomization);
	}

	private void LoadDefaultConfig (string value)
	{
		switch (value) {
		case "Sober":
			this.LoadSoberConfig ();
			break;    
		case "Slightly Drunk":
			this.LoadSlightlyDrunkConfig ();
			break;
		case "Drunk":
			this.LoadDrunkConfig ();
			break;
		case "Very Drunk":
			this.LoadVeryDrunkConfig ();
			break;
		case "Create":
		case "Delete":
			break;
		default:
			this.LoadSoberConfig ();
			break;
		}
	}

	private void LoadSoberConfig ()
	{
		this.configuration.Name = "Sober";
		this.configuration.BlurLevel = 0;
		this.configuration.TunnelLevel = 0;
		this.configuration.Delay = 0;
		this.configuration.MotionBlur = 0;
		this.configuration.RedColor = 0;    
		this.configuration.Randomization = 0;
	}


	private void LoadSlightlyDrunkConfig ()
	{
		this.configuration.Name = "Slightly Drunk";
		this.configuration.BlurLevel = 2;
		this.configuration.TunnelLevel = 0;
		this.configuration.Delay = 0;
		this.configuration.MotionBlur = 0;
		this.configuration.RedColor = 0;    
		this.configuration.Randomization = 0;
	}

	private void LoadDrunkConfig ()
	{
		this.configuration.Name = "Drunk";
		this.configuration.BlurLevel = 3;
		this.configuration.TunnelLevel = 0;
		this.configuration.Delay = 0;
		this.configuration.MotionBlur = 0;
		this.configuration.RedColor = 0;    
		this.configuration.Randomization = 0;
	}

	private void LoadVeryDrunkConfig ()
	{
		this.configuration.Name = "Very Drunk";
		this.configuration.BlurLevel = 4;
		this.configuration.TunnelLevel = 0;
		this.configuration.Delay = 0;
		this.configuration.MotionBlur = 0;
		this.configuration.RedColor = 0;    
		this.configuration.Randomization = 0;
	}

	private void LoadScene (int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}

	private void ShowErrorMessage (string message, Exception exception)
	{
		this.errormessagesText.text = message + "\n" + exception.Message;
	}

	private void LoadCorrespondingScene ()
	{
		ConfigFile selectedConfig = ConfigFilesList.SelectedConfig;
		string value = selectedConfig.FileName;
		switch (value) {
		case "Sober":
		case "Slightly Drunk":
		case "Drunk":
		case "Very Drunk":
			this.LoadScene (2);
			break;    
		case "Create":
			this.LoadScene (1);
			break;
		case "Delete":
			this.LoadScene (3);
			break;
		default:
			this.LoadScene (2);
			break;
		}
	}
}
