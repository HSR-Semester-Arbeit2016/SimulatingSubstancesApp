using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeleteConfigViewModel : MonoBehaviour
{

	private string applicationPath;

	private string ApplicationPath { get { return applicationPath; } set { applicationPath = value; } }

	private Configuration configuration;

	private ConfigFilesScrollList configFilesList;

	private ConfigFilesScrollList ConfigFilesList { get { return configFilesList; } set { configFilesList = value; } }

	private Text messagesText;


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
		messagesText = GameObject.Find ("MessagesText").GetComponent<Text> ();
		configuration = new Configuration ();
		ConfigFilesList.OnSelectionChanged = ShowSelectedConfig;
	}


	private void ShowSelectedConfig (int index)
	{
		Debug.Log ("ShowSelectedConfig called with index: " + index.ToString ());
		try {
			this.ResetMessages ("");
			ConfigFile selectedConfig = ConfigFilesList [index];
			if (readOnlyConfigurations.Contains (selectedConfig.FileName)) {
				this.LoadDefaultConfig (selectedConfig.FileName);
				Debug.Log ("ShowSelectedConfig called with file name: " + selectedConfig.FileName);
				this.ShowConfigFromDTo ();
			} else {
				configuration = FileManager.Load (selectedConfig.FileName);
				this.ShowConfigFromDTo ();
			}
		} catch (Exception ex) {
			this.ShowErrorMessage ("Error at loading configuration", ex);
		}
	}

	public void Delete ()
	{
		try {
			this.ResetMessages ("");
			ConfigFile selectedConfig = ConfigFilesList.SelectedConfig;
			configFilesList.RemoveSelectedConfig ();
			FileManager.DeleteFile (selectedConfig.FileName);
		} catch (TriedToDeleteDefaultConfigException ex) {
			this.ShowErrorMessage ("Error at deleting configuration:", ex);
		} catch (Exception ex) {
			this.ShowErrorMessage ("Error at deleting configuration:", ex);
		}
	}

	private void ShowErrorMessage (string message, Exception ex)
	{
		messagesText.text = message + "\n" + ex.Message;
	}

	private void ResetMessages (string value)
	{
		this.configuration.Name = value;
		this.configuration.BlurLevel = 0;
		this.configuration.TunnelLevel = 0;
		this.configuration.Delay = 0;
		this.configuration.MotionBlur = 0;
		this.configuration.RedColor = 0; 
		this.configuration.Randomization = 0;
	}

	private void ShowConfigFromDTo ()
	{
		messagesText.text = "Configuration Properties:\n";
		messagesText.text += "\nConfiguration Name: " + this.configuration.Name;
		messagesText.text += "\nBlur Level: " + this.configuration.BlurLevel.ToString ();
		messagesText.text += "\nTunnel Level: " + this.configuration.TunnelLevel.ToString ();
		messagesText.text += "\nDelay Level: " + (this.configuration.Delay == 0 ? "Off" : "On");  
		messagesText.text += "\nMotion Blur: " + (this.configuration.MotionBlur == 0 ? "Off" : "On");  
		messagesText.text += "\nRedColor Distortion: " + (this.configuration.RedColor == 0 ? "Off" : "On");
		messagesText.text += "\nRandom Effects: " + (this.configuration.Randomization == 0 ? "Off" : "On");
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
			this.ResetMessages (value);
			break;
		case "Delete":
			this.ResetMessages (value);
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
}