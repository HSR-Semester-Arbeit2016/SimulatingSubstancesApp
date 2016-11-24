using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadConfigurationViewModel : MonoBehaviour
{
	private string applicationPath;

	private string ApplicationPath { get { return applicationPath; } set { applicationPath = value; } }

	private ConfigurationDTO configuration;

	private ConfigFilesScrollList configFilesList;

	private ConfigFilesScrollList ConfigFilesList { get { return configFilesList; } set { configFilesList = value; } }

	private Text messagesText;


	private readonly static List<string> defaultConfigurations = new List<string> (new string[] {
		"Sober",
		"Slightly Drunk",
		"Drunk",
		"Very Drunk"
	});

	private readonly ReadOnlyCollection<string> readOnlyConfigurations = 
		new ReadOnlyCollection<string> (defaultConfigurations);


	void Start ()
	{
		ApplicationPath = Application.persistentDataPath;
		ConfigFilesList = GameObject.Find ("GameController").GetComponent<ConfigFilesScrollList> ();
		messagesText = GameObject.Find ("MessagesText").GetComponent<Text> ();
		configuration = new ConfigurationDTO ();
	}

	public void LoadSelectedFile ()
	{
		this.LoadConfigurationDTOfromConfig ();
		this.SaveDataToPlayerPrefs ();
	}

	private void LoadConfigurationDTOfromConfig ()
	{
		try {
			ConfigFile selectedConfig = ConfigFilesList.SelectedConfig;
			if (readOnlyConfigurations.Contains (selectedConfig.FileName)) {
				this.LoadDefaultConfig (selectedConfig.FileName);
				this.ShowConfigFromDTo ();
			} else {
				configuration = FileManager.Load (selectedConfig.FileName);
				this.ShowConfigFromDTo ();
			}
		} catch (Exception ex) {
			this.ShowErrorMessage ("Error at loading configuration", ex);
		}
	}


	private void SaveDataToPlayerPrefs ()
	{
		PlayerPrefs.SetString ("ConfigurationName", this.configuration.Name);
		PlayerPrefs.SetFloat ("BlurLevel", this.configuration.BlurLevel);
		PlayerPrefs.SetFloat ("TunnelLevel", this.configuration.TunnelLevel);
		PlayerPrefs.SetInt ("DelayLevel", this.configuration.Delay);
		PlayerPrefs.SetInt ("MotionBlur", this.configuration.MotionBlur);
		PlayerPrefs.SetInt ("RedColorDistortion", this.configuration.RedColor);    
		PlayerPrefs.SetInt ("RandomEffects", this.configuration.Randomness);
	}

	public void Delete ()
	{
		try {
			this.ResetMessages ();
			ConfigFile selectedConfig = ConfigFilesList.SelectedConfig;
			FileManager.DeleteFile (selectedConfig.FileName);
			configFilesList.RemoveSelectedConfig ();
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

	private void ResetMessages ()
	{
		messagesText.text = "";
	}

	private void ShowConfigFromDTo ()
	{
		messagesText.text = "File Name: " + this.configuration.Name;
		messagesText.text = "Blur Level: " + this.configuration.BlurLevel.ToString ();
		messagesText.text += "\nTunnel Level: " + this.configuration.TunnelLevel.ToString ();
		messagesText.text += "\nDelay Level: " + this.configuration.Delay.ToString ();
		messagesText.text += "\nMotion Blur: " + (this.configuration.MotionBlur == 0 ? "Off" : "On");  
		messagesText.text += "\nRedColor Distortion: " + (this.configuration.RedColor == 0 ? "Off" : "On");
		messagesText.text += "\nRandom Effects: " + (this.configuration.Randomness == 0 ? "Off" : "On");
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
		this.configuration.Randomness = 0;
	}


	private void LoadSlightlyDrunkConfig ()
	{
		this.configuration.Name = "Slightly Drunk";
		this.configuration.BlurLevel = 2;
		this.configuration.TunnelLevel = 0;
		this.configuration.Delay = 0;
		this.configuration.MotionBlur = 0;
		this.configuration.RedColor = 0;    
		this.configuration.Randomness = 0;
	}

	private void LoadDrunkConfig ()
	{
		this.configuration.Name = "Drunk";
		this.configuration.BlurLevel = 3;
		this.configuration.TunnelLevel = 0;
		this.configuration.Delay = 0;
		this.configuration.MotionBlur = 0;
		this.configuration.RedColor = 0;    
		this.configuration.Randomness = 0;
	}

	private void LoadVeryDrunkConfig ()
	{
		this.configuration.Name = "Very Drunk";
		this.configuration.BlurLevel = 4;
		this.configuration.TunnelLevel = 0;
		this.configuration.Delay = 0;
		this.configuration.MotionBlur = 0;
		this.configuration.RedColor = 0;    
		this.configuration.Randomness = 0;
	}
}
