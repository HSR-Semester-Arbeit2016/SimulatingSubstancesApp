using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class SaveConfigurationSceneViewModel : MonoBehaviour
{
	private ConfigurationDTO configuration;

	private Text messagesText;

	public SaveConfigurationSceneViewModel ()
	{
		this.configuration = new ConfigurationDTO ();
	}

	public void SaveToFile ()
	{
		this.SaveDataToConfigurationDTO ();
		this.SaveConfigurationDTOToFile ();
	}

	private void SaveDataToConfigurationDTO ()
	{
		InputField fileNameInputField = GameObject.Find ("FileNameInputField").GetComponent<InputField> ();
		Debug.Log ("Input field text: " + fileNameInputField.text); 
		this.configuration.Name = fileNameInputField.text;
		this.configuration.BlurLevel = PlayerPrefs.GetFloat ("BlurLevel");
		this.configuration.TunnelLevel = PlayerPrefs.GetFloat ("TunnelLevel");
		this.configuration.Delay = PlayerPrefs.GetInt ("DelayLevel");
		this.configuration.MotionBlur = PlayerPrefs.GetInt ("MotionBlur");
		this.configuration.RedColor = PlayerPrefs.GetInt ("RedColorDistortion");    
		this.configuration.Randomness = PlayerPrefs.GetInt ("RandomEffects");
	}


	private void SaveConfigurationDTOToFile ()
	{
		messagesText = GameObject.Find ("MessagesText").GetComponent<Text> ();
		try {
			FileManager.Save (this.configuration);
			messagesText.text = configuration.Name + " Saved!";
		} catch (Exception ex) {
			this.ShowMessage ("Error at saving " + configuration.Name, ex);
		}
	}


	private void ShowMessage (string message, Exception ex)
	{
		messagesText.text = message + "\n" + ex.Message;
	}
}
